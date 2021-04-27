using System;
using System.Drawing;
using ProgbaseLab.ImageEditor.Common;
using static System.Math;

namespace ProgbaseLab.ImageEditor.Pixel
{
    public class ClassPixel : IRunnable
    {
        public Bitmap FlipVertical(Bitmap bmp)
        {
            bmp.RotateFlip(RotateFlipType.RotateNoneFlipY);
            return bmp;
        }
        
        public Color ApplyFilter(Bitmap image,
                                 int x,
                                 int y,
                                 double[,] filter)
        {
            double red = 0.0;
            double green = 0.0;
            double blue = 0.0;
            
            int filterSize = filter.GetLength(0);
            int radius = filterSize / 2;
            
            int w = image.Width;
            int h = image.Height;
            
            for (int filterX = -radius; filterX <= radius; filterX++)
            {
                for (int filterY = -radius; filterY <= radius; filterY++)
                {
                    double filterValue = filter[filterX + radius, filterY + radius];
            
                    int imageX = (x + filterX + w) % w;
                    int imageY = (y + filterY + h) % h;
            
                    Color imageColor = image.GetPixel(imageX, imageY);
            
                    red += imageColor.R * filterValue;
                    green += imageColor.G * filterValue;
                    blue += imageColor.B * filterValue;
                }
            }
            
            int r = Math.Min(Math.Max((int)red, 0), 255);
            int g = Math.Min(Math.Max((int)green, 0), 255);
            int b = Math.Min(Math.Max((int)blue, 0), 255);
            
            return Color.FromArgb(r, g, b);

        }

        public Bitmap Blur(Bitmap bmp, int sigma)    
        {
            int radius = (int)(sigma * 2);
            int size = 2 * radius + 1;
            double[,] filter = new double[size, size];
            for (int x = -radius; x <= radius; x++)
            {
                for (int y = -radius; y <= radius; y++)
                {
                    filter[radius + x, radius + y] = Gauss(x, y, sigma);
                }
            }

            for (int x = 0; x < bmp.Width; x++)
            {
                for (int y = 0; y < bmp.Height; y++)
                {
                    Color originalColor = bmp.GetPixel(x, y);
                    Color newColor = ApplyFilter(bmp, x, y, filter);
                    bmp.SetPixel(x, y, newColor);
                }
            }
            return bmp;
        }
        static double Gauss(int x, int y, int sigma)
        {
            return (1 /(2 * PI * Pow(sigma,2)) * Exp(- (Pow(x,2) + Pow(y,2)) / (2 * Pow(sigma,2))));
        }

        public Bitmap Grayscale(Bitmap bmp)
        {
            for (int i = 0; i < bmp.Height; i++)
            {
                for (int j = 0; j < bmp.Width; j++)
                {
                    Color color = bmp.GetPixel(j, i);
                    int yLinear = (int)(0.2126 * color.R + 0.7152 * color.G + 0.0722 * color.B);
                    Color newColor = Color.FromArgb(255, yLinear, yLinear, yLinear);
                    bmp.SetPixel(j, i, newColor);
                }
            }
            return bmp;
        }

        public Bitmap RemoveRed(Bitmap bmp)
        {
            for (int i = 0; i < bmp.Height; i++)
            {
                for (int j = 0; j < bmp.Width; j++)
                {
                    Color color = bmp.GetPixel(j, i);
                    Color newColor = Color.FromArgb(255, 0, color.G, color.B);
                    bmp.SetPixel(j, i, newColor);
                }
            }
            return bmp;
        }

    }
}
