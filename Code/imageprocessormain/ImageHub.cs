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
            m_Image = image;
            return image;
        }

        private void onZoomClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (CurrentImage.Width > 40 && CurrentImage.Height > 40)
                {
                    m_Image.Width = Convert.ToInt32(m_Image.Width * 0.95);
                    m_Image.Height = Convert.ToInt32(m_Image.Height * 0.95);
                    m_Image.Refresh();
                    CurrentImage.Update();
                }
            }
            if(e.Button == MouseButtons.Left)
            {
                m_Image.Width = Convert.ToInt32(m_Image.Width * 1.05);
                m_Image.Height = Convert.ToInt32(m_Image.Height * 1.05);
                m_Image.Refresh();
                CurrentImage.Update();
            }
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
            CurrentImage.SizeMode = PictureBoxSizeMode.StretchImage;
            CurrentImage.MouseUp += onZoomClick;
        }
    }
}
