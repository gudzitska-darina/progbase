using System.Drawing;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using ProgbaseLab.ImageEditor.Common;

namespace ProgbaseLab.ImageEditor.Fast
{
    public class ClassFast : IRunnable
    {
        public Bitmap Blur(Bitmap bmp, int sigma)
        {
            Mat converted = BitmapConverter.ToMat(bmp);
            Mat result = new Mat();
            Cv2.GaussianBlur(converted, result, new OpenCvSharp.Size(5, 5), sigma);
            bmp = BitmapConverter.ToBitmap(result);
            return bmp;
        }

        public Bitmap FlipVertical(Bitmap bmp)
        {
            Mat converted = BitmapConverter.ToMat(bmp);
            Mat result = new Mat();
            Cv2.Flip(converted, result, FlipMode.X);
            bmp = BitmapConverter.ToBitmap(result);
            return bmp;
        }

        public Bitmap Grayscale(Bitmap bmp)
        {
            Mat converted = BitmapConverter.ToMat(bmp);
            Mat result = new Mat();
            Cv2.CvtColor(converted, result, ColorConversionCodes.BGR2GRAY);
            bmp = BitmapConverter.ToBitmap(result);
            return bmp;
        }

        public Bitmap RemoveRed(Bitmap bmp)
        {
            Mat converted = BitmapConverter.ToMat(bmp);
            Mat[] channels = Cv2.Split(converted);
            channels[2].SetTo(0);
            Mat result = new Mat();
            Cv2.Merge(channels, result);
            bmp = BitmapConverter.ToBitmap(result);
            return bmp;
        }
    }
}
