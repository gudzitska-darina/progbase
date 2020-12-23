using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace labaDarina
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] mountains = new int[int.Parse(Console.ReadLine())];
            int[] mountains1 = new int[mountains.Length];

            Random random = new Random();
            for (int i = 0; i < mountains.Length; i++)
            {
                mountains[i] = random.Next(-10, 10);
                Console.Write("{0,2} ", mountains[i]);
            }
            Console.WriteLine();
            int min = Math.Abs(mountains.Min());
            for (int i = 0; i < mountains.Length; i++)
            {
                mountains[i] += min;
                Console.Write("{0,2} ", mountains[i]);
            }
            Console.WriteLine();
            int waterLevel = int.Parse(Console.ReadLine());
            
            Print(mountains, mountains1, waterLevel);
            
            int vol=CountUnderwaterLandVolume(mountains1, waterLevel);
            Console.WriteLine("Обьем самой большой горы: {0}",vol);
        }

        static int CountUnderwaterLandVolume(int[] heights, int waterLevel) 
        { 
            int vol=0;
            int counter=0;
            int[] changes = new int[heights.Length]; 
            for (int i = 0; i < heights.Length; i++)
            {
                if (heights[i]!= 0) 
                {
                    counter += heights[i];
                    changes[i]=counter;
                }

                else if (heights[i] == 0)
                {
                    counter=0;
                } 
            }            
            vol = Math.Abs(changes.Max());
            return vol;
        } 

        static void Print(int[] mountains, int[] mountains1, int waterLevel)
        {
            for (int i = 0; i < mountains.Length; i++)
            {
                if (mountains[i] - waterLevel < 0)
                    mountains1[i] = 0;
                else
                    mountains1[i] = mountains[i] - waterLevel;
                Console.Write("{0} ", mountains1[i]);
            }
            char[,] mount = new char[mountains.Max(),mountains.Length];
            for (int i = 0; i < mount.GetLength(0); i++)
            {
                for (int j = 0; j < mount.GetLength(1); j++)
                {
                    if (mountains[j] >= i)
                        mount[i, j] = 'N';
                    else
                        mount[i, j] = (i < waterLevel) ? '~' : ' ';

                }
            }
            Console.WriteLine();
            for (int i = mount.GetLength(0) - 1; i > 0; i--)
            {
                for (int j = 0; j < mount.GetLength(1); j++)
                {
                    Console.Write(mount[i, j]);
                }
                Console.WriteLine();
            }
        }
    }
}
