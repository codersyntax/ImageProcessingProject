using System;
using System.Drawing;
using System.Windows.Forms;

namespace ImageProcessorMain.AdjustmentComponents
{
    internal class BlurAdjustment : IAdjustment
    {
        private ImageHandler m_ImageHandler;
        private ImageHub m_ImageHub;
        private Form m_BlurDialog;
        private TrackBar m_BlurSlider;
        private Label m_TrackBarValueLabel;
        private Button m_OkButton;

        internal BlurAdjustment(ImageHandler imageHandler, ImageHub imageHub)
        {
            m_ImageHandler = imageHandler;
            m_ImageHub = imageHub;
            ShowDialog();
        }
        public void AdjustImage(int amount)
        {
            Cursor.Current = Cursors.WaitCursor;
            Bitmap blurredImage = Blur(m_ImageHandler.CurrentBitmap, m_BlurSlider.Value);
            m_ImageHandler.SetPreviousVersion();
            m_ImageHandler.CurrentBitmap = blurredImage;
            UpdateImage();
            Cursor.Current = Cursors.Default;
        }

        public Form ShowDialog()
        {
            m_BlurDialog = new Form();
            m_BlurDialog.Text = "Adjust Blur";
            m_BlurDialog.ShowIcon = false;
            m_BlurDialog.ShowInTaskbar = false;
            m_BlurDialog.MinimizeBox = false;
            m_BlurDialog.MaximizeBox = false;
            m_BlurDialog.StartPosition = FormStartPosition.CenterScreen;
            m_BlurDialog.Size = new Size(200, 200);
            CreateDialogTrackBar();
            CreateDialogTrackbarLabel();
            CreateDialogOkButton();
            m_BlurDialog.Controls.Add(m_BlurSlider);
            m_BlurDialog.Controls.Add(m_TrackBarValueLabel);
            m_BlurDialog.Controls.Add(m_OkButton);
            m_BlurDialog.Show();
            return m_BlurDialog;
        }

        public void UpdateImage()
        {
            m_ImageHub.CurrentImage.Image = m_ImageHandler.CurrentBitmap;
        }

        private void CreateDialogTrackBar()
        {
            m_BlurSlider = new TrackBar();
            m_BlurSlider.Left = (m_BlurDialog.Width - m_BlurSlider.Width) / 2;
            m_BlurSlider.Top = ((m_BlurDialog.Height - m_BlurSlider.Height) / 2) - 40;
            m_BlurSlider.Maximum = 6;
            m_BlurSlider.Minimum = 0;
            m_BlurSlider.TickFrequency = 1;
            m_BlurSlider.ValueChanged += new EventHandler(onBlurSliderValueChanged);
        }

        private void CreateDialogTrackbarLabel()
        {
            m_TrackBarValueLabel = new Label();
            m_TrackBarValueLabel.TextAlign = ContentAlignment.MiddleCenter;
            m_TrackBarValueLabel.Left = (m_BlurDialog.Width - m_TrackBarValueLabel.Width) / 2;
            m_TrackBarValueLabel.Top = ((m_BlurDialog.Height - m_TrackBarValueLabel.Height) / 2) + 10;
            m_TrackBarValueLabel.Text = m_BlurSlider.Value.ToString();
        }

        private void CreateDialogOkButton()
        {
            m_OkButton = new Button();
            m_OkButton.Left = (m_BlurDialog.Width - m_OkButton.Width) / 2;
            m_OkButton.Top = ((m_BlurDialog.Height - m_OkButton.Height) / 2) + 40;
            m_OkButton.Text = Constants.OkButtonText;
            m_OkButton.Click += new EventHandler(onOkButtonClicked);
        }

        private void onOkButtonClicked(object sender, EventArgs e)
        {
            AdjustImage(m_BlurSlider.Value);
            m_BlurDialog.Dispose();
        }

        private void onBlurSliderValueChanged(object sender, EventArgs e)
        {
            m_TrackBarValueLabel.Text = m_BlurSlider.Value.ToString();
        }

        private Bitmap Blur(Bitmap image, Int32 blurSize)
        {
            Rectangle rectangle = new Rectangle(0, 0, m_ImageHandler.CurrentBitmap.Width, m_ImageHandler.CurrentBitmap.Height);

            Bitmap blurred = new Bitmap(image.Width, image.Height);

            using (Graphics graphics = Graphics.FromImage(blurred))
                graphics.DrawImage(image, new Rectangle(0, 0, image.Width, image.Height),
                    new Rectangle(0, 0, image.Width, image.Height), GraphicsUnit.Pixel);

            for (int xx = rectangle.X; xx < rectangle.X + rectangle.Width; xx++)
            {
                for (int yy = rectangle.Y; yy < rectangle.Y + rectangle.Height; yy++)
                {
                    int avgR = 0, avgG = 0, avgB = 0;
                    int blurPixelCount = 0;
                    for (int x = xx; (x < xx + blurSize && x < image.Width); x++)
                    {
                        for (int y = yy; (y < yy + blurSize && y < image.Height); y++)
                        {
                            Color pixel = blurred.GetPixel(x, y);

                            avgR += pixel.R;
                            avgG += pixel.G;
                            avgB += pixel.B;

                            blurPixelCount++;
                        }
                    }

                    avgR = avgR / blurPixelCount;
                    avgG = avgG / blurPixelCount;
                    avgB = avgB / blurPixelCount;
                    for (int x = xx; x < xx + blurSize && x < image.Width && x < rectangle.Width; x++)
                        for (int y = yy; y < yy + blurSize && y < image.Height && y < rectangle.Height; y++)
                            blurred.SetPixel(x, y, Color.FromArgb(avgR, avgG, avgB));
                }
            }

            return blurred;
        }
    }
}
