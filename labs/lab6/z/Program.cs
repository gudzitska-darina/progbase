using System;
using System.Diagnostics;
using System.IO;

namespace z
{
     enum State
    {
        Initial,
        BeforeQuotes,
        AfterQuotes,
        Symbol,
    }
    struct Options
    {
        public bool isInteractiveMode;  // for -i boolean option
        public string inputFile;  // for the independent option
        public string outputFile;  // for -o value option
        // for errors
        public string parsingError;
    }
    class Program
    {
        static bool CheckLiteralQuotes(string input)
        {
            bool check = true;
            State state = State.Initial;
            if (CountLiteralQuotes(input) != 1)
            {
                check = false;
            }
            else
            {
                for (int i = 0; i < input.Length; i++)
                {
                    char c = input[i];
                    if (state == State.Initial)
                    {
                        if (c == '"')
                        {
                            state = State.BeforeQuotes;
                        }
                        else
                        {
                            check = false;
                        }
                    }
                    else if (state == State.BeforeQuotes)
                    {
                        if (c == '"')
                        {
                            check = false;
                        }
                        else
                        {
                            state = State.AfterQuotes;
                        }
                    }
                    else if (state == State.AfterQuotes)
                    {
                        if (c == '"')
                        {
                            state = State.Symbol;
                        }
                    }
                    else if (state == State.Symbol)
                    {
                        if (c == 0)
                        {
                            check = true;
                        }
                        else
                        {
                            check = false;
                        }
                    }
                } 
            }
            return check;
        }

        static int CountLiteralQuotes(string input)
        {
            int counter = 0;
            State state = State.Initial;
            for (int i = 0; i < input.Length; i++)
            {
                char c = input[i];
                if (state == State.Initial)
                {
                    if (c == '"')
                    {
                        state = State.BeforeQuotes;
                    }
                    else 
                    {
                        state = State.Initial;
                    }
                }
                else if (state == State.BeforeQuotes)
                {
                    if (c == '"')
                    {
                        state = State.Initial;
                    }
                    else 
                    {
                        state = State.AfterQuotes;
                    }
                }
                else if (state == State.AfterQuotes)
                {
                    if(c == '"')
                    {
                        state = State.Symbol;
                        counter++;
                    }
                }
                else if (state == State.Symbol)
                {
                    if (c == '"')
                    {
                        state = State.BeforeQuotes;
                    }
                    else
                    {
                        state = State.Initial;
                    }
                } 

            }           
            return counter;
        }

        static string[] GetAllLiteralQuotes(string input)
        {
            string[] arr = new string[1000];
            State state = State.Initial;
            int i = 0;
            foreach(char c in input)
            {
                if (state == State.Initial)
                {
                    if (c == '"')
                    {
                        state = State.BeforeQuotes;
                        arr[i] += c;
                    }
                    else 
                    {
                        state = State.Initial;
                    }

                }
                else if (state == State.BeforeQuotes)
                {
                    if (c == '"')
                    {
                        state = State.Initial;
                    }
                    else 
                    {
                        state = State.AfterQuotes;
                        arr[i] += c;
                    }
                }
                else if (state == State.AfterQuotes)
                {
                    if (c != '"')
                    {
                        arr[i] += c;
                    }
                    else if(c == '"')
                    {
                        state = State.Symbol;
                        arr[i] += c;
                        i++;
                    }
                }
                else if (state == State.Symbol)
                {
                    if (c == '"')
                    {
                        state = State.BeforeQuotes;
                    }
                    else
                    {
                        state = State.Initial;
                    }
                } 
            }
            string[] literal = new string[i];
            for(int j = 0; j < i; j++)
            {
                literal[j] = arr[j];
            }
            return literal;
        }

        static void Print(string[] value)
        {
            for(int i = 0; i < value.Length; i++)
            {
                if(i == value.Length - 1)
                {
                    Console.Write("{0}.", value[i]);
                }
                else
                {
                    Console.Write("{0}, ", value[i]);
                }
            }
            Console.WriteLine();
        }

        static bool Contain(bool[] arr, bool value)
        {
            for(int i=0; i<arr.Length; i++)
                {
                    if(arr[i] == value)
                    {
                        return true;
                    }
                }
            return false;
        }
        static Options ParseOptions(string[] args) 
        { 
            Options options = new Options()
            {
                inputFile = "",
                outputFile= "",
                parsingError = "",
            };
          
          bool[] isParsed = new bool[args.Length];
            while(true)
            {
                if(!Contain(isParsed, false))
                {
                    break;
                }

                for(int i = 0; i<args.Length; i++)
                {
                    if(isParsed[i]==true)
                    {
                        continue;
                    }

                    string argum = args[i];
                    if(argum == "-i")
                    {
                        options.isInteractiveMode = true;
                        isParsed[i]= true;
                    }
                    else if(argum == "-o")
                    {
                        if(i == args.Length-1)
                        {
                            options.parsingError = "Outputfile not specified!";
                            return options;
                        }
                        string next = args[i+1];
                        if(next.StartsWith('-'))
                        {
                            options.parsingError = "Incorrect arrangement of arguments!";
                            return options;
                        }
                        options.outputFile = next;
                        isParsed[i] = true;
                        isParsed[i+1] = true;
                    }
                    else if(argum.StartsWith('-'))
                    {
                        options.parsingError = "Unknown option!";
                        return options;
                    }
                    else
                    {
                        if(options.inputFile!="")
                        {
                            options.parsingError = "Duplicate option!";
                            return options;
                        }
                        options.inputFile = argum;
                        isParsed[i]=true; 
                    }

                }
            }            
            
            return options;
        } 

