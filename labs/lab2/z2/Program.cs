using System;
using static System.Math;

 class Program{
    static void Main()
    {
        Console.WriteLine("Введите минимальное значение ");
        double xMin = double.Parse(Console.ReadLine());
        Console.WriteLine("Введите максимальное значение ");
        double xMax = double.Parse(Console.ReadLine());
        Console.WriteLine("Введите шаг ");
        double nSteps = double.Parse(Console.ReadLine());
            if(xMin>=xMax )
            {
                Console.WriteLine("Error (min|max)");
            }
            else if(nSteps<=0)
            {
                Console.WriteLine("Error (step)");
            }
            else
            {
                double integl = IntFx(xMin, xMax, nSteps);
                Console.WriteLine("Интеграл: {0}", integl);
            }

    }   
 
    static double Fx(double x)
    {
        if(x>=-5 && x<=3)
        {
            return Gx(x);
        }
         else if( (x-1)==0)
        {
            return double.NaN;
        }
	    else
        {
	        return Hx(x);
        }	
    }
    
    static double Gx(double x)
    {
         return (Pow(x,2) - 5) / (x - 1);
    }

    static double Hx(double x)
    {
         return Cos(Pow(x,2)) / Pow(Sin(2*x), 2) + 1;
    }
    
    static double IntFx(double xMin, double xMax, double nSteps)
    {
        double step = (xMax-xMin)/nSteps;
        double sum =0;
        for(int i =0; i<=nSteps; i++ )
        {
            double x = xMin + i * step;
            double y = Fx(x); 
            sum =+ y;
        }

        double integl = step*sum;
        return integl;

    }
}
