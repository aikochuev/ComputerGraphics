using System;
using System.Drawing;
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
            bmp2 = bmp1;
            //fs.Close();
            CompressionAlgorithm.RunLengthEncodeBitmap(bmp2);
            //bmp1.Save("D:\\Documents\\Downloads\\rgb8bit\\flower_foveon_res.ppm");
            Clear();
        }
    }
}
