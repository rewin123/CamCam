using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;



namespace US2
{
    class LibHelp
    {
        [DllImport("ArduCamConfigLib.dll", EntryPoint = "ArduCamCfg_LoadCameraConfig")]
        public static extern UInt32 ArduCamCfg_LoadCameraConfig(ref UInt32 useHandle, [MarshalAs(UnmanagedType.LPStr)] ref string csFilePath);

        [DllImport("ArduCamLib.dll", EntryPoint = "ArduCam_scan")]
        public static extern UInt32 ArduCam_scan(UInt32[] pUsbIdxArray, UInt16 useVID);

        [DllImport("ArduCamLib.dll",EntryPoint = "ArduCam_autoopen")]
        public static extern UInt32 ArduCam_autoopen(ref UInt32 useHandle, ref ArduCamCfg useCfg);

        [DllImport("ArduCamLib.dll", EntryPoint = "ArduCam_open")]
        public static extern UInt32 ArduCam_open(ref UInt32 useHandle, ref ArduCamCfg useCfg, UInt32 usbIdx);

        [DllImport("ArduCamLib.dll", EntryPoint = "ArduCam_close")]
        public static extern UInt32 ArduCam_close(ref UInt32 useHandle);

        [DllImport("ArduCamLib.dll", EntryPoint = "ArduCam_beginCaptureImage")]
        public static extern UInt32 ArduCam_beginCaptureImage(ref UInt32 useHandle);

        [DllImport("ArduCamLib.dll", EntryPoint = "ArduCam_captureImage")]
        public static extern UInt32 ArduCam_captureImage(ref UInt32 useHandle);

        [DllImport("ArduCamLib.dll", EntryPoint = "ArduCam_endCaptureImage")]
        public static extern UInt32 ArduCam_endCaptureImage(ref UInt32 useHandle);

        [DllImport("ArduCamLib.dll", EntryPoint = "ArduCam_captureIMU")]
        public static extern UInt32 ArduCam_captureIMU(ref UInt32 useHandle);

        [DllImport("ArduCamLib.dll", EntryPoint = "ArduCam_availiableImage")]
        public static extern UInt32 ArduCam_availiableImage(ref UInt32 useHandle);

        [DllImport("ArduCamLib.dll", EntryPoint = "ArduCam_availiableIMU")]
        public static extern UInt32 ArduCam_availiableIMU(ref UInt32 useHandle);

        [DllImport("ArduCamLib.dll", EntryPoint = "ArduCam_readImage")]
        public static extern unsafe UInt32 ArduCam_readImage(ref UInt32 useHandle, ref ArduCamOutData pstFrameData);

        [DllImport("ArduCamLib.dll", EntryPoint = "ArduCam_readSensorReg")]
        public static extern UInt32 ArduCam_readSensorReg(ref UInt32 useHandle, UInt32 regAddr, ref UInt32 pval);


        


    }

    enum i2c_mode
    {
        I2C_MODE_8_8 = 0,
        I2C_MODE_8_16 = 1,
        I2C_MODE_16_8 = 2,
        I2C_MODE_16_16 = 3
    }

    enum format_mode
    {
        FORMAT_MODE_RAW = 0,
        FORMAT_MODE_RGB = 1,
        FORMAT_MODE_YUV = 2,
        FORMAT_MODE_JPG = 3,
    }

    [StructLayout(LayoutKind.Sequential)]
    struct ArduCamCfg
    {
        public UInt32 u32CameraType;           // Camera Type
        public UInt16 u16Vid;                  // Vendor ID for USB

        public UInt32 u32Width;                // Image Width
        public UInt32 u32Height;               // Image Height

        public UInt32 u32Size;

        public byte u8PixelBytes;
        public byte u8PixelBits;

        public format_mode emImageFmtMode;         // image format mode 

        public i2c_mode emI2cMode;
        public UInt32 u32I2cAddr;

        public UInt32 u32TransLvl;
    }

    [StructLayout(LayoutKind.Sequential)]
    struct ArduIMUData
    {
        
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct ArduCamOutData
    {
        public unsafe ArduCamCfg* stImagePara;

        //[MarshalAs(UnmanagedType.ByValArray, SizeConst = 3664 * 2748, ArraySubType = UnmanagedType.U1)]
        //public byte[] pu8ImageData;
        
        public unsafe byte* pu8ImageData;
    }
}
