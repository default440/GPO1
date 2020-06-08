using AForge.Imaging;
using AForge.Imaging.Filters;
using AForge.Video;
using AForge.Video.DirectShow;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Prot0
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            video_Start();
        }

        private static FilterInfoCollection videoDevices;
        private static VideoCaptureDevice videoSource = null;
        private static ResizeBicubic FilterResize;

        private static int H_resolution = 256;
        private static int W_resolution = 256;

        private static float red = 0f, green = 0.2f, blue = 0.56f;

        public void video_Start()
        {
            videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            videoSource = new VideoCaptureDevice(videoDevices[0].MonikerString);

            FilterResize = new ResizeBicubic(W_resolution, H_resolution);

            videoSource.NewFrame += new NewFrameEventHandler(video_NewFrame);
            CloseVideoSource();
            videoSource.Start();
        }

        Bitmap gest = null;
        bool mutex = true; 

        private void video_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap img = FilterResize.Apply((Bitmap)eventArgs.Frame.Clone());

            pictureBox1.Image = (Bitmap)img.Clone();

            Grayscale FilterGrayscale = new Grayscale(red, green, blue);
            
            img = FilterGrayscale.Apply((Bitmap)img.Clone());

            pictureBox2.Image = (Bitmap)img.Clone();

            SobelEdgeDetector FilterSobelEdgeDetector = new SobelEdgeDetector();

            img = FilterSobelEdgeDetector.Apply((Bitmap)img.Clone());

            pictureBox3.Image = (Bitmap)img.Clone();
            pictureBox5.Image = (Bitmap)img.Clone();
            pictureBox8.Image = (Bitmap)img.Clone();

            if (mutex)
            {
                gest = (Bitmap)img.Clone();
                mutex = false;
                pictureBox11.Image = (Bitmap)gest.Clone();
            }

            float sim = 0;

            ExhaustiveTemplateMatching tm = new ExhaustiveTemplateMatching(0.5f);
            TemplateMatch[] matchings;

            Bitmap overlay = (Bitmap)img.Clone();

            Add AddFilter = new Add(overlay);
            if (gest != null) overlay = AddFilter.Apply((Bitmap)gest.Clone());

            pictureBox9.Image = (Bitmap)overlay.Clone();

            matchings = tm.ProcessImage(img, overlay);
            if (matchings.Length > 0) sim = matchings[0].Similarity;

            this.label4.BeginInvoke((MethodInvoker)(() => this.label4.Text = Convert.ToString(sim)));
        }

        public void CloseVideoSource()
        {
            if (!(videoSource == null))
                if (videoSource.IsRunning)
                {
                    videoSource.SignalToStop();
                    videoSource = null;
                }
            videoDevices.Clear();
        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
