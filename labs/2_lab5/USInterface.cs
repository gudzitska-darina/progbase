using System;
using static System.Console;
using System.Collections.Generic;


public class USInterface
{
    public static void ProcessSets()
    {
        LibCourse courses = new LibCourse();
        Serializer ser = new Serializer();
        DataProcessing inter = new DataProcessing();
        while(true)
        {
            WriteLine("\nEnter command");
            string command = ReadLine();
            if(command == "exit")
            {
                WriteLine("Goodbye");
                break;
            }
            string[] sub = command.Split(' ');
            if(sub.Length == 2)
            {
                if(sub[0] == "load")
                {
                    courses = ser.Deserialize(sub[1]);
                    WriteLine("Deserialization success.");
                }
                else if(sub[0] == "print")
                {
                    int num;
                    if(Int32.TryParse(sub[1], out num))
                    {
                        WriteLine($"All pages: {inter.GetTotalPages(courses)}");
                        WriteLine("\r\nData:");
                        inter.GetPage(Int32.Parse(sub[1]), courses);
                    }
                    else 
                    {
                        WriteLine($"\tEnter incorrect value for page: {sub[1]})");
                    }   
                    
                }
                else if(sub[0] == "save")
                {
                    ser.Serialize(sub[1], courses);
                    WriteLine("Saving success.");
                }
                else if(sub[0] == "subject")
                {
                    WriteLine($"List of titles for {sub[1]}:");
                    HashSet<string> listTitle = inter.ListSubj(courses, sub[1]);
                    int i = 1;
                    foreach(string item in listTitle)
                    {
                        WriteLine($"{i}. {item}");
                        i++;
                    }
                }
                else if(sub[0] == "image")
                {
                    var subject = inter.UniqSubj(courses);
                    ImageGen img = new ImageGen();
                    img.GenerationImg(subject, courses, sub[1]);
                    WriteLine("Creation image success.");

                }
                else
                {
                    WriteLine($"Incorrect input {sub[0]}");
                }
            }
            else if(sub.Length == 3)
            {
                if(sub[0] == "export")
                {
                    int num;
                    if(Int32.TryParse(sub[1], out num))
                    {
                        LibCourse units = inter.MaxUnit(Int32.Parse(sub[1]), courses);
                        ser.Serialize(sub[2], units);
                        WriteLine("Export success.");
                    }
                    else 
                    {
                        WriteLine($"\tEnter incorrect value for max value: {sub[1]})");
                    } 

                }
                else
                {
                    WriteLine($"Incorrect input {sub[0]}");
                }
            }
            else if(sub.Length == 1)
            {
                if(sub[0] == "subjects")
                {
                    WriteLine("List of unique subject:");
                    int i = 1;
                    var subject = inter.UniqSubj(courses);
                    foreach(Course item in subject)
                    {
                        WriteLine($"{i}. {item.subj}");
                        i++;
                    }
                }
                else if(sub[0] == "instructors")
                {
                    WriteLine("List of unique subject:");
                    int i = 1;
                    var subject = inter.UniqInstr(courses);
                    foreach(Course item in subject)
                    {
                        WriteLine($"{i}. {item.instructor}");
                        i++;
                    }

                }
                else
                {
                    WriteLine($"Incorrect input {sub[0]}");
                }
            }
            else
            {
                WriteLine("Incorrect number of argm");
            } 
            
        }
        
    }
}
   