using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;
using ZXing;


namespace aforgewebkamera
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        FilterInfoCollection fico;
        VideoCaptureDevice vcd;


        private void Form1_Load(object sender, EventArgs e)
        {
            fico = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo f  in fico)
            {
                comboBox1.Items.Add(f.Name);    
                comboBox1.SelectedIndex = 0;    
                  



            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            vcd = new VideoCaptureDevice(fico[comboBox1.SelectedIndex].MonikerString);
            vcd.NewFrame += Vcd_NewFrame;
            vcd.Start();
            timer1.Start();


        }

        private void Vcd_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            pictureBox1.Image=(Bitmap)eventArgs.Frame.Clone();  

        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog sv=new SaveFileDialog();
            sv.Filter = "(*.jpg)|*.jpg";
            DialogResult result = sv.ShowDialog();
            if(result == DialogResult.OK)
            {
                pictureBox1.Image.Save(sv.FileName);    

            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(pictureBox1 == null)
            {

                BarcodeReader barcodeReader = new BarcodeReader();
                Result sonuc = barcodeReader.Decode((Bitmap)pictureBox1.Image);
                if(sonuc != null)
                {
                    richTextBox1.Text=sonuc.ToString();
                    timer1.Stop();


                }   

            }

        }
    }
}
