using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace z2
{
    class Program
    {

        static void Main()
        {
            int[,] mass = new int[6, 6];
            Random rand = new Random();
            for (int i = 0; i < mass.GetLength(0) - 1; i++)
            {
                for (int j = 0; j < mass.GetLength(1) - 1; j++)
                {
                    mass[i, j] = rand.Next(0, 2);
                    Console.Write("{0} ", mass[i, j]);
                }
                Console.WriteLine();
            }


            int counter;
            InvertMatrix(mass);
            Console.WriteLine("".PadLeft(mass.GetLength(0), '='));
            EnumerateOnes(mass, out counter);
            Console.WriteLine("".PadLeft(mass.GetLength(0), '='));
            int length = counter;
            CreateCounters(length);
            MassSwap(mass);
            MergeOnes(mass, length);
            Print(mass);            


        }

        static void InvertMatrix(int[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0) - 1; i++)
            {
                for (int j = 0; j < matrix.GetLength(1) - 1; j++)
                {
                    if (matrix[i, j] == 1)
                    {

                        matrix[i, j] = 0;
                    }
                    else if (matrix[i, j] == 0)
                    {
                        matrix[i, j] = 1;
                    }
                    //Console.Write("{0} ", matrix[i, j]);
                }
                //Console.WriteLine();
            }
        }

        static int EnumerateOnes(int[,] matrix, out int counter)
        {
            counter = 1;
            for (int i = 0; i < matrix.GetLength(0) - 1; i++)
            {
                for (int j = 0; j < matrix.GetLength(1) - 1; j++)
                {
                    if (matrix[i, j] == 1)
                    {
                        matrix[i, j] = counter;
                        counter++;
                    }
                        Console.Write("{0,2} ", matrix[i, j]);
                }
                Console.WriteLine();
            }
            return counter;

        }

        static int[] CreateCounters(int length)
        {
            int c = 0;
            do
            {
                int[] countersL = new int[length];
                for (int i = 0; length > i; i++)
                {
                    countersL[i] = i;
                    Console.Write("{0} ", countersL[i]);
                }
                c++;
                return countersL;
            }
            while (c < length - 1);
        }
        
        static void MassSwap(int[,] Massive)
        {
            int changes = 0;
            Console.WriteLine();
            do
            {
                changes = 0;
                for (int j = 0; j < Massive.GetLength(0) - 1; j++)
                {
                    for (int i = 0; i < Massive.GetLength(1) - 1; i++)
                    {

                        if (i - 1 >= 0)
                        {
                            if (Massive[i - 1, j] < Massive[i, j] && Massive[i - 1, j] != 0)
                            {
                                Massive[i, j] = Massive[i - 1, j];
                                changes++;
                            }
                            
                        }
                        if (i <= Massive.GetLength(0))
                        {
                            if (Massive[i + 1, j] < Massive[i, j] && Massive[i + 1, j] != 0)
                            {
                                Massive[i, j] = Massive[i + 1, j];
                                changes++;
                            }
                            
                        }
                        if (j - 1 >= 0)
                        {
                            if (Massive[i, j - 1] < Massive[i, j] && Massive[i, j - 1] != 0)
                            {
                                Massive[i, j] = Massive[i, j - 1];
                                changes++;
                            }
                            
                        }
                        if (j <= Massive.GetLength(1))
                        {
                            if (Massive[i, j + 1] < Massive[i, j] && Massive[i, j + 1] != 0)
                            {
                                Massive[i, j] = Massive[i, j + 1];
                                changes++;
                            }
                            
                        }


                    }
                }
            }
            while (changes > 0);
            Console.WriteLine();
            for (int i = 0; i < Massive.GetLength(0); i++)
            {
                for (int j = 0; j < Massive.GetLength(1); j++) 
                {
                    if (i != Massive.GetLength(0) - 1 && j != Massive.GetLength(1) - 1)
                        Console.Write("{0, 2} ", Massive[i, j]);
                }
                Console.WriteLine();
            }
                    
        }

        static void MergeOnes(int[,] a, int n)
        {
            int[] ac = new int[n];
            for (int i = 0; i < a.GetLength(0) -1; i++)
                for (int j = 0; j < a.GetLength(1) -1; j++)
                {
                    if (a[i, j] == 0)
                    {
                        continue;
                    }
                    else ac[a[i, j] - 1]++;
                }
            for (int i = 0; i < ac.Length; i++)
            {
                Console.Write("{0} ",ac[i]);
            }
             int max = Math.Abs(ac.Max());
            
            Console.WriteLine("\nОбьем самого большого резервуара воды: {0} ",max);
        }

        static void Print(int[,] a)
        {
            Console.WriteLine();
            for (int i = 0; i < a.GetLength(0) -1; i++)
            {
                Console.Write("|");
                for (int j = 0; j < a.GetLength(1) -1; j++)
                    Console.Write((a[i, j] == 0) ? ' ' : 'N');
            
                Console.Write("|");
                Console.WriteLine();
            }
        }
    }
}