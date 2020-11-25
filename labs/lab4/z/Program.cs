using System;
using static System.Math;
using Progbase.Procedural;

struct Point
{
    public double x;
    public double y;
}
struct Circle
{
    public Point center;
    public int radius;

}
class Program
{
    static void Main()
    {
        const int size = 40;
        int r1 = 10;
        int r2 = 3;
        double alpha = PI/3;

        Point a = new Point();
        a.x = size/2;
        a.y = size/2;

        Canvas.SetSize(size, size);
        Canvas.InvertYOrientation();
        Console.Clear();
        ConsoleKeyInfo keyInfo;

        do{
            Point b = new Point();
            b.x = r1 * Cos(alpha) + a.x; 
            b.y = r1 * Sin(alpha) + a.y;

            Circle c = new Circle();
            c.center = b;
            c.radius = r2;

            Canvas.BeginDraw();
            Canvas.StrokeLine(0, 0, size, size);
            Canvas.SetColor("#FFCBA4");
            Canvas.PutPixel((int)a.x, (int)a.y);
            Canvas.SetColor("#3EA99F");
            Canvas.FillCircle((int)b.x, (int)b.y, (int)r2);
            Canvas.PutPixel((int)b.x, (int)b.y);       
            Canvas.EndDraw();

            keyInfo = Console.ReadKey();
            if(keyInfo.Key == ConsoleKey.A)
            {
                if(a.x > 0)
                {
                    a.x -= 1;
                    a.y -= 1;
                }
            }
            else if(keyInfo.Key == ConsoleKey.D)
            {
                if(a.x < size-1)
                {
                    a.x += 1;
                    a.y += 1;
                }
            }
            else if(keyInfo.Key == ConsoleKey.S)
            {
                    alpha -= PI/10; 
            }
            else if(keyInfo.Key == ConsoleKey.W)
            {
                    alpha += PI/10; 
            }
             else if(keyInfo.Key == ConsoleKey.E)
            {
                if(r1 < size-1)
                {
                    r1 += 1;
                }
            }
            else if(keyInfo.Key == ConsoleKey.Q)
            {
               if(a.x != b.x && a.y != b.y)
                {
                   r1 -= 1; 
                }
            }
            else if(keyInfo.Key == ConsoleKey.X)
            {
                if( r2 < size)
                {
                    r2 += 1;
                }
            }
            else if(keyInfo.Key == ConsoleKey.Z)
            {
               if(r2 > 1)
                {
                   r2 -= 1; 
                }
            }
        }
        while(keyInfo.Key != ConsoleKey.Escape); 
    }
}