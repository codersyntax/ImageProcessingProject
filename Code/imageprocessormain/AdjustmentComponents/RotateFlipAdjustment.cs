using System;
using System.Windows.Forms;
using System.Drawing;

namespace ImageProcessorMain.AdjustmentComponents
{
    internal class RotateFlipAdjustment
    {
        private ImageHandler m_ImageHandler;
        private ImageHub m_ImageHub;
        private Form m_RotateDialog;
        private TrackBar m_RotateSlider;
        private Label m_TrackBarValueLabel;
        private Button m_OkButton;
        public RotateFlipAdjustment(ImageHandler imageHandler, ImageHub imageHub)
        {
            m_ImageHandler = imageHandler;
            m_ImageHub = imageHub;
            ShowDialog();
        }

        public void AdjustImage(int amount)
        {
            throw new NotImplementedException();
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
            CreateDialogTrackBar();
            CreateDialogTrackbarLabel();
            CreateDialogOkButton();
            m_RotateDialog.Controls.Add(m_RotateSlider);
            m_RotateDialog.Controls.Add(m_TrackBarValueLabel);
            m_RotateDialog.Controls.Add(m_OkButton);
            m_RotateDialog.Show();
            return m_RotateDialog;
        }
        private void CreateDialogTrackBar()
        {
            m_RotateSlider = new TrackBar();
            m_RotateSlider.Left = (m_RotateDialog.Width - m_RotateSlider.Width) / 2;
            m_RotateSlider.Top = ((m_RotateDialog.Height - m_RotateSlider.Height) / 2) - 40;
            m_RotateSlider.Maximum = 255;
            m_RotateSlider.Minimum = -255;
            m_RotateSlider.TickFrequency = 50;
            m_RotateSlider.ValueChanged += new EventHandler(onRotateSliderValueChanged);
        }

        public void UpdateImage()
        {
            throw new NotImplementedException();
        }
    }
}
