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
            List<string> m_Adjustments = new List<string> { "Undo", "Zoom", "Brightness", "Blur", "Contrast", "Resize" };
            foreach(string adjustment in m_Adjustments)
            {
                m_MainForm.Controls.Add(createButton(adjustment));
            }
        }

        private void onClickToolbarButton(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                m_ImageHub.ZoomEnabled = false;
                switch (button.Name)
                {
                    case "Undo":
                        Undo();
                        break;
                    case "Zoom":
                        m_ImageHub.ZoomEnabled = true;
                        break;
                    case "Brightness":
                        new BrightnessAdjustment(m_MainForm, m_ImageHandler, m_ImageHub);
                        break;
                    case "Blur":
                        new BlurAdjustment(m_MainForm, m_ImageHandler, m_ImageHub);
                        break;
                    case "Contrast":
                        new ContrastAdjustment(m_MainForm, m_ImageHandler, m_ImageHub);
                        break;
                    case "Resize":
                        new ResizeAdjustment(m_MainForm, m_ImageHandler, m_ImageHub);
                        break;
                    
                }
            }
        }

        private Button createButton(string buttonText)
        {
            Button button = new Button();
            button.Location = new Point(0, buttonSeperator);
            buttonSeperator += 20;
            button.Text = buttonText;
            button.Name = buttonText;
            button.Click += new EventHandler(onClickToolbarButton);
            return button;
        }
        
        private void Undo()
        {
            m_ImageHub.CurrentImage.Image = m_ImageHandler.PopUndoStack();
            if (m_ImageHandler.undoButtonStack.Count == 0)
            {
                m_ImageHub.CurrentImage.Image = m_ImageHandler.OriginalBitmap;
                m_MainForm.Controls.Find("Undo", true)[0].Enabled = false;
            }            
        }
    }
}
