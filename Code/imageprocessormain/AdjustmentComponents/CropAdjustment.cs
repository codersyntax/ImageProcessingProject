using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageProcessorMain.AdjustmentComponents
{
    internal class CropAdjustment : IAdjustment
    {
        private ImageHandler m_ImageHandler;
        private ImageHub m_ImageHub;
        private Rectangle m_CropArea; //placeholder

        public CropAdjustment(ImageHandler ImageHandler, ImageHub ImageHub)
        {
            m_ImageHandler = ImageHandler;
            m_ImageHub = ImageHub;
        }

        public void AdjustImage(int amount)
        {
            Cursor.Current = Cursors.WaitCursor;
            Bitmap cropImage = Crop(m_ImageHandler.CurrentBitmap, m_CropArea);
            m_ImageHandler.SetPreviousVersion();
            m_ImageHandler.CurrentBitmap = cropImage;
            UpdateImage();
            Cursor.Current = Cursors.Default;
        }



        public Form ShowDialog()
        {
            throw new NotImplementedException();
        }

        public void UpdateImage()
        {
            throw new NotImplementedException();
        }

        public Bitmap Crop(Bitmap image, Rectangle selection)
        {
            Bitmap bmp = image;

            // Check if it is a bitmap:
            if (bmp == null)
                throw new ArgumentException("No valid bitmap");

            // Crop the image:
            Bitmap cropBmp = bmp.Clone(selection, bmp.PixelFormat);

            // Release the resources:
            image.Dispose();

            return cropBmp;
        }

    }
}
