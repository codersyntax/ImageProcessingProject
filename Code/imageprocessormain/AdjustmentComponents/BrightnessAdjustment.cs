using System;
using System.Drawing;
using System.Windows.Forms;

namespace ImageProcessorMain.AdjustmentComponents
{
    internal class BrightnessAdjustment : IAdjustment
    {
        private ImageHandler m_ImageHandler;
        private ImageHub m_ImageHub;
        private Form m_BrightnessDialog;
        private TrackBar m_BrightnessSlider;
        private Label m_TrackBarValueLabel;
        private Button m_OkButton;

        internal BrightnessAdjustment(ImageHandler imageHandler, ImageHub imageHub)
        {
            m_ImageHandler = imageHandler;
            m_ImageHub = imageHub;
            ShowDialog();
        }

        public Form ShowDialog()
        {
            m_BrightnessDialog = new Form();
            m_BrightnessDialog.Text = "Adjust Brightness";
            m_BrightnessDialog.ShowIcon = false;
            m_BrightnessDialog.ShowInTaskbar = false;
            m_BrightnessDialog.MinimizeBox = false;
            m_BrightnessDialog.MaximizeBox = false;
            m_BrightnessDialog.StartPosition = FormStartPosition.CenterScreen;
            m_BrightnessDialog.Size = new Size(200, 200);
            CreateDialogTrackBar();
            CreateDialogTrackbarLabel();
            CreateDialogOkButton();
            m_BrightnessDialog.Controls.Add(m_BrightnessSlider);
            m_BrightnessDialog.Controls.Add(m_TrackBarValueLabel);
            m_BrightnessDialog.Controls.Add(m_OkButton);
            m_BrightnessDialog.Show();
            return m_BrightnessDialog;
        }

        public void AdjustImage(int brightness)
        {
            Cursor.Current = Cursors.WaitCursor;
            Bitmap temp = (Bitmap)m_ImageHandler.CurrentBitmap;
            Bitmap bmap = (Bitmap)temp.Clone();
            if (brightness < -255)
                brightness = -255;
            if (brightness > 255)
                brightness = 255;
            Color c;
            for (int i = 0; i < bmap.Width; i++)
            {
                for (int j = 0; j < bmap.Height; j++)
                {
                    c = bmap.GetPixel(i, j);
                    int cR = c.R + brightness;
                    int cG = c.G + brightness;
                    int cB = c.B + brightness;

                    if (cR < 0)
                        cR = 1;
                    if (cR > 255)
                        cR = 255;

                    if (cG < 0)
                        cG = 1;
                    if (cG > 255)
                        cG = 255;

                    if (cB < 0)
                        cB = 1;
                    if (cB > 255)
                        cB = 255;

                    bmap.SetPixel(i, j, Color.FromArgb((byte)cR, (byte)cG, (byte)cB));
                }
            }
            m_ImageHandler.SetPreviousVersion();
            m_ImageHandler.CurrentBitmap = (Bitmap)bmap.Clone();
            UpdateImage();
            Cursor.Current = Cursors.Default;
        }

        public void UpdateImage()
        {
            m_ImageHub.CurrentImage.Image = m_ImageHandler.CurrentBitmap;
        }

        private void CreateDialogTrackBar()
        {
            m_BrightnessSlider = new TrackBar();
            m_BrightnessSlider.Left = (m_BrightnessDialog.Width - m_BrightnessSlider.Width) / 2;
            m_BrightnessSlider.Top = ((m_BrightnessDialog.Height - m_BrightnessSlider.Height) / 2) - 40;
            m_BrightnessSlider.Maximum = 255;
            m_BrightnessSlider.Minimum = -255;
            m_BrightnessSlider.TickFrequency = 50;
            m_BrightnessSlider.ValueChanged += new EventHandler(onBrightnessSliderValueChanged);
        }

        private void CreateDialogTrackbarLabel()
        {
            m_TrackBarValueLabel = new Label();
            m_TrackBarValueLabel.TextAlign = ContentAlignment.MiddleCenter;
            m_TrackBarValueLabel.Left = (m_BrightnessDialog.Width - m_TrackBarValueLabel.Width) / 2;
            m_TrackBarValueLabel.Top = ((m_BrightnessDialog.Height - m_TrackBarValueLabel.Height) / 2) + 10;
            m_TrackBarValueLabel.Text = m_BrightnessSlider.Value.ToString();
        }

        private void CreateDialogOkButton()
        {
            m_OkButton = new Button();
            m_OkButton.Left = (m_BrightnessDialog.Width - m_OkButton.Width) / 2;
            m_OkButton.Top = ((m_BrightnessDialog.Height - m_OkButton.Height) / 2) + 40;
            m_OkButton.Text = Constants.OkButtonText;
            m_OkButton.Click += new EventHandler(onOkButtonClicked);
        }

        private void onOkButtonClicked(object sender, EventArgs e)
        {
            AdjustImage(m_BrightnessSlider.Value);
            m_BrightnessDialog.Dispose();
        }

        private void onBrightnessSliderValueChanged(object sender, EventArgs e)
        {
            m_TrackBarValueLabel.Text = m_BrightnessSlider.Value.ToString();
        }
    }
}
