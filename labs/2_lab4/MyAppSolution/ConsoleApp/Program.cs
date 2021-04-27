using System;
using System.IO;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] command = new string[args.Length];
            if(args.Length < 4 || args.Length > 5)
            {
                TextWriter errorWriter = Console.Error;
                errorWriter.WriteLine("\tEnter incorrect arguments");
                Environment.Exit(1);
            }
            else if(args.Length == 4 || args.Length == 5)
            {
                command = args;
            }
            UserInterface.ProcessSets(command);
            
        }
    }
}
