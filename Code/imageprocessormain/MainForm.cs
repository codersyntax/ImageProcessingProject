using System;
using System.Drawing;
using System.Windows.Forms;

namespace ImageProcessorMain
{
    public partial class MainForm : Form
    {
        OpenFileDialog openFileDialog;
        SaveFileDialog saveFileDialog;
        ImageHub m_ImageHub;
        ImageHandler m_ImageHandler;
        Toolbar m_Toolbar;

        public MainForm()
        {
            InitializeComponent();
            MaximizedBounds = new Rectangle(0,0,100, 400);
            InitComponents();
            InitDialogs();
        }

        private void InitComponents()
        {
            m_ImageHandler = new ImageHandler();
            m_ImageHub = new ImageHub(this, m_ImageHandler);
            m_Toolbar = new Toolbar(this, m_ImageHub, m_ImageHandler);
        }

        private void InitDialogs()
        {
            InitOpenFileDialog();
            InitSaveFileDialog();
        }

        private void InitOpenFileDialog()
        {
            openFileDialog = new OpenFileDialog
            {
                RestoreDirectory = true,
                InitialDirectory = Constants.InitialDirectory,
                FilterIndex = 1,
                Filter = Constants.FilterString
            };
        }

        private void InitSaveFileDialog()
        {
            saveFileDialog = new SaveFileDialog
            {
                RestoreDirectory = true,
                InitialDirectory = Constants.InitialDirectory,
                FilterIndex = 1,
                Filter = Constants.FilterString
            };
        }

        private void menuItemOpen_Click(object sender, EventArgs e)
        {
            if (m_ImageHandler.BitmapPath == null)
            {
                if (DialogResult.OK == openFileDialog.ShowDialog())
                {
                    m_ImageHandler.CurrentBitmap = (Bitmap)Bitmap.FromFile(openFileDialog.FileName);
                    m_ImageHandler.OriginalBitmap = m_ImageHandler.CurrentBitmap;
                    m_ImageHandler.BitmapPath = openFileDialog.FileName;
                    m_ImageHub.CreateImageForm();
                    menuItemSave.Enabled = true;
                }
            } else
            {
                MessageBox.Show("An image is already open.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void menuItemSave_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == saveFileDialog.ShowDialog())
            {
                m_ImageHandler.SaveBitmap(saveFileDialog.FileName);
            }
        }

        private void menuItemExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
