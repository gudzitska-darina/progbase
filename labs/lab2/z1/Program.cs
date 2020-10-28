using System;
using static System.Math;

 class Program{

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

     static void Main()
     {
        const double min = -10;
        const double max = 10;
        const double step = 0.5;
        double x= min;
        double y=0;
        while(x<=max)
        { 
            y= Fx(x);
    
        Console.WriteLine("y[{0}] = {1}",x, y);
        x=x+step;
        }
    }
 }