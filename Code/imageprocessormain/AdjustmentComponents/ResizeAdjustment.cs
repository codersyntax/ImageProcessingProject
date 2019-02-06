using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;



namespace ImageProcessorMain.AdjustmentComponents
{
    internal class ResizeAdjustment : IAdjustment
    {
        Image img;
        string[] m_exten = { ".PNG", ".JPEG", ".JPG", "GIF" };
        public void AdjustImage()
        {
            // int X1, int Y1, int Width, int Height
            //    string fileName = @"C:\testimage.jpg";
            //      using (Image image = Image.FromFile(fileName))
            //        {
            //           using (Graphics graphic = Graphics.FromImage(image))
            //           {
            //                // <comment>Crop and resize the image.</comment>
            //                Rectangle destination = new Rectangle(0, 0, Width, Height);
            //                graphic.DrawImage(image, destination, X1, Y1, Width, Height, GraphicsUnit.Pixel);
            //           }
            //            image.Save(@"C:\testimagea.jpg");
            //        }
            //    throw new NotImplementedException();
        }

        pulbic Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
          for (int i=0; i<m_exten.Length; i++)
        }

        public void UpdateImage()
        {
            throw new NotImplementedException();
        }


        public Form ShowDialog()
        {

            throw new NotImplementedException();
        }


    }
}
