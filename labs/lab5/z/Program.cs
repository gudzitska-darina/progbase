﻿using System;
using static System.IO.File;

namespace z
{
    struct Provider
{
    public int id;
    public string name;
    public string ip;
    public int speed;
}
    class Program
    {
        static void ProcessChar(string[] subcommand)
        {
            if(subcommand.Length != 2)
            {
                Console.WriteLine("Incorrect command! More subcommands required (аt least 1)!");
                return;
            }
           if(subcommand[1] == "all")
           {
               CharAll();
           }
           else if(subcommand[1] == "upper")
           {
               CharUpper();
           }
           else if(subcommand[1] == "alpha")
           {
               CharAlpha();
           }
           else if(subcommand[1] == "alnum")
           {
               CharAlnum();
           }
           else
           {
               Console.WriteLine("Incorrect subcommand! Your subcommand is: {0}. Please, true again.", subcommand[1]);
           }
        }
        static void ProcessString(string[] subcommand)
        {
           if(subcommand.Length < 2)
            {
                Console.WriteLine("Incorrect command! More subcommands required (аt least 1)!");
                return;
            }
           if(subcommand[1] == "print")
           {
               StringPrint();
           }
           else if(subcommand[1] == "set")
           {
               if(subcommand.Length != 3)
               {
                   Console.WriteLine("Incorrect command! More subcommands required!");
               }
               string str = subcommand[2];
               StringSetNewString(str, subcommand);
           }
           else if(subcommand[1] == "substr")
           {
               if(subcommand.Length <= 2)
               {
                   Console.WriteLine("Incorrect command! More subcommands required");
               }
               string indexSt = subcommand[2];
               string lengthSt = subcommand[3];
               int index = int.Parse(indexSt);
               int length = int.Parse(lengthSt);
               StringSubstr(index, length);
           }
           else
           {
               Console.WriteLine("Incorrect subcommand! Your subcommand is: {0}. Please, try again.", subcommand[1]);
           }
        }
        static void ProcessCSV(string[] subcommand)
        {
            if(subcommand.Length < 2)
            {
                Console.WriteLine("Incorrect command! More subcommands required (аt least 1)!");
                return;
            }
           if(subcommand[1] == "load")
           {
               CSVLoad(t3CsvText);
           }
           else if(subcommand[1] == "text")
           {
               CSVText();
           }
           else if(subcommand[1] == "table")
           {
                CSVTable(t3CsvText, subcommand);
           }
           else if(subcommand[1] == "entities")
           {
               CSVEntities(t3CsvText, subcommand);
           }
            else if(subcommand[1] == "get")
           {
               
               if(subcommand.Length != 3)
               {
                   Console.WriteLine("Incorrect command! More subcommands required");
               }
               CSVGetIndex(t3CsvText, subcommand);
           }
            else if(subcommand[1] == "set")
            {
               if(subcommand.Length != 5)
               {
                   Console.WriteLine("Incorrect command! More subcommands required");
               }
           
               CSVSetIndex(t3CsvText, subcommand);
           }
            else if(subcommand[1] == "save")
           {
                CSVSave();
           }
           else
           {
               Console.WriteLine("Incorrect subcommand! Your subcommand is: {0}. Please, try again.", subcommand[1]);
           }
        }
        

        static void CharAll()
        {
            for(int c = 0; c <= 127; c++)
            {
                Console.WriteLine("[{0}] ~{1}~", c, (char)c);
            }
        }
        static void CharUpper()
        {
             for(int c = 65; c <= 90; c++)
            {
                Console.WriteLine("[{0}] ~{1}~", c, (char)c);
            }
        }
        static void CharAlpha()
        {
            for(int c = 65; c <= 90; c++)
            {
                Console.WriteLine("[{0}] ~{1}~", c, (char)c);
            }
            for(int c = 97; c <= 122; c++)
            {
                Console.WriteLine("[{0}] ~{1}~", c, (char)c);
            }
        }
        static void CharAlnum()
        {
            for(int c = 48; c <= 57; c++)
            {
                Console.WriteLine("[{0}] ~{1}~", c, (char)c);
            }
        }

        static string t2String = "";
       
        static void StringPrint()
        {
            Console.WriteLine("Сurrent string and its length: '{0}', {1}",t2String, t2String.Length);
        }
        static void StringSetNewString(string newString,string[] subcommand)
        {
            newString = subcommand[2];
            t2String =  newString;
        }
        static void StringSubstr(int index, int length)
        {
            if(length < 0)
            {
                 Console.WriteLine("Incorrect command");
                 return;
            }
            string str = t2String.Substring(index, length);
            Console.WriteLine("Substring: ~{0}~", str);
        }

        static string t3CsvText = "";
        static string[,] t3Table = new string[0,0];
        static Provider[] t3Providers = new Provider[0];

        static string[,] CsvToTable(string csvText) 
        {
            string[] lines = csvText.Split("\r\n");

            string[] lines_without_row1 = new string[lines.Length - 1];
            for (int i = 1; i < lines.Length; i++)
            {
                lines_without_row1[i-1] = lines[i];
            }


            int num_rows = lines_without_row1.Length;
            int num_cols = lines_without_row1[0].Split(',').Length;

            string[,] arr_for_t3Table = new string[num_rows, num_cols];
            for (int r = 0; r < num_rows; r++)
            {
                string[] line_r = lines_without_row1[r].Split(',');
                for (int c = 0; c < num_cols; c++)
                {
                    arr_for_t3Table[r, c] = line_r[c];
                }
            }

            t3Table = arr_for_t3Table;

            Array.Resize(ref t3Providers, t3Table.GetLength(0));

           for (int r = 0; r < arr_for_t3Table.GetLength(0); r++)
            {
                string[] row = new string[arr_for_t3Table.GetLength(1)];
                for (int c = 0; c < arr_for_t3Table.GetLength(1); c++)
                {
                    row[c] = arr_for_t3Table[r, c];
                }
                t3Providers[r] = StringToProviders(row, t3Table);
            }

            string[,] arr = new string[lines.Length, lines[0].Split(',').Length];
            for (int r = 0; r < lines.Length; r++)
            {
                string[] line_r = lines[r].Split(',');
                for (int c = 0; c < lines[0].Split(',').Length; c++)
                {
                    arr[r, c] = line_r[c];
                }
            }
            return arr;
        }

