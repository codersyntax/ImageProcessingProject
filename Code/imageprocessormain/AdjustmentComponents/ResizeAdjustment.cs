﻿using System;
using System.Windows.Forms;
using System.Drawing;


namespace ImageProcessorMain.AdjustmentComponents
{
    internal class ResizeAdjustment
    {
        private ImageHandler m_ImageHandler;
        private ImageHub m_ImageHub;

        internal ResizeAdjustment(ImageHandler imageHandler, ImageHub imageHub)
        {
            m_ImageHandler = imageHandler;
            m_ImageHub = imageHub;
            ShowDialog();
        }

        public Form ShowDialog()
        {
            //Focus on making this
            throw new NotImplementedException();
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
        }
    }
}
