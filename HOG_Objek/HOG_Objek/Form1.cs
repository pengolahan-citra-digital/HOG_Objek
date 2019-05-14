using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.Util;
using Emgu.CV.CvEnum;

namespace HOG_Objek
{
    public partial class Form1 : Form
    {
        Capture capture;
        bool captureprogress;
        public Form1()
        {
            InitializeComponent();
        }

      

        private void prosesframe(object sender, EventArgs e)
        {
            Image<Bgr, Byte> imageFrame = capture.QueryFrame();

             long processingTime;
            Rectangle[] result = FindPedestrian.Find(imageFrame, out processingTime);
            foreach (Rectangle rect in result)
            {
                imageFrame.Draw(rect, new Bgr(Color.Blue), 1);
            }
              imageBox1.Image = imageFrame;
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            if (capture == null)
            {
                try
                {
                    capture = new Capture();
                }
                catch (NullReferenceException exc)
                {
                    MessageBox.Show("camera tidak aktif");
                }
            }

            if (capture != null)
            {
                if (captureprogress)
                {
                    Application.Idle -= prosesframe;
                }
                else
                {
                    Application.Idle += prosesframe;
                }
                captureprogress = !captureprogress;
            }
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            
        }
    }
}

