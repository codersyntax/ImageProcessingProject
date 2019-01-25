using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace ImageProcessorMain.AdjustmentComponents
{
    internal class ContrastAdjustment : IAdjustment
    {
        private ImageHandler m_ImageHandler;
        private ImageHub m_ImageHub;
        private Form m_ContrastDialog;
        private TrackBar m_ContrastSlider;
        private Label m_TrackBarValueLabel;
        private Button m_OkButton;

        internal ContrastAdjustment(ImageHandler imageHandler, ImageHub imageHub)
        {
            m_ImageHandler = imageHandler;
            m_ImageHub = imageHub;
            ShowDialog();
        }

        public void AdjustImage(int contrast)
        {
            Cursor.Current = Cursors.WaitCursor;
            Bitmap contrastedImage = Contrast (m_ImageHandler.CurrentBitmap, m_ContrastSlider.Value);
            m_ImageHandler.SetPreviousVersion();
            m_ImageHandler.CurrentBitmap = contrastedImage;
            UpdateImage();
            Cursor.Current = Cursors.Default;
        }

        public Form ShowDialog()
        {
            m_ContrastDialog = new Form();
            m_ContrastDialog.Text = "Adjust Contrast";
            m_ContrastDialog.ShowIcon = false;
            m_ContrastDialog.ShowInTaskbar = false;
            m_ContrastDialog.MinimizeBox = false;
            m_ContrastDialog.MaximizeBox = false;
            m_ContrastDialog.StartPosition = FormStartPosition.CenterScreen;
            m_ContrastDialog.Size = new Size(200, 200);
            CreateDialogTrackBar();
            CreateDialogTrackbarLabel();
            CreateDialogOkButton();
            m_ContrastDialog.Controls.Add(m_ContrastSlider);
            m_ContrastDialog.Controls.Add(m_TrackBarValueLabel);
            m_ContrastDialog.Controls.Add(m_OkButton);
            m_ContrastDialog.Show();
            return m_ContrastDialog;
        }

        public void UpdateImage()
        {
            m_ImageHub.CurrentImage.Image = m_ImageHandler.CurrentBitmap;
        }

        private void CreateDialogTrackBar()
        {
            m_ContrastSlider = new TrackBar();
            m_ContrastSlider.Left = (m_ContrastDialog.Width - m_ContrastSlider.Width) / 2;
            m_ContrastSlider.Top = ((m_ContrastDialog.Height - m_ContrastSlider.Height) / 2) - 40;
            m_ContrastSlider.Maximum = 6;
            m_ContrastSlider.Minimum = 0;
            m_ContrastSlider.TickFrequency = 1;
            m_ContrastSlider.ValueChanged += new EventHandler(onContrastSliderValueChanged);
        }

        private void CreateDialogTrackbarLabel()
        {
            m_TrackBarValueLabel = new Label();
            m_TrackBarValueLabel.TextAlign = ContentAlignment.MiddleCenter;
            m_TrackBarValueLabel.Left = (m_ContrastDialog.Width - m_TrackBarValueLabel.Width) / 2;
            m_TrackBarValueLabel.Top = ((m_ContrastDialog.Height - m_TrackBarValueLabel.Height) / 2) + 10;
            m_TrackBarValueLabel.Text = m_ContrastSlider.Value.ToString();
        }

        private void CreateDialogOkButton()
        {
            m_OkButton = new Button();
            m_OkButton.Left = (m_ContrastDialog.Width - m_OkButton.Width) / 2;
            m_OkButton.Top = ((m_ContrastDialog.Height - m_OkButton.Height) / 2) + 40;
            m_OkButton.Text = Constants.OkButtonText;
            m_OkButton.Click += new EventHandler(onOkButtonClicked);
        }

        private void onOkButtonClicked(object sender, EventArgs e)
        {
            AdjustImage(m_ContrastSlider.Value);
            m_ContrastDialog.Dispose();
        }

        private void onContrastSliderValueChanged(object sender, EventArgs e)
        {
            m_TrackBarValueLabel.Text = m_ContrastSlider.Value.ToString();
        }

    }
}
