using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace ImageProcessorMain.AdjustmentComponents
{
    internal class ResizeAdjustment : IAdjustment
    {
        public void AdjustImage(int size)
        {
            ResizeImage(0, 0, 200, 200);
            ///<summary> 
            ///    i think to resize my image method will need three arguments both my Adjustment method is accepting only one argument return
            ///</summary>
            throw new NotImplementedException();
        }
        public static void ResizeImage(int X1, int Y1, int Width, int Height)
        {

            string fileName = @"C:\testimage.jpg";
            using (Image image = Image.FromFile(fileName))
            {
                using (Graphics graphic = Graphics.FromImage(image))
                {
                    // Crop and resize the image.
                    Rectangle destination = new Rectangle(0, 0, Width, Height);
                    graphic.DrawImage(image, destination, X1, Y1, Width, Height, GraphicsUnit.Pixel);
                }
                image.Save(@"C:\testimagea.jpg");
            }
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
