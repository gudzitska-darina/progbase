using System;
using System.Text;
using static System.IO.File;
using static System.Console;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace z2
{
    class Provider
    {
        public int id;
        public string nameProvider;
        public int speed;
        public string nameClient;
        public Provider()
        {
            id = 0;
            nameProvider = "";
            speed = 0;
            nameClient = "";
        }
        public Provider(int id, string nameProvider, int speed, string nameClient)
        {
            this.id = id;
            this.nameProvider = nameProvider;
            this.speed = speed;
            this.nameClient = nameClient;
        }
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
        public int GetCount()
        {
            return _size;
        }
        public void Add(Provider newMobileOperator)
        {
            int i = 0;
            if (_size == 0)
            {
                _items[0] = newMobileOperator;
                _size++;
                return;
            }
            while (true)
            {
                if (i == _items.Length - 1)
                {
                    Array.Resize(ref _items, _items.Length * 2);
                }
                if (i == _size)
                {
                    _items[i] = newMobileOperator;
                    _size++;
                    break;
                }
                i++;
            }
        }

        public IEnumerator<Provider> GetEnumerator() 
        {
            return this._items.Take(this._size).GetEnumerator();
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= _items.Length)
            {
                throw new IndexOutOfRangeException();
            }
            if (_size == _items.Length)
            {
                Array.Resize(ref _items, _items.Length * 2);
            }
            while (true)
            {
                _items[index - 1] = _items[index];
                if (_items[index] == null)
                {
                    _size--;
                    return;
                }
                index++;
            }
        }
         public bool Remove(Provider provider)
        {
            if (_size == _items.Length)
            {
                Array.Resize(ref _items, _items.Length * 2);
            }
            for (int i = 0; i < _size; i++)
            {
                if (_items[i].id == provider.id && _items[i].nameProvider == provider.nameProvider && _items[i].speed == provider.speed && _items[i].nameClient == provider.nameClient)
                {
                    for (int j = i + 1; j <= _size; j++)
                    {
                        _items[i] = _items[j];
                        i++;
                    }
                    _size--;
                    return true;
                }
            }
            return false;
        }

        public static void Print(ListProvider providers)
        {
            foreach(Provider prov in providers)
            {   
                if(prov.id <= 10)
                    WriteLine($"\t{prov}");
            }
        }

        public ListProvider Reader(ListProvider providers,string filePath)
        {
            StreamReader sr = new StreamReader(filePath);
            string provider = "";
            while (true)
            {
                provider = sr.ReadLine();
                if(provider == null)
                {
                    break;
                }

                string[] part = provider.Split(',');
                if(part.Length != 4)
                {
                    throw new Exception("Not enought elements in string.");
                }
            
                Provider pr = new Provider();
                pr.id  = int.Parse(part[0]);
                pr.nameProvider = part[1];
                pr.speed = int.Parse(part[2]);
                pr.nameClient = part[3];

                Add(pr);
            }
            sr.Close();
            return providers;
        }
        public ListProvider Reader2(ListProvider providers,string filePath)
        {
            StreamReader sr = new StreamReader(filePath);
            string provider = "";
            while (true)
            {
                provider = sr.ReadLine();
                if(provider == null)
                {
                    break;
                }

                string[] part = provider.Split(',');
                if(part.Length != 4)
                {
                    throw new Exception("Not enought elements in string.");
                }
            
                Provider pr = new Provider();
                pr.id  = int.Parse(part[0]) + 17;
                pr.nameProvider = part[1];
                pr.speed = int.Parse(part[2]);
                pr.nameClient = part[3];

                Add(pr);
            }
            sr.Close();
            return providers;
        }
        public static ListProvider ReadAllProviders(string filePath)
        {
            ListProvider pr1 = new ListProvider();
            return pr1.Reader(pr1, filePath);
        }

        public static ListProvider ReadAllProvidersFrom2(string filePath1,string filePath2)
        {
            ListProvider prAll = new ListProvider();
            prAll.Reader(prAll,filePath1);
            return prAll.Reader2(prAll, filePath2);;
        }

        public static int AverageSpeed(ListProvider providers)
        {
           int count = providers.GetCount();
           int sum = 0;
           foreach(Provider prov in providers)
            {   
               sum += prov.speed;
            }
            return (int)Math.Ceiling(sum / (double)count);
        }
        public static ListProvider RemoveProviders(int serSpeed, ListProvider providers)
        {
            int i = 0;
            foreach(Provider prov in providers)
            {   
                if(prov == null)
                    return providers;
                if(prov.speed < serSpeed)
                {
                   providers.RemoveAt(prov.id - i);
                   i++;
                }
                
            }
            return providers;
        }
        public static void WriteAllProviders(string filePath, ListProvider providers)
        {
            StreamWriter write = new StreamWriter(filePath);
                foreach(Provider prov in providers)
                {  
                    string csvPr = ConvertToCsv(prov);
                    write.WriteLine(csvPr);
                }
            write.Close();
        } 
        static string ConvertToCsv(Provider pr)
        {
            return $"{pr.id},{pr.nameProvider},{pr.speed},{pr.nameClient}";
        }         
    }    

    class Program
    {
        static void Main(string[] args)
        {
            string file1 = "./data1.csv";
            string file2 = "./data2.csv";
            string file3 = "./all.csv";
            
            ListProvider prov1 = ListProvider.ReadAllProviders(file1);
            ListProvider prov2 = ListProvider.ReadAllProviders(file2);
            WriteLine($"\nFirst 10 elements in {file1}");
            ListProvider.Print(prov1);
            WriteLine($"\nFirst 10 elements in {file2}");
            ListProvider.Print(prov2);

            ListProvider prAll = ListProvider.ReadAllProvidersFrom2(file1,file2);
            WriteLine(ListProvider.AverageSpeed(prov1));
            ListProvider provS = ListProvider.RemoveProviders(ListProvider.AverageSpeed(prAll), prAll);
            //ListProvider.Print(prov1);
            WriteLine("Success delete");

            ListProvider.WriteAllProviders(file3,provS);
            WriteLine("Success write");
        }
    }
}
