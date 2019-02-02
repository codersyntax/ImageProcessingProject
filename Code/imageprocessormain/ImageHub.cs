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
            m_Image = new Form();
            m_Image.Text = m_ImageHandler.BitmapPath;
            m_Image.Size = new Size(m_ImageHandler.CurrentBitmap.Width, m_ImageHandler.CurrentBitmap.Height);
            m_Image.StartPosition = FormStartPosition.Manual;
            m_Image.Location = new Point(136, 0);
            m_Image.FormBorderStyle = FormBorderStyle.FixedSingle;
            m_Image.ShowIcon = false;
            m_Image.BackColor = Color.DarkGray;
            CreateImageContainer();
            m_Image.Controls.Add(CurrentImage);
            m_Image.FormClosed += new FormClosedEventHandler(onImageClose);
            m_Image.Show();
            return m_Image;
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
