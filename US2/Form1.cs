using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Video.DirectShow;

namespace US2
{
    public partial class Form1 : Form
    {
        VideoCaptureDevice dev;
        CameraControlProperty prop = CameraControlProperty.Iris;
        public Form1()
        {
            InitializeComponent();

            FilterInfoCollection col = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            dev = new VideoCaptureDevice(col[1].MonikerString);
            dev.NewFrame += Dev_NewFrame1;
            if(dev.CheckIfCrossbarAvailable())
                dev.DisplayCrossbarPropertyPage(Handle);

            int minVal;
            int maxVal;
            int defVal;
            int step;
            CameraControlFlags flag;

            dev.GetCameraPropertyRange(CameraControlProperty.Exposure, out minVal, out maxVal,
                out step, out defVal, out flag);

            trackBar1.Minimum = minVal;
            trackBar1.Maximum = maxVal;
            trackBar1.SmallChange = step;
            trackBar1.Value = defVal;
                

            dev.Start();

            

            //uint handle = 10;
            
            //uint val2 = 11;

            //uint[] array_names = new uint[100];

            //ArduCamCfg cfg = new ArduCamCfg();

            

            //val2 = LibHelp.ArduCam_scan( array_names, 21195);
            //MessageBox.Show(val2.ToString());

            //cfg.u32Width = 3664;
            //cfg.u32Height = 2748;
            //cfg.u16Vid = 0x52CB;
            //cfg.u32I2cAddr = 0x20;
            //cfg.emI2cMode = i2c_mode.I2C_MODE_16_16;
            //cfg.emImageFmtMode = format_mode.FORMAT_MODE_RAW;
            //cfg.u8PixelBytes = 1;
            //cfg.u8PixelBits = 8;
            //cfg.u32TransLvl = 0;
            //cfg.u32CameraType = 0;

            //val2 = 11;
            //val2 = LibHelp.ArduCam_open(ref handle, ref cfg, 0);
            //MessageBox.Show(val2 + "\n" + handle);

            //UInt32 conf_handle = 11;

            //String tmpStr = "MT9J001_10MP_8bit.cfg";

            //val2 = LibHelp.ArduCamCfg_LoadCameraConfig(ref conf_handle, ref tmpStr);
            //MessageBox.Show(val2 + "/n" + conf_handle);

            //UInt32 register_val = 11;
            //val2 = LibHelp.ArduCam_readSensorReg(ref handle, 0x0204, ref register_val);
            //MessageBox.Show(val2 + "\n" + register_val);

            //ArduCamOutData dt = new ArduCamOutData();
            ////dt.stImagePara = cfg;
            ////dt.pu8ImageData = new byte[cfg.u32Width * cfg.u32Height];
            //unsafe
            //{
            //    byte[] arr = new byte[(int)(cfg.u32Height * cfg.u32Width * 10)];
            //    fixed (byte* point = arr)
            //    {
            //        dt.pu8ImageData = point;
            //        ArduCamCfg[] cfg_arr = new ArduCamCfg[] { cfg };
            //        fixed (ArduCamCfg* cfg_point = cfg_arr)
            //        {
            //            dt.stImagePara = cfg_point;
            //            val2 = LibHelp.ArduCam_readImage(ref handle, ref dt);
            //        }
            //    }
            //}

            //val2 = 11;
            //MessageBox.Show(val2 + "");
        }

        private void Dev_NewFrame1(object sender, AForge.Video.NewFrameEventArgs eventArgs)
        {
            pictureBox1.Image = new Bitmap(eventArgs.Frame);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //dev.SetCameraProperty(CameraControlProperty.Exposure, 0, CameraControlFlags.Manual);
            dev.DisplayPropertyPage(Handle);
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            dev.SetCameraProperty(CameraControlProperty.Exposure, trackBar1.Value, CameraControlFlags.Manual);
        }
    }
}