         static Provider[] TableToEntities(string[,] csvTable) {
            Provider[] provider = new Provider[csvTable.GetLength(0)]; 
            for (int r = 0; r < csvTable.GetLength(0)-1; r++)
            {
                string[] row = new string[csvTable.GetLength(1)];
                for (int c = 0; c < csvTable.GetLength(1); c++)
                {
                    row[c] = csvTable[r+1, c];
                }
                provider[r] = StringToProviders(row , csvTable);
            }
            return provider;
        }
        
        static Provider StringToProviders(string[] row, string[,] csvTable)
        {
            if (row.Length < csvTable.GetLength(1))
            {
                return new Provider();
            }
            int id = int.Parse(row[0]);
            string name = row[1];
            string ip = row[2];
            int speed = int.Parse(row[3]);
            Provider provider = new Provider();
            provider.id = id;
            provider.name = name;
            provider.ip = ip;
            provider.speed=speed;
            return provider;
        }


        static void CSVLoad(string entered)
        {
            string path = "./data.csv";
            t3CsvText = ReadAllText(path);
            t3Table = CsvToTable(t3CsvText);
            
        }

        static void CSVText()
        {
            Console.WriteLine(t3CsvText);
        }


        static void CSVTable(string entered, string[] input)
        {
            for(int i = 0; i < t3Table.GetLength(0); i++)
            {
                Console.Write("|");
                for (int j = 0; j < t3Table.GetLength(1); j++)
                {
                   Console.Write("{0, 5} |", t3Table[i, j].PadRight(13));
                }
                Console.WriteLine();
            }
        }

        static void CSVEntities(string entered, string[] input)
        {        
            foreach (Provider it in t3Providers)
            {
                Console.WriteLine("\r~~>Provider: {0} \r\n    |Id: {1}\r\n    |Ip: {2}\r\n    |Speed: {3}", it.name, it.id, it.ip, it.speed);
                Console.WriteLine();
            }

        }
        static void CSVGetIndex(string entered, string[] input)
        {
              if(input.Length != 3)
            {
                return;
            }
            int index = int.Parse(input[2]);
            Provider in_provider = t3Providers[index];
            Console.WriteLine("\r~~>Provider: {0} \r\n    |Id: {1}\r\n    |Ip: {2}\r\n    |Speed: {3}", in_provider.name, in_provider.id, in_provider.ip, in_provider.speed);
        }
        
        static string[,] ProvidersToTable(Provider[] providers, string[,] table) 
        {
             for (int i = 0; i < table.GetLength(0) - 1; i++)
            {
                table[i+1, 0] = Convert.ToString(providers[i].id);
                table[i+1, 1] = providers[i].name;
                table[i+1, 2] = providers[i].ip;
                table[i+1, 3] = Convert.ToString(providers[i].speed);
            }
            return table;
        }
         static void ProviderToTable()
         {
             t3Table = ProvidersToTable(t3Providers, t3Table);
         }
        
        static string TableToString(string[,] table)
        {
            string text;
            string[] row_before_join = new string[table.GetLength(1)];
            
            string[] row = new string[table.GetLength(0)];
            for (int item = 0; item < table.GetLength(0); item++)
            {
                row_before_join[0] = table[item, 0];
                row_before_join[1] = table[item, 1];
                row_before_join[2] = table[item, 2];
                row_before_join[3] = table[item, 3];

                row[item] = String.Join(",", row_before_join);
            }
                text = String.Join("\r\n",row);
            return text;
        }
        
        static void CSVSetIndex(string entered, string[] input)
        {
            if(input.Length != 5)
            {
                return;
            }
            int index = int.Parse(input[2]);
            switch(input[3])
            {
                case "id":
                t3Providers[index].id = int.Parse(input[4]);
                break;

                case "name":
                t3Providers[index].name = input[4];
                break;

                case "ip":
                t3Providers[index].ip = input[4];
                break;

                case "speed":
                t3Providers[index].speed = int.Parse(input[4]);                
                break;
            }

            string[,] table = ProvidersToTable(t3Providers, t3Table);
            t3CsvText = TableToString(table);
        }
        static void CSVSave()
        {
            WriteAllText("./data.csv", t3CsvText);
            Console.WriteLine("Your data has been saved.");
        }
                
        static void Main()
        {
            while(true)
            {
                Console.WriteLine("Write command: ");
                string command = Console.ReadLine();
                Console.WriteLine("~{0}~", command);
                // string command = "csv.set.1.name.hui";
                string[] subcommand = command.Split('.');
                switch(subcommand[0])
                {
                    case "char":
                        ProcessChar(subcommand);
                        continue;   
                    case "string":
                        ProcessString(subcommand);
                        continue;
                    case "csv":
                        ProcessCSV(subcommand);
                        continue;
                    case "exit":
                        Console.WriteLine("Goodbye!");
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Error, unknown command! Your command is:{0}",command);
                        break;
                }
            }
        }
    }
}