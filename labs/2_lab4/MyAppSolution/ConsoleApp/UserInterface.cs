using System;
using System.IO;
using ProgbaseLab.ImageEditor.Common;
using ProgbaseLab.ImageEditor.Pixel;
using ProgbaseLab.ImageEditor.Fast;
using System.Drawing;
using System.Diagnostics;
namespace ConsoleApp
{
    public class UserInterface
    {
        public static void ProcessSets(string[] command)
        {
            Stopwatch stopwatchAll = new Stopwatch();
            Stopwatch stopwatchMethod = new Stopwatch();
            stopwatchAll.Start();

            TextWriter errorWriter = Console.Error;
            Bitmap bitmap = new Bitmap(command[1]);
            IRunnable pixel = new ClassPixel();
            IRunnable fast = new ClassFast();
            if(command[0] == "pixel")
            {
                if(command.Length == 4)
                {
                    if(command[3] == "flip")
                    {
                        stopwatchMethod.Start();
                        pixel.FlipVertical(bitmap).Save(command[2]);
                        stopwatchMethod.Stop();
                    }
                    else if(command[3] == "remove")
                    {
                        stopwatchMethod.Start();
                        pixel.RemoveRed(bitmap).Save(command[2]);
                        stopwatchMethod.Stop();
                    }
                    else if(command[3] == "grayscale")
                    {
                        stopwatchMethod.Start();
                        pixel.Grayscale(bitmap).Save(command[2]);
                        stopwatchMethod.Stop();
                    }
                    else
                    {
                        errorWriter.WriteLine($"\tEnter incorrect method:{command[3]}");
                        Environment.Exit(1);
                    }
                }
                else if(command.Length == 5)
                {
                    if(command[3] == "blur")
                    {
                        int num;
                        if(Int32.TryParse(command[4], out num))
                        {
                            stopwatchMethod.Start();
                            pixel.Blur(bitmap, Int32.Parse(command[4])).Save(command[2]);
                            stopwatchMethod.Stop();
                        }
                        else 
                        {
                            errorWriter.WriteLine($"\tEnter incorrect value for sigma: {command[4]})");
                            Environment.Exit(1);
                        }   
                    }
                    else
                    {
                        errorWriter.WriteLine($"\tEnter incorrect method: {command[3]}, {command[4]}");
                        Environment.Exit(1);
                    }
                }
                else
                {
                    errorWriter.WriteLine("\tEnter  incorrect command");
                    Environment.Exit(1);
                }
            }
            else if(command[0] == "fast")
            {
                if(command.Length == 4)
                {
                    if(command[3] == "flip")
                    {
                        stopwatchMethod.Start();
                        fast.FlipVertical(bitmap).Save(command[2]);
                        stopwatchMethod.Stop();
                    }
                    else if(command[3] == "remove")
                    {
                        stopwatchMethod.Start();
                        fast.RemoveRed(bitmap).Save(command[2]);
                        stopwatchMethod.Stop();
                    }
                    else if(command[3] == "grayscale")
                    {
                        stopwatchMethod.Start();
                        fast.Grayscale(bitmap).Save(command[2]);
                        stopwatchMethod.Stop();
                    }
                    else
                    {
                        errorWriter.WriteLine($"\tEnter incorrect method:{command[3]}");
                        Environment.Exit(1);
                    }
                }
               else if(command.Length == 5)
                {
                    if(command[3] == "blur")
                    {
                        int num;
                        if(Int32.TryParse(command[4], out num))
                        {
                            stopwatchMethod.Start();
                            fast.Blur(bitmap, Int32.Parse(command[4])).Save(command[2]);
                            stopwatchMethod.Stop();
                        }
                        else 
                        {
                            errorWriter.WriteLine($"\tEnter incorrect value for sigma: {command[4]})");
                            Environment.Exit(1);
                        }   
                    }
                    else
                    {
                        errorWriter.WriteLine($"\tEnter incorrect method: {command[3]}, {command[4]}");
                        Environment.Exit(1);
                    }
                }
                else
                {
                    errorWriter.WriteLine("\tEnter  incorrect command");
                    Environment.Exit(1);
                }
            }
            else
            {
                errorWriter.WriteLine($"\tEnter  incorrect modul: {command[0]}");
                Environment.Exit(1); 
            }
            stopwatchAll.Stop();
            Console.WriteLine($"\tChosen method: {stopwatchMethod.Elapsed}");
            Console.WriteLine($"\tAll program: {stopwatchAll.Elapsed}");
        }
    }
}