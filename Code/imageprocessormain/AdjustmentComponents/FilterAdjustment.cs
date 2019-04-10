using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Drawing.Drawing2D;

namespace ImageProcessorMain.AdjustmentComponents
{
    internal class FilterAdjustment
    {
        private ImageHandler m_ImageHandler;
        private ImageHub m_ImageHub;
        private Form m_FilterDialog;
        private TrackBar m_BlueSlider;
        private TrackBar m_GreenSlider;
        private TrackBar m_RedSlider;
        private Label m_RedTrackBarValueLabel;
        private Label m_GreenBarValueLabel;
        private Label m_RedBarValueLabel;
        private Button m_OkButton;
  

        internal FilterAdjustment(ImageHandler imageHandler, ImageHub imageHub)
        {
            m_ImageHandler = imageHandler;
            m_ImageHub = imageHub;
            ShowDialog();
        }
        public void AdjustImage()
        {
            Cursor.Current = Cursors.WaitCursor;
            Bitmap filteredImage = ColorTint(m_ImageHandler.CurrentBitmap, m_BlueSlider.Value, m_GreenSlider.Value, 255);
            m_ImageHandler.SetPreviousVersion();
            m_ImageHandler.CurrentBitmap = filteredImage;
            UpdateImage();
            Cursor.Current = Cursors.Default;
        }

        public Form ShowDialog()
        {
            m_FilterDialog = new Form();
            m_FilterDialog.Text = "Filter Select";
            m_FilterDialog.ShowIcon = false;
            m_FilterDialog.ShowInTaskbar = false;
            m_FilterDialog.MinimizeBox = false;
            m_FilterDialog.MaximizeBox = false;
            m_FilterDialog.StartPosition = FormStartPosition.CenterScreen;
            m_FilterDialog.Size = new Size(200, 230);
            CreateDialogBlueTrackBar();
            CreateDialogBlueTrackbarLabel();
            CreateDialogGreenTrackBar();
            CreateDialogGreenTrackbarLabel();
            CreateDialogRedTrackBar();
            CreateDialogRedTrackbarLabel();
            CreateDialogOkButton();

            m_FilterDialog.Controls.Add(m_BlueSlider);
            m_FilterDialog.Controls.Add(m_RedTrackBarValueLabel);
            m_FilterDialog.Controls.Add(m_GreenSlider);
            m_FilterDialog.Controls.Add(m_GreenBarValueLabel);
            m_FilterDialog.Controls.Add(m_RedSlider);
            m_FilterDialog.Controls.Add(m_RedBarValueLabel);
            m_FilterDialog.Controls.Add(m_OkButton);

            m_FilterDialog.Show();
            return m_FilterDialog;
        }

        public void UpdateImage()
        {
            m_ImageHub.CurrentImage.Image = m_ImageHandler.CurrentBitmap;
        }

        private void CreateDialogBlueTrackBar()
        {
            m_BlueSlider = new TrackBar();
            m_BlueSlider.Left = 10;
            m_BlueSlider.Top = 5;
            m_BlueSlider.Maximum = 255;
            m_BlueSlider.Minimum = 0;
            m_BlueSlider.TickFrequency = 25;
            m_BlueSlider.ValueChanged += new EventHandler(onBlueSliderValueChanged);
        }

        private void CreateDialogGreenTrackBar()
        {
            m_GreenSlider = new TrackBar();
            m_GreenSlider.Left = 10;
            m_GreenSlider.Top = 60;
            m_GreenSlider.Maximum = 255;
            m_GreenSlider.Minimum = 0;
            m_GreenSlider.TickFrequency = 25;
            m_GreenSlider.ValueChanged += new EventHandler(onGreenSliderValueChanged);
        }

        private void CreateDialogRedTrackBar()
        {
            m_RedSlider = new TrackBar();
            m_RedSlider.Left = 10;
            m_RedSlider.Top = 115;
            m_RedSlider.Maximum = 255;
            m_RedSlider.Minimum = 0;
            m_RedSlider.TickFrequency = 25;
            m_RedSlider.ValueChanged += new EventHandler(onRedSliderValueChanged);
        }

