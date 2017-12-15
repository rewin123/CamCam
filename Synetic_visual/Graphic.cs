using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synetic_visual
{
    class Plot2D
    {
        List<double> ys = new List<double>();
        List<double> xs = new List<double>();

        double max = double.MinValue;
        double min = double.MaxValue;

        object lock_obj = new object();

        public void AddPoint(double y)
        {
            lock (lock_obj)
            {
                ys.Add(y);
                if (xs.Count > 0)
                    xs.Add(xs.Last() + 1);
                else xs.Add(0);
                min = Math.Min(min, y);
                max = Math.Max(max, y);
            }
        }

        public void AddPoint(double y, double x)
        {
            lock (lock_obj)
            {
                ys.Add(y);
                xs.Add(x);
            }
        }

        public Bitmap DrawAll(int width, int height, Pen pen)
        {
            lock (lock_obj)
            {
                Bitmap res = new Bitmap(width, height);

                Graphics gr = Graphics.FromImage(res);

                double maxY = double.MinValue;
                double minY = double.MaxValue;

                double maxX = double.MinValue;
                double minX = double.MaxValue;

                for (int i = 0; i < ys.Count; i++)
                {
                    maxY = Math.Max(maxY, ys[i]);
                    minY = Math.Min(minY, ys[i]);

                    maxX = Math.Max(maxX, xs[i]);
                    minX = Math.Min(minX, xs[i]);
                }

                double dx = maxX - minX;
                double dy = maxY - minY;

                for (int i = 1; i < ys.Count; i++)
                {
                    gr.DrawLine(pen, (float)((xs[i - 1] - minX) / dx * width),
                        (float)((ys[i - 1] - minY) / dy * height),
                        (float)((xs[i] - minX) / dx * width),
                        (float)((ys[i] - minY) / dy * height));
                }


                gr.Dispose();
                res.RotateFlip(RotateFlipType.RotateNoneFlipY);

                return res;
            }
        }

        public Bitmap DrawLast(int width, int height, Pen pen, int count)
        {
            lock (lock_obj)
            {
                Bitmap res = new Bitmap(width, height);

                Graphics gr = Graphics.FromImage(res);

                double maxY = double.MinValue;
                double minY = double.MaxValue;

                double maxX = double.MinValue;
                double minX = double.MaxValue;

                int start = ys.Count - count;
                start = start > 0 ? start : 0;

                for (int i = start; i < ys.Count; i++)
                {
                    maxY = Math.Max(maxY, ys[i]);
                    minY = Math.Min(minY, ys[i]);

                    maxX = Math.Max(maxX, xs[i]);
                    minX = Math.Min(minX, xs[i]);
                }

                double dx = maxX - minX;
                double dy = maxY - minY;

                if (dy != 0 && dx != 0)
                {
                    for (int i = start + 1; i < ys.Count; i++)
                    {
                        gr.DrawLine(pen, (float)((xs[i - 1] - minX) / dx * width),
                            (float)((ys[i - 1] - minY) / dy * height),
                            (float)((xs[i] - minX) / dx * width),
                            (float)((ys[i] - minY) / dy * height));
                    }
                }


                gr.Dispose();
                res.RotateFlip(RotateFlipType.RotateNoneFlipY);

                return res;
            }
        }

        public Bitmap DrawLast(int width, int height, Pen pen, int count, double hval)
        {
            lock (lock_obj)
            {
                Bitmap res = new Bitmap(width, height);

                Graphics gr = Graphics.FromImage(res);

                //double maxY = double.MinValue;
                //double minY = double.MaxValue;

                double maxX = double.MinValue;
                double minX = double.MaxValue;

                int start = ys.Count - count;
                start = start > 0 ? start : 0;

                for (int i = start; i < ys.Count; i++)
                {
                    //maxY = Math.Max(maxY, ys[i]);
                    //minY = Math.Min(minY, ys[i]);

                    maxX = Math.Max(maxX, xs[i]);
                    minX = Math.Min(minX, xs[i]);
                }

                double dx = maxX - minX;

                if ( dx != 0)
                {
                    for (int i = start + 1; i < ys.Count; i++)
                    {
                        gr.DrawLine(pen, (float)((xs[i - 1] - minX) / dx * width),
                            (float)((ys[i - 1]) / hval * height),
                            (float)((xs[i] - minX) / dx * width),
                            (float)((ys[i]) / hval * height));
                    }
                }


                gr.Dispose();
                res.RotateFlip(RotateFlipType.RotateNoneFlipY);

                return res;
            }
        }

        public double Mean
        {
            get
            {
                lock (lock_obj)
                {
                    double mean = 0;
                    for (int i = 0; i < ys.Count; i++)
                    {
                        mean += ys[i];
                    }
                    mean /= ys.Count;
                    return mean;
                }
            }
        }

        public double Deviation
        {
            get
            {
                lock(lock_obj)
                {
                    double mean = Mean;
                    double dev = 0;
                    for (int i = 0; i < ys.Count; i++)
                    {
                        dev += (mean - ys[i]) * (mean - ys[i]);
                    }

                    dev = Math.Sqrt(dev);
                    return dev / ys.Count;
                }
            }
        }
    }
}
