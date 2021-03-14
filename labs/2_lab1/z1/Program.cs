using System;
using System.Text;
using static System.IO.File;
using static System.Console;

namespace z1
{
    public class Program
    {
       static void ProcessString(string f, int r)
       {
           Random rand = new Random();          
            string[] nameProvider = {"Lanet", "Укртелеком","Triolan", "Vega Telecom", "ФРИНЕТ", "Optinet", "WestNet", "IT-Invest", "Mixnet", "UTELS"};
            string[] nameClient = {"Алексей","Владимир","Диана", "Евгения", "Кирилл","Александр","Софья","Анастасия","Виктор"};
            string[] surnameClient = {"Авраменко","Жуковский","Любимов", "Митрофанов", "Цой","Шумахер","Дюма","Ефремов"};
            string newLine = Environment.NewLine;

            StringBuilder provider = new StringBuilder();
            int i = 1;
            while(i <= r)
            {
                provider.Append(i.ToString()).Append(",")
                        .Append(nameProvider[rand.Next(0, nameProvider.Length)].ToString()).Append(",")
                        .Append(rand.Next(0, 150).ToString()).Append(",")
                        .Append(nameClient[rand.Next(0, nameClient.Length)].ToString()).Append(" ").Append(surnameClient[rand.Next(0, surnameClient.Length)].ToString())
                        .AppendLine();
                i++;
            }
            AppendAllText(f, provider.ToString());
            WriteLine($"Success!");
       }
        static void Main(string[] args)
        {
            WriteLine($"Enter output file and number of string.");
            string command = ReadLine();
            if(command.StartsWith("./"))
            {
                string[] parts = command.Split(' ');
                if(parts.Length != 2)
                {
                    throw new Exception("You miss sth!");
                }
                string file = parts[0];
                int nRows = int.Parse(parts[1]);
                if(nRows <= 0)
                {
                    throw new Exception("Number of strings cannot be 0 or negative!");
                }
                ProcessString(file, nRows);
            }
            else
            {
                throw new Exception("Enter the complete path to the file");
            }
            
        }
    }
}