        private void CreateDialogBlueTrackbarLabel()
        {
            m_RedTrackBarValueLabel = new Label();
            m_RedTrackBarValueLabel.Left = 130;
            m_RedTrackBarValueLabel.Top = 25;
            m_RedTrackBarValueLabel.Text = "Blue: "+m_BlueSlider.Value.ToString();
        }

        private void CreateDialogGreenTrackbarLabel()
        {
            m_GreenBarValueLabel = new Label();
            m_GreenBarValueLabel.Left = 130;
            m_GreenBarValueLabel.Top = 80;
            m_GreenBarValueLabel.Text = "Green: " + m_GreenSlider.Value.ToString();
        }

        private void CreateDialogRedTrackbarLabel()
        {
            m_RedBarValueLabel = new Label();
            m_RedBarValueLabel.Left = 130;
            m_RedBarValueLabel.Top = 135;
            m_RedBarValueLabel.Text = "Red: " + m_RedSlider.Value.ToString();
        }

        private void CreateDialogOkButton()
        {
            m_OkButton = new Button();
            m_OkButton.Left = 100;
            m_OkButton.Top = 165;
            m_OkButton.Text = Constants.OkButtonText;
            m_OkButton.Click += new EventHandler(onOkButtonClicked);
        }

        private void onOkButtonClicked(object sender, EventArgs e)
        {
             AdjustImage();
            m_FilterDialog.Dispose();
        }

        private void onBlueSliderValueChanged(object sender, EventArgs e)
        {
            m_RedTrackBarValueLabel.Text = "Blue: "+ m_BlueSlider.Value.ToString();
            Bitmap filteredImage = ColorTint(m_ImageHandler.CurrentBitmap, m_BlueSlider.Value, m_GreenSlider.Value, 255);
            m_ImageHandler.SetPreviousVersion();
            m_ImageHandler.CurrentBitmap = filteredImage;
            UpdateImage();
        }

        private void onGreenSliderValueChanged(object sender, EventArgs e)
        {
            m_GreenBarValueLabel.Text = "Green: "+ m_GreenSlider.Value.ToString();
        }

        private void onRedSliderValueChanged(object sender, EventArgs e)
        {
            m_RedBarValueLabel.Text = "Red: " + m_RedSlider.Value.ToString();
        }

        private Bitmap ColorTint( Bitmap sourceBitmap, float blueTint, float greenTint, float redTint)
        {
            BitmapData sourceData = sourceBitmap.LockBits(new Rectangle(0, 0,
                                    sourceBitmap.Width, sourceBitmap.Height),
                                    ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            byte[] pixelBuffer = new byte[sourceData.Stride * sourceData.Height];

            Marshal.Copy(sourceData.Scan0, pixelBuffer, 0, pixelBuffer.Length);

            sourceBitmap.UnlockBits(sourceData);

            float blue = 0;
            float green = 0;
            float red = 0;

            for (int k = 0; k + 4 < pixelBuffer.Length; k += 4)
            {
                blue = pixelBuffer[k] + (255 - pixelBuffer[k]) * blueTint;
                green = pixelBuffer[k + 1] + (255 - pixelBuffer[k + 1]) * greenTint;
                red = pixelBuffer[k + 2] + (255 - pixelBuffer[k + 2]) * redTint;

                if (blue > 255)
                { blue = 255; }

                if (green > 255)
                { green = 255; }

                if (red > 255)
                { red = 255; }

                pixelBuffer[k] = (byte)blue;
                pixelBuffer[k + 1] = (byte)green;
                pixelBuffer[k + 2] = (byte)red;

            }

            Bitmap resultBitmap = new Bitmap(sourceBitmap.Width, sourceBitmap.Height);

            BitmapData resultData = resultBitmap.LockBits(new Rectangle(0, 0,
                                    resultBitmap.Width, resultBitmap.Height),
                                    ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

            Marshal.Copy(pixelBuffer, 0, resultData.Scan0, pixelBuffer.Length);
            resultBitmap.UnlockBits(resultData);

            return resultBitmap;
        }

    }

}
