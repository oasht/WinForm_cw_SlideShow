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

namespace WinForm_cw_SlideShow
{
    public partial class Form1 : Form
    {
        Timer timer = new Timer();
        List<Bitmap> images = new List<Bitmap>();
        int np=0;
        public Form1()
        {
            InitializeComponent();
            timer.Interval = 500;
            timer.Tick += next_Click;
            label1.Text = $"{np}/{images.Count}";             
        }

        private void next_Click(object sender, EventArgs e)
        {
            if (images.Count == 0)
            {
                return;
            }
            
            np++;
            
            if (np >= images.Count)
            {
                np = 0;
            }
            label1.Text = $"{np + 1}/{images.Count}";
            pictureBox1.Image = images[np];
        }

        

        private void button_start_Click(object sender, EventArgs e)
        {
            if (images.Count!=0)
            {
                timer.Start();
            }
            else
            {
                MessageBox.Show("Нету картинок");
            }
        }

        private void button_stop_Click(object sender, EventArgs e)
        {
            timer.Stop();
        }

        private void button_prev_Click(object sender, EventArgs e)
        {
            if (images.Count == 0)
            {
                return;
            }
           
            np--;
            
            if (np<0)
            {
                np = images.Count - 1;
            }
            label1.Text = $"{np + 1}/{images.Count}";
            pictureBox1.Image = images[np];
        }

        private void button_open_Click(object sender, EventArgs e)
        {
            button_stop_Click(null,null);
            FolderBrowserDialog folder = new FolderBrowserDialog();
            if (folder.ShowDialog()== DialogResult.OK)
            {
                timer.Stop();
                if (images.Count!=0)
                {
                    foreach (var item in images)
                    {
                        item.Dispose();
                    }
                    images.Clear();
                }
                DirectoryInfo direct = new DirectoryInfo(folder.SelectedPath);
                IEnumerable<FileInfo> all_file = direct.EnumerateFiles();
                foreach (var item in all_file)
                {
                    Bitmap pt = new Bitmap(item.FullName);
                    Size pt_size = pictureBox1.Size;
                    images.Add(new Bitmap(pt,pt_size));
                }
                pictureBox1.Image = images[np];
                label1.Text = $"{np + 1}/{images.Count}";
            }
        }

        private void button_next_Click(object sender, EventArgs e)
        {
            if (images.Count == 0)
            {
                return;
            }
           
            np++;
           
            if (np >= images.Count)
            {
                np =0;
            }
            label1.Text = $"{np + 1}/{images.Count}";
            pictureBox1.Image = images[np];
        }
    }
}
