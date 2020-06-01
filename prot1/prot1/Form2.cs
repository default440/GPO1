using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prot1
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();

            Path = Data.Path;

            this.Red = Data.Red;
            this.Green = Data.Green;
            this.Blue = Data.Blue;

            this.Similarity_limit = Data.Similarity_limit;

            trackBar1.Value = Convert.ToInt32(this.Red * trackBar1.Maximum);
            trackBar2.Value = Convert.ToInt32(this.Green * trackBar2.Maximum);
            trackBar3.Value = Convert.ToInt32(this.Blue * trackBar3.Maximum);

            trackBar4.Value = Convert.ToInt32(this.Similarity_limit * trackBar4.Maximum);

            textBox1.Text = Path;
            folderBrowserDialog1.SelectedPath = Path;
        }

        public float Red, Green, Blue;
        public static string Path;
        public float Similarity_limit;

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            Red = Convert.ToSingle(trackBar1.Value) / Convert.ToSingle(trackBar1.Maximum);
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            Green = Convert.ToSingle(trackBar2.Value) / Convert.ToSingle(trackBar2.Maximum);
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            Blue = Convert.ToSingle(trackBar3.Value) / Convert.ToSingle(trackBar3.Maximum);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            folderBrowserDialog1.SelectedPath = textBox1.Text;
            Path = textBox1.Text;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            Path = folderBrowserDialog1.SelectedPath;
            textBox1.Text = Path;
        }

        private void trackBar4_Scroll(object sender, EventArgs e)
        {
            Similarity_limit = Convert.ToSingle(trackBar4.Value) / Convert.ToSingle(trackBar4.Maximum);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Data.Default();

            Path = Data.Path;

            this.Red = Data.Red;
            this.Green = Data.Green;
            this.Blue = Data.Blue;

            this.Similarity_limit = Data.Similarity_limit;

            trackBar1.Value = Convert.ToInt32(this.Red * trackBar1.Maximum);
            trackBar2.Value = Convert.ToInt32(this.Green * trackBar2.Maximum);
            trackBar3.Value = Convert.ToInt32(this.Blue * trackBar3.Maximum);

            trackBar4.Value = Convert.ToInt32(this.Similarity_limit * trackBar4.Maximum);

            textBox1.Text = Path;
            folderBrowserDialog1.SelectedPath = Path;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Data.Path = Path;
            
            Data.Red = Red;
            Data.Green = Green;
            Data.Blue = Blue;

            Data.Similarity_limit = Similarity_limit;

            //this.Close();
        }
    }
}
