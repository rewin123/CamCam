using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace Synetic_visual
{
    public static class ImageDataConverter
    {
        public static double[,] GetGrey(this Bitmap input, int startX, int startY, int w, int h, int offset, int scansize)
        {
            const int PixelWidth = 3;
            const PixelFormat PixelFormat = PixelFormat.Format32bppArgb;
            const double max = 3 * 255;

            double[,] rgbArray = new double[w, h];

            Bitmap image = input.Clone(new Rectangle(0, 0, input.Width, input.Height), PixelFormat);

            // En garde!
            if (image == null) throw new ArgumentNullException("image");
            if (rgbArray == null) throw new ArgumentNullException("rgbArray");
            if (startX < 0 || startX + w > image.Width) throw new ArgumentOutOfRangeException("startX");
            if (startY < 0 || startY + h > image.Height) throw new ArgumentOutOfRangeException("startY");
            if (w < 0 || w > scansize || w > image.Width) throw new ArgumentOutOfRangeException("w");
            if (h < 0 || (rgbArray.Length < offset + h * scansize) || h > image.Height) throw new ArgumentOutOfRangeException("h");

            BitmapData data = image.LockBits(new Rectangle(startX, startY, w, h), System.Drawing.Imaging.ImageLockMode.ReadOnly, PixelFormat);

            try
            {
                byte[] pixelData = new byte[3];
                int index = 0;
                for (int y = 0; y < h; y++)
                {
                    for (int x = 0; x < w; x++)
                    {
                        Marshal.ReadByte(data.Scan0, index); //alpha
                        index++;

                        byte r = Marshal.ReadByte(data.Scan0, index);
                        index++;
                        byte g = Marshal.ReadByte(data.Scan0, index);
                        index++;
                        byte b = Marshal.ReadByte(data.Scan0, index);
                        index++;

                        rgbArray[x, y] = (r + g + b) / max;
                    }
                }

            }
            finally
            {
                image.UnlockBits(data);
            }

            return rgbArray;
        }

        

        /// <summary>
        /// Возращает изображение в виде массива типа (color channel, y, x) нормированного на 1
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static double[,,] GetRGBArray(this Bitmap input)
        {
            const int PixelWidth = 3;
            const PixelFormat PixelFormat = PixelFormat.Format32bppArgb;
            const double max = 3 * 255;

            int w = input.Width;
            int h = input.Height;

            double[,,] rgbArray = new double[3,h, w];

            Bitmap image = input.Clone(new Rectangle(0, 0, input.Width, input.Height), PixelFormat);
            BitmapData data = image.LockBits(new Rectangle(0, 0, w, h), System.Drawing.Imaging.ImageLockMode.ReadOnly, PixelFormat);

            try
            {
                //byte[] pixelData = new byte[3];
                int index = 0;
                for (int y = 0; y < h; y++)
                {
                    for (int x = 0; x < w; x++)
                    {
                        byte b = Marshal.ReadByte(data.Scan0, index);
                        index++;
                        byte g = Marshal.ReadByte(data.Scan0, index);
                        index++;
                        byte r = Marshal.ReadByte(data.Scan0, index);
                        index++;

                        byte a = Marshal.ReadByte(data.Scan0, index); //alpha
                        index++;
                        rgbArray[0, y, x] = r / 255.0;
                        rgbArray[1, y, x] = g / 255.0;
                        rgbArray[2, y, x] = b / 255.0;

                    }
                }

            }
            finally
            {
                image.UnlockBits(data);
            }

            return rgbArray;
        }

        public static Bitmap GetImage(this double[,] array)
        {
            int width = array.GetLength(0);
            int height = array.GetLength(1);
            Bitmap map = new Bitmap(width, height);

            Graphics gr = Graphics.FromImage(map);

            SolidBrush[] colors = new SolidBrush[256];
            for (int i = 0; i < colors.Length; i++)
            {
                colors[i] = new SolidBrush(Color.FromArgb(i, i, i));
            }

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    gr.FillRectangle(colors[(byte)(array[x, y] * 255)], x, y, 1, 1);
                }
            }

            return map;
        }

        
        

        public static void LevelBinar(this double[,] array, double level)
        {
            int width = array.GetLength(0);
            int height = array.GetLength(1);
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    array[x, y] = array[x, y] >= level ? 1 : 0;
                }
            }
        }

        public static void Binary(this double[] array, double level)
        {
            for (int i = 0; i < array.Length; i++)
                array[i] = array[i] > level ? 1 : 0;
        }
    }
}
