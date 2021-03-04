using System;
using System.Data;
using static System.Console;
using Microsoft.Data.Sqlite;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace _2_lab2
{
    class Provider
    {
        public int id;
        public string nameProvider;
        public int speed;
        public string nameClient;
        public override string ToString()
        {
            return $"|{id}| {nameProvider} *Speed: {speed}Mb/s *User: {nameClient}";
        }
    }
    class ListProvider
    {
        private Provider[] _items;
        private int _size;
        
        public ListProvider()
        {
            _items = new Provider[16];  
            _size = 0;
        }
        public void Add(Provider newPr) 
        {
            if (this._size >= this._items.Length)
            {
                this.Expand();
            }
            this._items[this._size] = newPr;
            this._size += 1;
        }
        private void Expand()
        {
            int oldCapacity = this._items.Length;
            Provider[] oldArray = this._items;
            this._items = new Provider[oldCapacity * 2];
            System.Array.Copy(oldArray, this._items, oldCapacity);
        }
        public IEnumerator<Provider> GetEnumerator() 
        {
            return this._items.Take(this._size).GetEnumerator();
        }

        public void Print(ListProvider providers)
        {
            foreach(Provider prov in providers)
            {   
                WriteLine(prov);
            }
        }

    }
    class ProviderRepository 
    {
        private SqliteConnection connection;
 
        public ProviderRepository(SqliteConnection connection)
        {
            this.connection = connection;
        }
        public long GetCountBooks(SqliteConnection connection)
        {        
            SqliteCommand command = connection.CreateCommand();
            command.CommandText = @"SELECT COUNT(*) FROM providers";
            return (long)command.ExecuteScalar();;
        }
        public Provider GetById(int id, SqliteConnection connection)
        {
            SqliteCommand command = connection.CreateCommand();
            command.CommandText = @"SELECT * FROM providers WHERE id = $id";
            command.Parameters.AddWithValue("$id", id);
        
            SqliteDataReader reader = command.ExecuteReader();
            Provider pr = new Provider();
            if (reader.Read())
            {   
                pr.id = int.Parse(reader.GetString(0));;
                pr.nameProvider = reader.GetString(1);
                pr.speed = int.Parse(reader.GetString(2));
                pr.nameClient = reader.GetString(3);
            }

            reader.Close();
            return pr;  
        }
        public int DeleteById(int id, SqliteConnection connection) 
        {             
            SqliteCommand command = connection.CreateCommand();
            command.CommandText = @"DELETE FROM providers WHERE id = $id";
            command.Parameters.AddWithValue("$id", id);
            
            int nChanged = command.ExecuteNonQuery();
            
            return nChanged;
        }
        public long Insert(Provider prov, SqliteConnection connection) 
        {        
            SqliteCommand command = connection.CreateCommand();
            command.CommandText = @"
            INSERT INTO providers (nameProvider, speed, nameClient)
            VALUES ($nameProvider, $speed, $nameClient);

            SELECT last_insert_rowid();
            ";
            command.Parameters.AddWithValue("$nameProvider", prov.nameProvider);
            command.Parameters.AddWithValue("$speed", prov.speed);
            command.Parameters.AddWithValue("$nameClient", prov.nameClient);
            
            long newId = (long)command.ExecuteScalar();
            
            return newId;
        }

        public int GetTotalPages(SqliteConnection connection)
        {
            const int pageSize = 10;
            return (int)Math.Ceiling(this.GetCountBooks(connection) / (double)pageSize);
        }
        
        public ListProvider GetPage(int pageNumber, SqliteConnection connection) 
        { 
            const int pageSize = 10;
            SqliteCommand command = connection.CreateCommand();
            command.CommandText = @"SELECT * FROM providers LIMIT $pageSize OFFSET $pageSize * ($pageNumber - 1)";
            command.Parameters.AddWithValue("$pageSize", pageSize);
            command.Parameters.AddWithValue("$pageNumber", pageNumber);
            SqliteDataReader reader = command.ExecuteReader();
            ListProvider providers = new ListProvider();
            while(reader.Read())
            {
                Provider pr = new Provider();
                pr.id = int.Parse(reader.GetString(0));
                pr.nameProvider = reader.GetString(1);
                pr.speed = int.Parse(reader.GetString(2));
                pr.nameClient = reader.GetString(3);

                providers.Add(pr);
            }
            reader.Close();
            return providers;
        }
        public ListProvider GetExport(string valueX, SqliteConnection connection) 
        { 
            SqliteCommand command = connection.CreateCommand();
            command.CommandText = @"SELECT * FROM providers WHERE nameProvider = $valueX";
            command.Parameters.AddWithValue("$valueX", valueX);
            SqliteDataReader reader = command.ExecuteReader();
            StreamWriter write = new StreamWriter("./export.csv");
            ListProvider providers = new ListProvider();
            while(reader.Read())
            {
                
                Provider pr = new Provider();
                pr.id = int.Parse(reader.GetString(0));
                pr.nameProvider = reader.GetString(1);
                pr.speed = int.Parse(reader.GetString(2));
                pr.nameClient = reader.GetString(3);
                providers.Add(pr);

                string csvline = ConvertToCsv(pr);
                write.WriteLine(csvline);
            }
            reader.Close();
            write.Close();

            return providers;
        }
        static string ConvertToCsv(Provider pr)
        {
            return $"{pr.id},{pr.nameProvider},{pr.speed},{pr.nameClient}";
        }
    }


    class Program
    {
        static void ProcessGetByID(string command, ProviderRepository pr1, SqliteConnection connect)
        {
            // Provider pr = new Provider();
            string[] parts = command.Split(' ');
            int nId = int.Parse(parts[1]);
            Provider p1 = pr1.GetById(nId, connect);
            if(p1 == null)
            {
                WriteLine($"No found provider with that id: {nId}");
            }
            else
            {
                WriteLine($"Provider: |{p1.id}| {p1.nameProvider} *Speed: {p1.speed}Mb/s *User: {p1.nameClient}");
            }
        }       
        static void ProcessDeleteByID(string command, ProviderRepository pr1, SqliteConnection connect)
        {
            Provider pr = new Provider();
            string[] parts = command.Split(' ');
            int dId = int.Parse(parts[1]);
            if(!int.TryParse(parts[1], out pr.id))
            {
                throw new ArgumentException("Input is NOT a number");
            }
            int stat = pr1.DeleteById(dId, connect);
            if (stat == 0)
            {
                Console.WriteLine("Book NOT deleted.");
            }
            else 
            {
                Console.WriteLine("Book deleted.");
            }
        }       
        static void ProcessInsert(string command, ProviderRepository pr1, SqliteConnection connect)
        {
            Provider pr = new Provider();
            string[] parts = command.Split(' ');
            string info = parts[1];
            string[] partInfo = info.Split(',');
            string name = partInfo[0];
            int speed = int.Parse(partInfo[1]);
            string client = partInfo[2];
            pr.nameProvider = name;
            pr.speed = speed;
            pr.nameClient = client;
            
            long st = pr1.Insert(pr, connect);
           if (st == 0)
            {
                Console.WriteLine("Book NOT added.");
            }
            else 
            {
                Console.WriteLine("Book added. New id is: " + st);
            }
        } 
        static void ProcessGetPage(string command, ProviderRepository pr1, SqliteConnection connect)
        {
            int numPages = pr1.GetTotalPages(connect);
            WriteLine(numPages);
        }
        static void ProcessGetTotalPage(string command, ProviderRepository pr1, SqliteConnection connect)
        {
            string[] parts = command.Split(' ');
            int nPage = int.Parse(parts[1]);
            
            ListProvider providers = pr1.GetPage(nPage, connect);
            providers.Print(providers);
        } 
        static void ProcessExport(string command, ProviderRepository pr1, SqliteConnection connect)
        {
            string[] parts = command.Split(' ');
            string valueX = parts[1];
            
            ListProvider providers = pr1.GetExport(valueX, connect);
        } 

        static void Main(string[] args)
        {
            string databaseFile = "./povdb.db";
            SqliteConnection connect = new SqliteConnection($"Data Source = {databaseFile}");
            connect.Open();

            ConnectionState state = connect.State;
            if(state == ConnectionState.Open)
            {
                ProviderRepository pr1 = new ProviderRepository(connect);
                while(true)
                {
                    WriteLine("Enter command:");
                    string command = ReadLine();
                    if(command.StartsWith("getById"))
                    {
                        ProcessGetByID(command, pr1, connect);
                    }
                    else if(command.StartsWith("deleteById"))
                    {
                        ProcessDeleteByID(command, pr1, connect);
                    }
                    else if(command.StartsWith("insert"))
                    {
                        ProcessInsert(command, pr1, connect);
                    }
                    else if(command.StartsWith("getPage"))
                    {
                        ProcessGetPage(command, pr1, connect);
                    }
                    else if(command.StartsWith("getTotalPages"))
                    {
                        ProcessGetTotalPage(command, pr1, connect);
                    }
                    else if(command.StartsWith("export"))
                    {
                        ProcessExport(command, pr1, connect);
                    }
                    else if(command.StartsWith("exit"))
                    {
                        WriteLine("Goodbye");
                        break;
                    }
                    else
                    {
                        WriteLine($"Nonexistent command: {command}");
                    }
                } 
            }
        connect.Close();  
        }
    }
}
