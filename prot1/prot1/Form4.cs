using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace prot1
{
    public partial class Form4 : Form
    {
        Gesture g = new Gesture();
        string name;

        public Form4()
        {
            InitializeComponent();

            pictureBox1.Image = new Bitmap(Data.Path + "/system/no_photo.png");

            foreach (string name in g.Gestures_name)
            {
                comboBox1.Items.Add(name);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex > -1)
            {
                name = g.Gestures_name[comboBox1.SelectedIndex];
                pictureBox1.Image = g.Gestures[comboBox1.SelectedIndex];

                button3.Enabled = true;
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            File.Delete(Data.Path + "/" + name + ".jpg");
            name = null;
            comboBox1.SelectedItem = null;

            pictureBox1.Image = new Bitmap(Data.Path + "/system/no_photo.png");

            comboBox1.Items.Clear();

            Gesture.Update_Base();

            foreach (string name in g.Gestures_name)
            {
                comboBox1.Items.Add(name);
            }

            button3.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            name = null;
            comboBox1.SelectedItem = null;

            button3.Enabled = false;
        }
    }
}
