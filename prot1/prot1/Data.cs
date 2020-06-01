using System.Drawing;
using System.IO;

using AForge.Imaging.Filters;

using AForge.Video;
using AForge.Video.DirectShow;

namespace prot1
{
    class Data
    {
        private static string path = "S:/ГПО/prot1/image";
        private static float red = 0f, green = 0.2f, blue = 0.56f;
        private static float similarity_limit = 0.75f;

        private static Bitmap image_original = null;
        private static Bitmap image_ready = null;

        private static int h_resolution = 445;
        private static int w_resolution = 600;

        //================================================================

        public static string Path 
        {
            get { return path; }
            set
            {
                if (Directory.Exists(value))
                    path = value;
            }
        }
        
        public static float Red
        {
            get { return red; }
            set
            {
                if (value >= 0 && value <= 1)
                    red = value;
            }
        }
        public static float Green
        {
            get { return green; }
            set
            {
                if (value >= 0 && value <= 1)
                    green = value;
            }
        }
        public static float Blue
        {
            get { return blue; }
            set
            {
                if (value >= 0 && value <= 1)
                    blue = value;
            }
        }
        
        public static float Similarity_limit
        {
            get { return similarity_limit; }
            set
            {
                if (value >= 0 && value <= 1)
                    similarity_limit = value;
            }
        }

        public static Bitmap Image_original
        {
            get { return image_original; }
            private set
            {
                image_original = value;
            }
        }
        public static Bitmap Image_ready
        {
            get { return image_ready; }
            private set
            {
                image_ready = value;
            }
        }

        public static int H_resolution
        {
            get { return h_resolution; }
            set { h_resolution = value; }
        }
        public static int W_resolution
        {
            get { return w_resolution; }
            set { w_resolution = value; }
        }

        public delegate void AccountHandler();
        public static event AccountHandler Update_frame;

        public static void Default()
        {
            Path = "S:/ГПО/prot1/image";

            Red = 0f;
            Green = 0.2f;
            Blue = 0.56f;

            Similarity_limit = 0.75f;
        }

        private static FilterInfoCollection videoDevices;
        private static VideoCaptureDevice videoSource = null;
        private static ResizeBicubic FilterResize;

        public static void video_Start()
        {
            videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            videoSource = new VideoCaptureDevice(videoDevices[0].MonikerString);

            FilterResize = new ResizeBicubic(W_resolution, H_resolution);

            videoSource.NewFrame += new NewFrameEventHandler(video_NewFrame);
            CloseVideoSource();
            videoSource.Start();
        }

        private static void video_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap img = FilterResize.Apply((Bitmap)eventArgs.Frame.Clone());

            Image_original = (Bitmap)img.Clone();

            Bitmap bmp = (Bitmap)img.Clone();

            Grayscale FilterGrayscale = new Grayscale(Data.Red, Data.Green, Data.Blue);
            SobelEdgeDetector FilterSobelEdgeDetector = new SobelEdgeDetector();

            bmp = FilterGrayscale.Apply(bmp);
            bmp = FilterSobelEdgeDetector.Apply(bmp);

            Image_ready = (Bitmap)bmp.Clone();

            Update_frame();
        }

        public static void CloseVideoSource()
        {
            if (!(videoSource == null))
                if (videoSource.IsRunning)
                {
                    videoSource.SignalToStop();
                    videoSource = null;
                }
            videoDevices.Clear();
        }
    }
}
