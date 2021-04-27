using System;
using System.Drawing;

namespace ProgbaseLab.ImageEditor.Common
{
    public interface IRunnable
    {
        Bitmap FlipVertical(Bitmap bmp);
        Bitmap RemoveRed(Bitmap bmp);
        Bitmap Grayscale(Bitmap bmp);
        Bitmap Blur(Bitmap bmp, int sigma);

    }
}
