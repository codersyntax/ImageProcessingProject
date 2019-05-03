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
        public bool ZoomEnabled = false;

        public ImageHub(Form mainForm, ImageHandler imageHandler)
        {
            m_MainForm = mainForm;
            m_ImageHandler = imageHandler;
        }

        public void CreateImageForm()
        {
            m_Image = new Form();
            m_Image.Text = m_ImageHandler.BitmapPath;
            m_Image.Size = new Size(Screen.PrimaryScreen.Bounds.Width - 150, Screen.PrimaryScreen.Bounds.Height - 100);
            m_Image.StartPosition = FormStartPosition.Manual;
            m_Image.Location = new Point(75, 0);
            m_Image.AutoScroll = true;
            m_Image.FormBorderStyle = FormBorderStyle.FixedSingle;
            m_Image.ShowIcon = false;
            m_Image.BackColor = Color.DarkGray;
            CreateImageContainer();
            m_Image.Controls.Add(CurrentImage);
            m_Image.FormClosed += new FormClosedEventHandler(onImageClose);
            m_Image.Show();
        }

        private void onZoomClick(object sender, MouseEventArgs e)
        {
            if (ZoomEnabled)
            {
                if (e.Button == MouseButtons.Right)
                {
                    if (CurrentImage.Width > 40 && CurrentImage.Height > 40)
                    {
                        CurrentImage.Width = Convert.ToInt32(CurrentImage.Width * 0.95);
                        CurrentImage.Height = Convert.ToInt32(CurrentImage.Height * 0.95);
                        CurrentImage.Refresh();
                        CurrentImage.Update();
                    }
                }
                if (e.Button == MouseButtons.Left)
                {
                    CurrentImage.Width = Convert.ToInt32(CurrentImage.Width * 1.05);
                    CurrentImage.Height = Convert.ToInt32(CurrentImage.Height * 1.05);
                    CurrentImage.Refresh();
                    CurrentImage.Update();
                }
            }
        }

        private void onImageClose(object sender, EventArgs e)
        {
            m_ImageHandler.BitmapPath = null;
            m_ImageHandler.OriginalBitmap = null;
            m_ImageHandler.CurrentBitmap = null;
            m_ImageHandler.undoButtonStack = null;
        }

        private void CreateImageContainer()
        {
            CurrentImage = new PictureBox();
            //CurrentImage.Dock = DockStyle.Fill;
            CurrentImage.Left = m_Image.Left;
            CurrentImage.Top = m_Image.Top;
            CurrentImage.Width = m_Image.Width;
            CurrentImage.Height = m_Image.Height;
            CurrentImage.Image = m_ImageHandler.CurrentBitmap;
            CurrentImage.SizeMode = PictureBoxSizeMode.Zoom;
            CurrentImage.MouseUp += onZoomClick;
        }
    }
}
