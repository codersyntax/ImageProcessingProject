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
        private ComboBox m_FlipCombobox;
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
            CreateDialogOkButton();
            CreateFlipCombobox();
            m_RotateDialog.Controls.Add(m_RotateSlider);
            m_RotateDialog.Controls.Add(m_FlipCombobox);
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
            
        }
      

            public void UpdateImage()
            {
                m_ImageHub.CurrentImage.Image = m_ImageHandler.CurrentBitmap;
            }
        private ComboBox CreateFlipCombobox()
        {
            m_FlipCombobox = new ComboBox();
            m_FlipCombobox.Items.Add("Vertical");
            m_FlipCombobox.Items.Add("Horizontal");
            return m_FlipCombobox;
        }
       
        private void CreateDialogOkButton()
        {
            m_OkButton = new Button();
            m_OkButton.Left = (m_RotateDialog.Width - m_OkButton.Width) / 2;
            m_OkButton.Top = ((m_RotateDialog.Height - m_OkButton.Height) / 2) + 40;
            m_OkButton.Text = Constants.OkButtonText;
            m_OkButton.Click += new EventHandler(onOkButtonClicked);
        }

        private void onOkButtonClicked(object sender, EventArgs e)
        {
            AdjustImage(m_RotateSlider.Value);
            m_RotateDialog.Dispose();
        }
    }
}
