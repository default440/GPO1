using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;

using AForge.Imaging.Filters;

using AForge.Video;
using AForge.Video.DirectShow;

namespace prot1
{
    public partial class Form3 : Form
    {
        private Bitmap Photo = null;
        private string name = null;
        public Form3()
        {
            InitializeComponent();
            Data.Update_frame += video_NewFrame;
        }

        private void video_NewFrame()
        {
            Bitmap img = (Bitmap)Data.Image_original.Clone();

            if (Photo == null) pictureBox1.Image = (Bitmap)img.Clone();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Photo = (Bitmap)Data.Image_original.Clone();

            pictureBox1.Image = (Bitmap)Photo.Clone();

            textBox1.ReadOnly = false;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Photo = null;
            textBox1.Text = "";
            textBox1.ReadOnly = true;
            button3.Enabled = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Gesture g = new Gesture();
            g.Create(Photo, "hand" + name);
            Close();
        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (!File.Exists(Data.Path + "/hand" + textBox1.Text + ".jpg"))
            {
                name = textBox1.Text;
                button3.Enabled = true;
            }
            else button3.Enabled = false;
        }
    }
}
