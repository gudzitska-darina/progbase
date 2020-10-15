using System;

namespace z1
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Write a ");
            double a = double.Parse(Console.ReadLine());
            Console.WriteLine("Write b ");
            double b = double.Parse(Console.ReadLine());
            Console.WriteLine("Write c ");
            double c = double.Parse(Console.ReadLine());
            if ((a - b) != 0 && a != 0 && Math.Sin(a) != 0)
            {
                double d0 = (Math.Pow((a + 3), (c + 1)) - 10) / (a - b);
                double d1 = 5 * b + c / a;
                double d2 = Math.Sqrt(Math.Abs(Math.Cos(b) / Math.Sin(a) + 5));

                double d = d0 + d1 + d2;

                Console.WriteLine("a = {0}", a);
                Console.WriteLine("b = {0}", b);
                Console.WriteLine("c = {0}", c);
                Console.WriteLine("d0 = {0}", d0);
                Console.WriteLine("d1 = {0}", d1);
                Console.WriteLine("d2 = {0}", d2);
                Console.WriteLine("d = {0}", d);
            }
            else
            {
                Console.WriteLine("Doesn`t correspond to ODZ!");
            }
        }
    }
}
