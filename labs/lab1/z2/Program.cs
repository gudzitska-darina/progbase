using System;

namespace z2
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Write x ");
            double x = double.Parse(Console.ReadLine());
            double y;

            if(x>=0 && x<=8){
            y= (Math.Pow(x,2)-5)/(x-1);
            }
            else if((x-1) == 0 ){
            y= double.NaN;
            }
	        else{
		    y= Math.Cos(Math.Pow(x,2)) / Math.Pow(Math.Sin(2*x), 2) + 1;
            }	
            

            Console.WriteLine("y = {0}", y);
        }
    }
}
