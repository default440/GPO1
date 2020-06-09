using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace prot1
{
    public partial class Form1 : Form
    {
        Form2 Settings;
        Form3 Create;
        Form4 Delete;

        public Form1()
        {
            InitializeComponent();
            Data.Update_frame += video_NewFrame;
            Data.video_Start();
        }

        private Gesture searcher = new Gesture(Data.Path, Data.Similarity_limit);

        private void video_NewFrame()
        {
            Bitmap img = (Bitmap)Data.Image_original.Clone();

            pictureBox1.Image = (Bitmap)img.Clone();
            try
            {
                string gest = searcher.Search((Bitmap)Data.Image_ready.Clone());

                this.textBox1.BeginInvoke((MethodInvoker)(() => this.textBox1.Text = textBox1.Text + gest));
                this.label1.BeginInvoke((MethodInvoker)(() => this.label1.Text = gest));

                if (gest != "")
                {
                    Thread.Sleep(1000);
                }
            }
            catch
            {

            }
        }

        private void pictureBox1_Click(object sender, EventArgs e) {}

        private void открытьНастройкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings = new Form2();
            Settings.TopLevel = true;
            Settings.Show();
        }

        private void добавитьЖестToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Create = new Form3();
            Create.TopLevel = true;

            Create.Show();
        }

        private void удалитьЖестToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Delete = new Form4();
            Delete.TopLevel = true;
            Delete.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Data.CloseVideoSource();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
