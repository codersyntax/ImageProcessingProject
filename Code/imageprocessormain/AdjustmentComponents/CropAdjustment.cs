using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ImageProcessorMain.AdjustmentComponents
{
    internal class CropAdjustment : IAdjustment
    {
        private ImageHandler m_ImageHandler;
        private ImageHub m_ImageHub;
        private Form m_CropDialog;
        private Label m_TrackBarValueLabel;
        private Button m_OkButton;
        private Label m_label = new Label();
        private Bitmap image;
        private PictureBox picBox;
        private Rectangle selection;

        internal CropAdjustment(ImageHandler imageHandler, ImageHub imageHub)
        {
            m_ImageHandler = imageHandler;
            m_ImageHub = imageHub;
            ShowDialog();
        }

        public Form ShowDialog()
        {
            image = (Bitmap)m_ImageHandler.CurrentBitmap;
            m_CropDialog = new Form();
            m_CropDialog.Text = "Adjust Crop";
            m_CropDialog.ShowIcon = false;
            m_CropDialog.ShowInTaskbar = false;
            m_CropDialog.MinimizeBox = false;
            m_CropDialog.MaximizeBox = false;
            m_CropDialog.StartPosition = FormStartPosition.CenterScreen;
            m_CropDialog.Size = new Size(200, 200);


            m_CropDialog.Controls.Add(m_label);
            m_label.Text = "Test";

            CreateDialogOkButton();
            m_CropDialog.Controls.Add(m_OkButton);
            m_CropDialog.Show();
            return m_CropDialog;
        }

        public void AdjustImage(int crop)
        {
            Cursor.Current = Cursors.WaitCursor;
            Bitmap temp = (Bitmap)m_ImageHandler.CurrentBitmap;
            Bitmap bmap = (Bitmap)temp.Clone();
            Bitmap bmp = image as Bitmap;

            // Check if it is a bitmap:
            if (bmp == null)
                throw new ArgumentException("No valid bitmap");

            // Crop the image:
            Bitmap cropBmp = bmp.Clone(selection, bmp.PixelFormat);

            // Release the resources:
            image.Dispose();

            m_ImageHandler.SetPreviousVersion();
            m_ImageHandler.CurrentBitmap = (Bitmap)bmap.Clone();
            UpdateImage();
            Cursor.Current = Cursors.Default;
        }

        public void UpdateImage()
        {
            m_ImageHub.CurrentImage.Image = m_ImageHandler.CurrentBitmap;
        }
        

        private void CreateDialogOkButton()
        {
            m_OkButton = new Button();
            m_OkButton.Left = (m_CropDialog.Width - m_OkButton.Width) / 2;
            m_OkButton.Top = ((m_CropDialog.Height - m_OkButton.Height) / 2) + 40;
            m_OkButton.Text = Constants.OkButtonText;
            m_OkButton.Click += new EventHandler(onOkButtonClicked);
        }

        private void onOkButtonClicked(object sender, EventArgs e)
        {
            AdjustImage(0);
            m_CropDialog.Dispose();
        }
    }
}






//using System;
//using System.Collections.Generic;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Forms;

//namespace ImageProcessorMain.AdjustmentComponents
//{
//    internal class CropAdjustment : IAdjustment
//    {
//        private ImageHandler m_ImageHandler;
//        private ImageHub m_ImageHub;
//        private Rectangle m_CropArea; //placeholder
//        private Form m_CropDialog;
//        private TrackBar m_CropSlider;
//        private Label m_TrackBarValueLabel;
//        private Button m_OkButton;

//        public CropAdjustment(ImageHandler ImageHandler, ImageHub ImageHub)
//        {
//            m_ImageHandler = ImageHandler;
//            m_ImageHub = ImageHub;
//        }

//        public void AdjustImage(int amount)
//        {
//            Cursor.Current = Cursors.WaitCursor;
//            Bitmap cropImage = Crop(m_ImageHandler.CurrentBitmap, m_CropArea);
//            m_ImageHandler.SetPreviousVersion();
//            m_ImageHandler.CurrentBitmap = cropImage;
//            UpdateImage();
//            Cursor.Current = Cursors.Default;
//        }



//        public Form ShowDialog()
//        {
//            m_CropDialog = new Form();
//            m_CropDialog.Text = "Adjust Crop";
//            m_CropDialog.ShowIcon = false;
//            m_CropDialog.ShowInTaskbar = false;
//            m_CropDialog.MinimizeBox = false;
//            m_CropDialog.MaximizeBox = false;
//            m_CropDialog.StartPosition = FormStartPosition.CenterScreen;
//            m_CropDialog.Size = new Size(200, 200);
//            CreateDialogTrackBar();
//            CreateDialogTrackbarLabel();
//            CreateDialogOkButton();
//            m_CropDialog.Controls.Add(m_CropSlider);
//            m_CropDialog.Controls.Add(m_TrackBarValueLabel);
//            m_CropDialog.Controls.Add(m_OkButton);
//            m_CropDialog.Show();
//            return m_CropDialog;
//        }

//        public void UpdateImage()
//        {
//            throw new NotImplementedException();
//        }

//        private void CreateDialogTrackBar()
//        {
//            m_CropSlider = new TrackBar();
//            m_CropSlider.Left = (m_CropDialog.Width - m_CropSlider.Width) / 2;
//            m_CropSlider.Top = ((m_CropDialog.Height - m_CropSlider.Height) / 2) - 40;
//            m_CropSlider.Maximum = 6;
//            m_CropSlider.Minimum = 0;
//            m_CropSlider.TickFrequency = 1;
//            m_CropSlider.ValueChanged += new EventHandler(onCropSliderValueChanged);
//        }

//        private void CreateDialogTrackbarLabel()
//        {
//            m_TrackBarValueLabel = new Label();
//            m_TrackBarValueLabel.TextAlign = ContentAlignment.MiddleCenter;
//            m_TrackBarValueLabel.Left = (m_CropDialog.Width - m_TrackBarValueLabel.Width) / 2;
//            m_TrackBarValueLabel.Top = ((m_CropDialog.Height - m_TrackBarValueLabel.Height) / 2) + 10;
//            m_TrackBarValueLabel.Text = m_CropSlider.Value.ToString();
//        }

//        private void CreateDialogOkButton()
//        {
//            m_OkButton = new Button();
//            m_OkButton.Left = (m_CropDialog.Width - m_OkButton.Width) / 2;
//            m_OkButton.Top = ((m_CropDialog.Height - m_OkButton.Height) / 2) + 40;
//            m_OkButton.Text = Constants.OkButtonText;
//            m_OkButton.Click += new EventHandler(onOkButtonClicked);
//        }

//        private void onOkButtonClicked(object sender, EventArgs e)
//        {
//            AdjustImage(m_CropSlider.Value);
//            m_CropDialog.Dispose();
//        }

//        private void onCropSliderValueChanged(object sender, EventArgs e)
//        {
//            m_TrackBarValueLabel.Text = m_CropSlider.Value.ToString();
//        }


//        //Sara's Code
//        public Bitmap Crop(Bitmap image, Rectangle selection)
//        {
//            Bitmap bmp = image;

//            // Check if it is a bitmap:
//            if (bmp == null)
//                throw new ArgumentException("No valid bitmap");

//            // Crop the image:
//            Bitmap cropBmp = bmp.Clone(selection, bmp.PixelFormat);

//            // Release the resources:
//            image.Dispose();

//            return cropBmp;
//        }

//    }
//}
