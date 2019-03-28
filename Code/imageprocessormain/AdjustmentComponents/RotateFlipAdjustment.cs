using System;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace ImageProcessorMain.AdjustmentComponents
{
    internal class RotateFlipAdjustment
    {
        private ImageHandler m_ImageHandler;
        private ImageHub m_ImageHub;
        private Form m_RotateDialog;
        private Button m_RotateButton;
        
        private TextBox m_AngleTextbox; 
        public RotateFlipAdjustment(ImageHandler imageHandler, ImageHub imageHub)
        {
            m_ImageHandler = imageHandler;
            m_ImageHub = imageHub;
            ShowDialog();
        }

        public void AdjustImage(float angle)
        {
            Bitmap temp = (Bitmap)m_ImageHandler.CurrentBitmap;
            Bitmap bmap = (Bitmap)temp.Clone();
            Matrix rotate_at_origin = new Matrix();
            rotate_at_origin.Rotate(angle);
            PointF[] points =
             {
                new PointF(0, 0),
                new PointF(temp.Width, 0),
                new PointF(temp.Width, temp.Height),
                new PointF(0, temp.Height),
            };
            rotate_at_origin.TransformPoints(points);
            int wid = temp.Width;
            int hgt = temp.Height;
            bmap = new Bitmap(wid, hgt);
            Matrix rotate_at_center = new Matrix();
            rotate_at_center.RotateAt(angle,
                new PointF(wid / 2f, hgt / 2f));
            using (Graphics gr = Graphics.FromImage(bmap))
            {
                gr.InterpolationMode = InterpolationMode.High;
                gr.Clear(bmap.GetPixel(0, 0));
                gr.Transform = rotate_at_center;
                int x = (wid - bmap.Width) / 2;
                int y = (hgt - bmap.Height) / 2;
                gr.DrawImage(bmap, x, y);

            }
            m_ImageHandler.SetPreviousVersion();
            m_ImageHandler.CurrentBitmap = bmap;
            UpdateImage();


        }

        public Form ShowDialog()
        {
            m_RotateDialog = new Form();
            m_RotateDialog.Text = "Rotate Image";
            m_RotateDialog.ShowIcon = false;
            m_RotateDialog.ShowInTaskbar = false;
            m_RotateDialog.MinimizeBox = false;
            m_RotateDialog.MaximizeBox = false;
            m_RotateDialog.StartPosition = FormStartPosition.CenterScreen;
            m_RotateDialog.Size = new Size(200, 200);
            CreateDialogRotateButton();
           
            m_AngleTextbox = new TextBox();
            m_RotateDialog.Controls.Add(m_AngleTextbox);
            m_RotateDialog.Controls.Add(m_RotateButton);
            m_RotateDialog.Show();
            return m_RotateDialog;
        }
        
            
        
      

            public void UpdateImage()
            {
                m_ImageHub.CurrentImage.Image = m_ImageHandler.CurrentBitmap;
            }
       
        private void CreateDialogRotateButton()
        {
            m_RotateButton = new Button();
            m_RotateButton.Left = (m_RotateDialog.Width - m_RotateButton.Width) / 2;
            m_RotateButton.Top = ((m_RotateDialog.Height - m_RotateButton.Height) / 2) + 40;
            m_RotateButton.Text = Constants.OkButtonText;
            m_RotateButton.Click += new EventHandler(onOkButtonClicked);
        }

        private void onOkButtonClicked(object sender, EventArgs e)
        {
            float angle = float.Parse(m_AngleTextbox.Text);
            AdjustImage(angle);
            UpdateImage();
            m_RotateDialog.Dispose();


        }
      
    }
}