        static bool Compare(in Options o1, in Options o2)
        {
            return o1.isInteractiveMode == o2.isInteractiveMode
                && o1.inputFile == o2.inputFile
                && o1.outputFile == o2.outputFile
                && o1.parsingError == o2.parsingError;
        }

        static void RunTest()
        {
            Debug.Assert(CountLiteralQuotes("\"Great Point\"") == 1);
            Debug.Assert(CountLiteralQuotes("dyrd\"") == 0);
            Debug.Assert(CountLiteralQuotes("\"Coffee") == 0);
            Debug.Assert(CountLiteralQuotes("") == 0);
            Debug.Assert(CountLiteralQuotes("\"jghvkh\"AQchN\"GWTFDF\"\"") == 2);
            Debug.Assert(CountLiteralQuotes("\"\"") == 0);

            Debug.Assert(CheckLiteralQuotes("") == false);
            Debug.Assert(CheckLiteralQuotes("\"Test is Successful\"") == true);
            Debug.Assert(CheckLiteralQuotes("\"hvjhh\"Terraria\"op\"") == false);
            Debug.Assert(CheckLiteralQuotes("\"whynot") == false);
            Debug.Assert(CheckLiteralQuotes("ytfdsy\"lkpiogr\"") == false);
           

            Debug.Assert(Compare(ParseOptions(new string[]{}),new Options(){
                isInteractiveMode = false,
                inputFile = "",
                outputFile= "",
                parsingError = "",
                }));
            Debug.Assert(Compare(ParseOptions(new string[]{"-i"}),new Options(){
                isInteractiveMode = true,
                inputFile = "",
                outputFile= "",
                parsingError = "",
                }));
             Debug.Assert(Compare(ParseOptions(new string[]{"-i","-i"}),new Options(){
                isInteractiveMode = true,
                inputFile = "",
                outputFile= "",
                parsingError = "",
                }));
            Debug.Assert(Compare(ParseOptions(new string[]{"-o","arg1","-o", "arg2"}),new Options(){
                isInteractiveMode = false,
                inputFile = "",
                outputFile= "arg2",
                parsingError = "",
                }));
            Debug.Assert(Compare(ParseOptions(new string[]{"-o", "file.txt"}),new Options(){
                isInteractiveMode = false,
                inputFile = "",
                outputFile= "file.txt",
                parsingError = "",
                }));
            Debug.Assert(Compare(ParseOptions(new string[]{"-i", "file.txt", "-o", "text"}),new Options(){
                isInteractiveMode = true,
                inputFile = "file.txt",
                outputFile= "text",
                parsingError = "",
                }));
            Debug.Assert(Compare(ParseOptions(new string[]{"-o", "text", "file.txt", "-i"}),new Options(){
                isInteractiveMode = true,
                inputFile = "file.txt",
                outputFile= "text",
                parsingError = "",
                }));
            Debug.Assert(Compare(ParseOptions(new string[]{"-i", "-o", "text","file.txt"}),new Options(){
                isInteractiveMode = true,
                inputFile = "file.txt",
                outputFile= "text",
                parsingError = "",
                }));
            Debug.Assert(ParseOptions(new string[]{"-o"}).parsingError != "");
            Debug.Assert(ParseOptions(new string[]{"-o", "-i"}).parsingError != "");
            Debug.Assert(ParseOptions(new string[]{"-N"}).parsingError != "");
            Debug.Assert(ParseOptions(new string[]{"input", "input2"}).parsingError != "");
        }
        static void Main(string[] args)
        {
            RunTest();
            Options options = ParseOptions(args);
            if(options.parsingError!="")
            {
                Console.WriteLine($"Error: {options.parsingError}");
                Environment.Exit(1);
            }
            Console.WriteLine("Command Line Arguments ({0}):", args.Length);
            for (int i = 0; i < args.Length; i++)
            {
                Console.WriteLine("[{0}] \"{1}\"", i, args[i]);
            }

            
            if (options.isInteractiveMode == true)
            {
                Console.WriteLine("Press Escape to quit");
                string str = "";
                ConsoleKeyInfo keyinfo;
                do
                {
                    Console.Write("Enter command: ");
                    str = Console.ReadLine();
                    if (str == "")
                    {
                        Console.WriteLine("You have not entered anything!!!!");
                        break;
                    }
                    Console.WriteLine("1. {0}", CheckLiteralQuotes(str));
                    Console.WriteLine("2. {0}", CountLiteralQuotes(str));
                    string[] lit;
                    lit = GetAllLiteralQuotes(str);
                    Console.Write("3. Array: ");
                    Print(lit);

                    keyinfo =  Console.ReadKey();
                } 
                while (keyinfo.Key != ConsoleKey.Escape);
            }
            else if (options.inputFile != "" && options.outputFile == "")
            {
                string text = File.ReadAllText(options.inputFile);
                string[] arr = GetAllLiteralQuotes(text);
                for (int i = 0; i < arr.Length; i++)
                {
                    Console.WriteLine("{0}", arr[i]);
                }
            }
             else if (options.inputFile != "" && options.outputFile != null)
            {
                string text = File.ReadAllText(options.inputFile);
                string[] arr = GetAllLiteralQuotes(text);
                string singlestring = "";
                for (int i = 0; i < arr.Length; i++)
                {
                    if(i != arr.Length - 1)
                    {
                        singlestring = singlestring + arr[i] + "\n";
                    }
                    else
                    {
                        singlestring = singlestring + arr[i];
                    }
                }
                File.WriteAllText(options.outputFile, singlestring);
            }
            else if (options.inputFile == "")
            {
                Console.WriteLine("Error: No input!");
            }

            Console.WriteLine("Ok");
        }
    }
}
 // ./bin/Debug/net5.0/z.exe "in.txt" "-o" "out.txt"