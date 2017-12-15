using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Video.DirectShow;

namespace Synetic_visual
{
    public partial class Experiment : Form
    {
        Plot2D plot = new Plot2D();
        VideoCaptureDevice dev;
        public Experiment(VideoCaptureDevice dev)
        {
            this.dev = dev;
            InitializeComponent();
            dev.SetCameraProperty(CameraControlProperty.Exposure, -8, CameraControlFlags.Manual);
            dev.NewFrame += Dev_NewFrame;
            dev.Start();

            timer1.Start();
        }

        private void Dev_NewFrame(object sender, AForge.Video.NewFrameEventArgs eventArgs)
        {
            pictureBox1.Image = new Bitmap(eventArgs.Frame);
            GraphBuilder(eventArgs.Frame);
        }

        void GraphBuilder(Bitmap map)
        {
            double[,] img_grey = ImageDataConverter.GetGrey(map, 0, 0, map.Width, map.Height, 0, map.Width);
           
            plot.AddPoint(img_grey[map.Width / 2, map.Height / 2]);
        }

        ~Experiment()
        {
            dev.Stop();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Bitmap res = plot.DrawLast(pictureBox2.Width, pictureBox2.Height, Pens.Red, 100, 1);
            pictureBox2.Image = res;

            label1.Text = plot.Deviation.ToString();
        }
    }
}
