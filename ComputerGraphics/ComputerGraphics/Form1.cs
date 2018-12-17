using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace ComputerGraphics
{
    public partial class Form1 : Form
    {
        Bitmap bmp1;
        Bitmap bmp2;
        //Обновление всей информации при открытии нового файла
        void Clear()
        {
            pictureBox1.Image = bmp1;
            pictureBox2.Image = bmp2;
        }

        void ComputeMetrics()
        {
            FileInfo file1 = new FileInfo(@".\\bmp1_res.png");
            FileInfo file2 = new FileInfo(@".\\bmp2_res.png");

            label4.Text = Math.Round((double)file1.Length/1024).ToString()+" B";
            label5.Text = Math.Round((double)file2.Length/1024).ToString()+" B";
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void panel1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
            //e.Effect = DragDropEffects.Copy;
        }

        private void panel1_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            //System.IO.FileStream fs = new System.IO.FileStream(files[0], System.IO.FileMode.Open);
            //PgmImage pgm = new PgmImage(files[0]);
            //bmp1 = new Bitmap(PgmImage.MakeBitmap(pgm.pgmImage, 1));

            bmp1 = new Bitmap(FormatReader.ReadBitmapFromPpm(files[0]));
            //fs.Close();
            Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //bmp2.Dispose();
            bmp2 = bmp1;
            bmp1.Save(".\\bmp1_res.png", ImageFormat.Png);
            CompressionAlgorithm.RunLengthEncodeBitmap(bmp2);
            //bmp1.Save("D:\\Documents\\Downloads\\rgb8bit\\flower_foveon_res.ppm");
            bmp1.Save(".\\bmp2_res.png", ImageFormat.Png);

            Clear();
            ComputeMetrics();
        }
    }
}
