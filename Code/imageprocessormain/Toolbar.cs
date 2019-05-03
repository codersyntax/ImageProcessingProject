using ImageProcessorMain.AdjustmentComponents;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ImageProcessorMain
{
    public class Toolbar
    {
        private Form m_MainForm;
        private ImageHub m_ImageHub;
        private ImageHandler m_ImageHandler;
        private int buttonSeperator = 0;

        public Toolbar(Form mainForm, ImageHub imageHub, ImageHandler imageHandler)
        {
            m_MainForm = mainForm;
            m_ImageHub = imageHub;
            m_ImageHandler = imageHandler;
            AddAdjustmentComponents();
            m_MainForm.Controls.Find("Undo", true)[0].Enabled = false;
        }

        private void AddAdjustmentComponents()
        {
            m_MainForm.Controls.Add(createZoomSlider());
            List<string> m_Adjustments = new List<string> { "Undo", "Brightness", "Contrast", "Crop", "Resize", "Filter", "Blur" };
            foreach(string adjustment in m_Adjustments)
            {
                m_MainForm.Controls.Add(createButton(adjustment));
            }
        }

        private void onClickToolbarButton(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if (button != null && m_ImageHandler.OriginalBitmap != null)
            {
                switch (button.Name)
                {
                    case "Undo":
                        Undo();
                        break;
                    case "Brightness":
                        new BrightnessAdjustment(m_MainForm, m_ImageHandler, m_ImageHub);
                        break;
                    case "Contrast":
                        new ContrastAdjustment(m_MainForm, m_ImageHandler, m_ImageHub);
                        break;
                    case "Crop":
                        new CropAdjustment(m_MainForm, m_ImageHandler, m_ImageHub);
                        break;
                    case "Resize":
                        new ResizeAdjustment(m_MainForm, m_ImageHandler, m_ImageHub);
                        break;
                    case "Filter":
                        new FilterAdjustment(m_MainForm, m_ImageHandler, m_ImageHub);
                        break;
                    case "Blur":
                        new BlurAdjustment(m_MainForm, m_ImageHandler, m_ImageHub);
                        break;
                }
            }
        }

        private void onZoomTrackBarScroll(object sender, EventArgs e)
        {
            TrackBar zoomTrackBar = sender as TrackBar;
            if (zoomTrackBar != null && m_ImageHandler.OriginalBitmap != null)
            {
                if (m_ImageHub.CurrentImage.Width > 40 && m_ImageHub.CurrentImage.Height > 40)
                {
                    double magnificationLevel = 1;
                    switch(zoomTrackBar.Value)
                    {
                        case 1:
                            magnificationLevel = 0.2;
                            break;
                        case 2:
                            magnificationLevel = 0.4;
                            break;
                        case 3:
                            magnificationLevel = 0.6;
                            break;
                        case 4:
                            magnificationLevel = 0.8;
                            break;
                        case 5:
                            magnificationLevel = 1;
                            break;
                        case 6:
                            magnificationLevel = 1.2;
                            break;
                        case 7:
                            magnificationLevel = 1.4;
                            break;
                        case 8:
                            magnificationLevel = 1.6;
                            break;
                        case 9:
                            magnificationLevel = 1.8;
                            break;
                        case 10:
                            magnificationLevel = 2.0;
                            break;
                    }
                    m_ImageHub.CurrentImage.Width = Convert.ToInt32(m_ImageHandler.CurrentBitmap.Width * magnificationLevel);
                    m_ImageHub.CurrentImage.Height = Convert.ToInt32(m_ImageHandler.CurrentBitmap.Height * magnificationLevel);
                    m_ImageHub.CurrentImage.Refresh();
                    m_ImageHub.CurrentImage.Update();
                }
            }
            else
            {
                zoomTrackBar.Value = 5;
            }
        }
        private TrackBar createZoomSlider()
        {
            TrackBar zoomTrackBar = new TrackBar();
            zoomTrackBar.Location = new Point(0, buttonSeperator);
            buttonSeperator += 50;
            zoomTrackBar.Value = 5;
            zoomTrackBar.Maximum = 10;
            zoomTrackBar.Minimum = 1;
            zoomTrackBar.TickFrequency = 1;
            zoomTrackBar.Name = "ZoomTrackBar";
            zoomTrackBar.Scroll += new EventHandler(onZoomTrackBarScroll);
            return zoomTrackBar;
        }

        private Button createButton(string buttonText)
        {
            Button button = new Button();
            button.Location = new Point(0, buttonSeperator);
            button.Width = 100;
            button.Height = 40;
            buttonSeperator += 40;
            button.Text = buttonText;
            button.Name = buttonText;
            button.Click += new EventHandler(onClickToolbarButton);
            return button;
        }

        private void Undo()
        {
            if (m_ImageHandler.undoButtonStack.Count > 1)
            {
                m_ImageHandler.CurrentBitmap = m_ImageHandler.PopUndoStack();
                m_ImageHub.CurrentImage.Image = m_ImageHandler.CurrentBitmap;
            }
            else
            {
                m_ImageHub.CurrentImage.Image = m_ImageHandler.OriginalBitmap;
                m_MainForm.Controls.Find("Undo", true)[0].Enabled = false;
            }            
        }
    }
}
