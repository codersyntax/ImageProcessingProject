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
        private Button m_RotateButton;
        private Label m_RotateFlipLabel; 
        private ListBox m_RotateFlipOptionsListBox; 
        public RotateFlipAdjustment(ImageHandler imageHandler, ImageHub imageHub)
        {
            m_ImageHandler = imageHandler;
            m_ImageHub = imageHub;
            ShowDialog();
        }

        public void AdjustImage(RotateFlipType rotFlipType)
        {
            Bitmap temp = (Bitmap)m_ImageHandler.CurrentBitmap;
            temp.RotateFlip(rotFlipType);
            m_ImageHandler.SetPreviousVersion();
            m_ImageHandler.CurrentBitmap = temp;
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
            m_RotateDialog.Size = new Size(300, 300);
            CreateDialogRotateButton();
            CreateDialogRotateLabel();
            CreateDialogRotateFlipOptionsListbox();
            m_RotateDialog.Controls.Add(m_RotateFlipOptionsListBox);
            m_RotateDialog.Controls.Add(m_RotateButton);
            m_RotateDialog.Controls.Add(m_RotateFlipLabel); 
            m_RotateDialog.Show();
            return m_RotateDialog;
        }
        
            
        
      

        public void UpdateImage()
        {
            m_ImageHub.CurrentImage.Image = m_ImageHandler.CurrentBitmap;
        }

        private void CreateDialogRotateFlipOptionsListbox()
        {
            m_RotateFlipOptionsListBox = new ListBox();
            m_RotateFlipOptionsListBox.Left = ((m_RotateDialog.Width - m_RotateFlipOptionsListBox.Width) / 2) + 40;
            m_RotateFlipOptionsListBox.Top = ((m_RotateDialog.Height - m_RotateButton.Height) / 2) - 80;
            foreach (string option in Enum.GetNames(typeof(RotateFlipType)))
            {
                m_RotateFlipOptionsListBox.Items.Add(option);
            }
        }
       
        private void CreateDialogRotateButton()
        {
            m_RotateButton = new Button();
            m_RotateButton.Left = (m_RotateDialog.Width - m_RotateButton.Width) / 2;
            m_RotateButton.Top = ((m_RotateDialog.Height - m_RotateButton.Height) / 2) + 40;
            m_RotateButton.Text = Constants.OkButtonText;
            m_RotateButton.Click += new EventHandler(onOkButtonClicked);
        }

        private void CreateDialogRotateLabel()
        {
            m_RotateFlipLabel = new Label();
            m_RotateFlipLabel.Left = m_RotateDialog.Left + 20;
            m_RotateFlipLabel.Top = ((m_RotateDialog.Height - m_RotateButton.Height) / 2) - 80;
            m_RotateFlipLabel.Text = "Select Rotation Type";
        }

        private void onOkButtonClicked(object sender, EventArgs e)
        {
            var flipType = (RotateFlipType)Enum.Parse(typeof(RotateFlipType), m_RotateFlipOptionsListBox.SelectedItem.ToString());
            AdjustImage(flipType);
            m_RotateDialog.Dispose();
        }

        
      
    }
}