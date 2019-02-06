using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageProcessorMain.AdjustmentComponents
{
    public partial class ResizeImage : Form
    {
        Image m_image;
        int height, width;

        private void ResizeImage_Load(object sender, EventArgs e)
        {
            //<summary> accepting image file and assigning it to my image control variable </summary>
            m_image = Image.FromFile("");

            width = m_image.Width; //<summary> seting image width </summary>
            height = m_image.Height; //setting image height </summary>

            //<summary> assigning image file to picturebox control item </summary>
            p_control.Image = m_image;
        }


        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            //<summary> setting image width value to equal trackerbar </summary>
            width = (m_image.Width * h_trackBar.Value) / 100;
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            //<summary> setting image height value to equal trackerbar </summary>
            height = (m_image.Height * v_trackBar.Value) / 100;
        }


        //<summary> creating a bitmap method to handle image resizing process </summary>
        private Bitmap M_ResizeImageb(int m_width, int m_height)
        {
            Rectangle m_rect = new Rectangle(0, 0, m_width, m_height);
            Bitmap m_destImage = new Bitmap(m_width, m_height);
            m_destImage.SetResolution(m_image.HorizontalResolution, m_image.VerticalResolution);

            using (var g = Graphics.FromImage(m_destImage))
            {
                g.CompositingMode = CompositingMode.SourceCopy;
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapmode = new ImageAttributes())
                {
                    wrapmode.SetWrapMode(WrapMode.TileFlipXY);
                    g.DrawImage(m_image, m_rect, 0, 0, m_image.Width, m_image.Height, GraphicsUnit.Pixel, wrapmode);
                }
            }
                return m_destImage;
        }

        public ResizeImage()
        {
            InitializeComponent();
        }

        
    }
}
