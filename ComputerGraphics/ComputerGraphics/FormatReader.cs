using System;
using System.IO;
using System.Drawing;

namespace ComputerGraphics
{
    class FormatReader
    {
        public static Bitmap ReadBitmapFromPpm(string file)
        {
            var reader = new BinaryReader(new FileStream(file, FileMode.Open));
            if (reader.ReadChar() != 'P' || reader.ReadChar() != '6')
                return null;
            reader.ReadChar(); //Eat newline
            string widths = "", heights = "";
            char temp;
            while ((temp = reader.ReadChar()) != ' ')
                widths += temp;
            while ((temp = reader.ReadChar()) >= '0' && temp <= '9')
                heights += temp;
            if (reader.ReadChar() != '2' || reader.ReadChar() != '5' || reader.ReadChar() != '5')
                return null;
            reader.ReadChar(); //Eat the last newline
            int width = int.Parse(widths),
                height = int.Parse(heights);
            Bitmap bitmap = new Bitmap(width, height);
            //Read in the pixels
            for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++)
                    bitmap.SetPixel(x, y, Color.FromArgb(reader.ReadByte(), reader.ReadByte(), reader.ReadByte()));
            return bitmap;
        }
        static string NextAnyLine(BinaryReader br)
        {
            string s = "";
            byte b = 0; // dummy
            while (b != 10) // newline
            {
                b = br.ReadByte();
                char c = (char)b;
                s += c;
            }
            return s.Trim();
        }

        static string NextNonCommentLine(BinaryReader br)
        {
            string s = NextAnyLine(br);
            while (s.StartsWith("#") || s == "")
                s = NextAnyLine(br);
            return s;
        }

        public static Bitmap ReadBitmapFromPgm(string file)
        {
            FileStream ifs = new FileStream(file, FileMode.Open);
            BinaryReader br = new BinaryReader(ifs);

            string magic = NextNonCommentLine(br);
            if (magic != "P5")
                throw new Exception("Unknown magic number: " + magic);

            string widthHeight = NextNonCommentLine(br);
            string[] tokens = widthHeight.Split(' ');
            int width = int.Parse(tokens[0]);
            int height = int.Parse(tokens[1]);

            string sMaxVal = NextNonCommentLine(br);
            int maxVal = int.Parse(sMaxVal);

            // read width * height pixel values . . .
            byte[][] pixels = new byte[height][];
            for (int i = 0; i < height; ++i)
                pixels[i] = new byte[width];

            for (int i = 0; i < height; ++i)
                for (int j = 0; j < width; ++j)
                    pixels[i][j] = br.ReadByte();

            br.Close(); ifs.Close();

            Bitmap result = new Bitmap(width, height/*, PixelFormat.Format8bppIndexed*/);
            Graphics gr = Graphics.FromImage(result);
            for (int i = 0; i < height; ++i)
            {
                for (int j = 0; j < width; ++j)
                {
                    int pixelColor = pixels[i][j];
                    Color c = Color.FromArgb(pixelColor, pixelColor, pixelColor);
                    SolidBrush sb = new SolidBrush(c);
                    gr.FillRectangle(sb, j, i, 1, 1);
                }
            }
            return result;
        }
    }
}
