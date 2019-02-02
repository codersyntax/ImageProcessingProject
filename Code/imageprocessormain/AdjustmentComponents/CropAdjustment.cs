using System;
using System.Drawing;
using System.Windows.Forms;

namespace ImageProcessorMain.AdjustmentComponents
{
    internal class CropAdjustment
    {
        private ImageHandler m_ImageHandler;
        private ImageHub m_ImageHub;
        private bool isDown = false;
        private int initialX;
        private int initialY;
        private int width;
        private int height;
        private Graphics cropRect;
        private DialogResult m_CropDialog;
        private string m_DialogCaption = "Crop";
        private string m_DialogMessage = "Do you want to crop this area?";

        internal CropAdjustment(ImageHandler imageHandler, ImageHub imageHub)
        {
            m_ImageHandler = imageHandler;
            m_ImageHub = imageHub;
            if(m_ImageHub.CurrentImage != null)
            {
                CreateEventHandlers();
            }
        }

        public DialogResult ShowDialog(string message, string caption)
        {
            m_CropDialog = MessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            return m_CropDialog;
        }


        public void Crop(int xPosition, int yPosition, int width, int height)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (xPosition + width > m_ImageHandler.CurrentBitmap.Width)
                width = m_ImageHandler.CurrentBitmap.Width - xPosition;
            if (yPosition + height > m_ImageHandler.CurrentBitmap.Height)
                height = m_ImageHandler.CurrentBitmap.Height - yPosition;
            Rectangle rect = new Rectangle(xPosition, yPosition, width, height);
            m_ImageHandler.SetPreviousVersion();
            m_ImageHandler.CurrentBitmap = m_ImageHandler.CurrentBitmap.Clone(rect, m_ImageHandler.CurrentBitmap.PixelFormat);
            UpdateImage();
            Cursor.Current = Cursors.Default;
        }

        public void UpdateImage()
        {
            m_ImageHub.CurrentImage.Image = m_ImageHandler.CurrentBitmap;
        }

        private void ResizeImageWindow()
        {
            m_ImageHub.m_Image.Width = width;
            m_ImageHub.m_Image.Height = height;
        }

        private void ResetImageWindow()
        {
            m_ImageHub.CurrentImage.Invalidate();
        }

        private void Dispose()
        {
            m_ImageHub.CurrentImage.MouseDown -= Image_MouseDown;
            m_ImageHub.CurrentImage.MouseMove -= Image_MouseMove;
            m_ImageHub.CurrentImage.MouseUp -= Image_MouseUp;
        }

        private void CreateEventHandlers()
        {
            m_ImageHub.CurrentImage.MouseDown += new MouseEventHandler(Image_MouseDown);
            m_ImageHub.CurrentImage.MouseMove += new MouseEventHandler(Image_MouseMove);
            m_ImageHub.CurrentImage.MouseUp += new MouseEventHandler(Image_MouseUp);
        }

        private void Image_MouseDown(object sender, MouseEventArgs e)
        {
            isDown = true;
            initialX = e.X;
            initialY = e.Y;
        }

        private void Image_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDown == true)
            {
                m_ImageHub.CurrentImage.Refresh();
                width = e.X - initialX;
                height = e.Y - initialY;
                width = width * Math.Sign(width);
                height = height * Math.Sign(height);
                cropRect = m_ImageHub.CurrentImage.CreateGraphics();
                cropRect.DrawRectangle(new Pen(Color.White, 2), new Rectangle(initialX, initialY, width, height));
            }
        }
        private void Image_MouseUp(object sender, MouseEventArgs e)
        {
            isDown = false;
            var dialogResult = ShowDialog(m_DialogMessage, m_DialogCaption);
            if (dialogResult == DialogResult.Yes)
            {
                Crop(initialX, initialY, width, height);
                //ResizeImageWindow();
            }
            else if (dialogResult == DialogResult.No)
            {
                ResetImageWindow();
            }

            Dispose();
        }
    }
}