using System;
using System.Windows.Forms;
using System.Drawing;


namespace ImageProcessorMain.AdjustmentComponents
{
    internal class ResizeAdjustment
    {
        private Form m_MainForm;
        private ImageHandler m_ImageHandler;
        private ImageHub m_ImageHub;
        private Form m_ResizeDialog;
        private Button m_OkButton;
        private TextBox m_WidthTextBox;
        private TextBox m_HeightTextBox;
        private Label m_WidthLabel;
        private Label m_HeightLabel;
        int w_textBox;
        int h_textBox;


        internal ResizeAdjustment(Form mainForm, ImageHandler imageHandler, ImageHub imageHub)
        {
            m_ImageHandler = imageHandler;
            m_ImageHub = imageHub;
            
            ShowDialog();
        }

        public Form ShowDialog()
        {
            //<summary> new form dialog </summary>
            m_ResizeDialog = new Form();
            m_ResizeDialog.Text = "Adjust Size";
            m_ResizeDialog.ShowIcon = false;
            m_ResizeDialog.ShowInTaskbar = false;
            m_ResizeDialog.MinimizeBox = false;
            m_ResizeDialog.MaximizeBox = false;
            m_ResizeDialog.StartPosition = FormStartPosition.CenterScreen;
            m_ResizeDialog.Size = new Size(200,200);
            CreateDialogOkButton();
            CreateWidthTextBox();
            CreateHeightTextBox();
            CreateWidthLabel();
            CreateHeightLabel();
            m_ResizeDialog.Show();
            m_ResizeDialog.Controls.Add(m_OkButton);
            m_ResizeDialog.Controls.Add(m_WidthTextBox);
            m_ResizeDialog.Controls.Add(m_HeightTextBox);
            m_ResizeDialog.Controls.Add(m_WidthLabel);
            m_ResizeDialog.Controls.Add(m_HeightLabel);


            return m_ResizeDialog;
        }

        public void AdjustImage(int width, int height)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (width != 0 && height != 0)
            {
                Bitmap temp = (Bitmap)m_ImageHandler.CurrentBitmap;
                Bitmap bmap = new Bitmap(width, height, temp.PixelFormat);

                double nWidthFactor = (double)temp.Width / (double)width;
                double nHeightFactor = (double)temp.Height / (double)height;

                double fx, fy, nx, ny;
                int cx, cy, fr_x, fr_y;
                Color color1 = new Color();
                Color color2 = new Color();
                Color color3 = new Color();
                Color color4 = new Color();
                byte nRed, nGreen, nBlue;

                byte bp1, bp2;

                for (int x = 0; x < bmap.Width; ++x)
                {
                    for (int y = 0; y < bmap.Height; ++y)
                    {

                        fr_x = (int)Math.Floor(x * nWidthFactor);
                        fr_y = (int)Math.Floor(y * nHeightFactor);
                        cx = fr_x + 1;
                        if (cx >= temp.Width) cx = fr_x;
                        cy = fr_y + 1;
                        if (cy >= temp.Height) cy = fr_y;
                        fx = x * nWidthFactor - fr_x;
                        fy = y * nHeightFactor - fr_y;
                        nx = 1.0 - fx;
                        ny = 1.0 - fy;

                        color1 = temp.GetPixel(fr_x, fr_y);
                        color2 = temp.GetPixel(cx, fr_y);
                        color3 = temp.GetPixel(fr_x, cy);
                        color4 = temp.GetPixel(cx, cy);

                        // Blue
                        bp1 = (byte)(nx * color1.B + fx * color2.B);

                        bp2 = (byte)(nx * color3.B + fx * color4.B);

                        nBlue = (byte)(ny * (double)(bp1) + fy * (double)(bp2));

                        // Green
                        bp1 = (byte)(nx * color1.G + fx * color2.G);

                        bp2 = (byte)(nx * color3.G + fx * color4.G);

                        nGreen = (byte)(ny * (double)(bp1) + fy * (double)(bp2));

                        // Red
                        bp1 = (byte)(nx * color1.R + fx * color2.R);

                        bp2 = (byte)(nx * color3.R + fx * color4.R);

                        nRed = (byte)(ny * (double)(bp1) + fy * (double)(bp2));

                        bmap.SetPixel(x, y, System.Drawing.Color.FromArgb
                (255, nRed, nGreen, nBlue));
                    }
                }
                m_ImageHandler.CurrentBitmap = (Bitmap)bmap.Clone();
            }
            Cursor.Current = Cursors.Default;
        }

        public void UpdateImage()
        {
            m_ImageHub.CurrentImage.Image = m_ImageHandler.CurrentBitmap;
            m_ImageHub.m_Image.Width = m_ImageHub.CurrentImage.Image.Width;
            m_ImageHub.m_Image.Height = m_ImageHub.CurrentImage.Image.Height;
            m_ImageHandler.AddBitMapToStack(m_ImageHandler.CurrentBitmap);
            m_MainForm.Controls.Find("Undo", true)[0].Enabled = true;
        }

        private void CreateDialogOkButton()
        {
            m_OkButton = new Button();
            m_OkButton.Left = (m_ResizeDialog.Width - m_OkButton.Width) / 2;
            m_OkButton.Top = ((m_ResizeDialog.Height - m_OkButton.Height) / 2) + 40;
            m_OkButton.Text = Constants.OkButtonText;
            m_OkButton.Click += new EventHandler(OnOkButtonClicked);
        }

        private void CreateWidthTextBox()
        {
            m_WidthTextBox = new TextBox();
            m_WidthTextBox.Left = m_ResizeDialog.Width - 135;
            m_WidthTextBox.Top = ((m_ResizeDialog.Height - m_OkButton.Height) / 2) - 40;
            
        }

        private void CreateWidthLabel()
        {
            m_WidthLabel = new Label();
            m_WidthLabel.Text = "Width";
            m_WidthLabel.Left = 10;
            m_WidthLabel.Top = ((m_ResizeDialog.Height - m_OkButton.Height) / 2) - 40;
        }

        
        private void CreateHeightTextBox()
        {
            m_HeightTextBox = new TextBox();
            m_HeightTextBox.Left = m_ResizeDialog.Width - 135;
            m_HeightTextBox.Top = ((m_ResizeDialog.Height - m_OkButton.Height) / 2);
        }

        private void CreateHeightLabel()
        {
            m_HeightLabel = new Label();
            m_HeightLabel.Text = "Height";
            m_HeightLabel.Left = 10;
            m_HeightLabel.Top = ((m_ResizeDialog.Height - m_OkButton.Height) / 2);
        }

        private void OnOkButtonClicked(object sender, EventArgs e)
        {
            m_WidthTextBox.Text = m_WidthTextBox.Text.Split('.')[0];
            m_HeightTextBox.Text = m_HeightTextBox.Text.Split('.')[0];
            w_textBox = Int32.Parse(m_WidthTextBox.Text);
            h_textBox = Int32.Parse(m_HeightTextBox.Text);
            AdjustImage(w_textBox, h_textBox);
            UpdateImage();
            m_ResizeDialog.Dispose();
        }
    }
}
