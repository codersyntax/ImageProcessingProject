using System;
using System.Drawing;
using System.Windows.Forms;

namespace ImageProcessorMain
{
    public class ImageHub
    {
        private Form m_MainForm;
        private ImageHandler m_ImageHandler;
        public Form m_Image;
        public PictureBox CurrentImage;

        public ImageHub(Form mainForm, ImageHandler imageHandler)
        {
            m_MainForm = mainForm;
            m_ImageHandler = imageHandler;
        }

        public Form CreateImageForm()
        {
            Form image = new Form();
            image.Text = m_ImageHandler.BitmapPath;
            image.Size = new Size(m_ImageHandler.CurrentBitmap.Width, m_ImageHandler.CurrentBitmap.Height);
            image.StartPosition = FormStartPosition.Manual;
            image.Location = new Point(136, 0);
            image.FormBorderStyle = FormBorderStyle.FixedSingle;
            image.ShowIcon = false;
            image.BackColor = Color.DarkGray;
            CreateImageContainer();
            image.Controls.Add(CurrentImage);
            image.FormClosed += new FormClosedEventHandler(onImageClose);
            image.Show();
            return image;
        }

        private void onImageClose(object sender, EventArgs e)
        {
            m_ImageHandler.BitmapPath = null;
        }

        private void CreateImageContainer()
        {
            CurrentImage = new PictureBox();
            CurrentImage.Dock = DockStyle.Fill;
            CurrentImage.Image = m_ImageHandler.CurrentBitmap;
        }
    }
}
