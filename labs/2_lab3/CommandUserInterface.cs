using static System.Console;
using System.Text;

public class CommandUserInterface
{
    public static void ProcessSets(ISetInt main, ILogger logger)
    {
        ISetInt alfa = new ArraySet();
        ISetInt beta = new ArraySet();
        while(true)
        {
            WriteLine("\nEnter command");
            string command = ReadLine();
            if(command == "exit")
            {
                logger.Log("Goodbye");
                break;
            }
            string[] sub = command.Split(' ');
            int n;
            if(sub.Length == 1)
            {
                if(sub[0] == "overlaps") logger.Log(alfa.Overlaps(beta).ToString());
                else if(sub[0] == "symmetricExceptWith") {beta.SymmetricExceptWith(alfa); logger.Log("Successful set change");}
                else{logger.LogError($"Incorrect additional method: {sub[0]}"); continue;}
            }
            else if(sub.Length <= 3)
            {
                if(sub[0] == "alfa") main = alfa;
                else if(sub[0] == "beta") main = beta;
                else{logger.LogError($"Invalid name for set: {sub[0]}"); continue;}

                if(sub.Length > 2 && !int.TryParse(sub[2], out n))
                {
                    logger.LogError($"Invalid value for set: {sub[2]}");
                    continue;
                }

                if(sub[1] == "add")
                {
                    logger.Log(main.Add(int.Parse(sub[2])).ToString());
                }
                else if(sub[1] == "remove")
                {
                    logger.Log(main.Remove(int.Parse(sub[2])).ToString());
                }
                else if(sub[1] == "contains")
                {
                    logger.Log(main.Contains(int.Parse(sub[2])).ToString());
                }
                else if(sub[1] == "count")
                {
                    logger.Log(main.Count.ToString());
                }
                else if(sub[1] == "clear")
                {
                    main.Clear();
                    logger.Log("Successful clear");
                }         
                else if(sub[1] == "log")
                {
                    if(main.Count == 0)
                    {
                        logger.Log("Set is empty");
                        continue;
                    }
                    StringBuilder sb = new StringBuilder();
                    int[] mass = new int[30];
                    main.CopyTo(mass);
                    for(int i = 0; i < mass.Length - 1; i++)
                    {
                        if(mass[i] == 0)
                        {
                            continue;
                        }
                        sb.Append($"{mass[i]} ");
                    }
                    logger.Log(sb.ToString());
                }
                else if(sub[1] == "read")
                {
                    main.ReadSet(sub[2]);
                    logger.Log("Successful reading");
                }
                    else if(sub[1] == "write")
                {
                    main.WriteSet(sub[2], main);
                    logger.Log("Successful writing");
                }
                else
                {
                    logger.LogError($"Nonexistent command: {sub[1]}");
                }
            }
            else if(sub.Length > 3)
            {
                StringBuilder sb = new StringBuilder();
                for(int i = 0; i < sub.Length - 1; i++)
                {
                    sb.Append($"{sub[i]} ");
                }
                logger.LogError($"Incorrect command: {sb.ToString()}");
            }
        }
    }
}
