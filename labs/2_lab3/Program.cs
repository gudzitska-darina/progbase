using static System.Console;

namespace _2_lab3
{
    class Program
    {
        static void Main(string[] args)
        {
            ISetInt set = new ArraySet();
            ILogger ilog = null;
            if(args.Length == 0)
            {
                ConsoleLogger logger = new ConsoleLogger();
                CommandUserInterface.ProcessSets(set, logger);
                return;
            }
            else if(args.Length > 3)
            {
                WriteLine("   Argument required");
                return;
            }

            if(args[0] == "console")
            {
                ConsoleLogger logger = new ConsoleLogger();
                ilog = logger;
            }
            else if(args[0] == "file")
            {
                if(args.Length != 3)
                {
                     WriteLine("   Enter a correct number of files");
                     return;
                }
                CsvFileLogger2 filelogger = new CsvFileLogger2();
                ilog = filelogger;
                filelogger.process = args[1];
                filelogger.error = args[2];
            }
            CommandUserInterface.ProcessSets(set, ilog);
            
        }
    }
}
