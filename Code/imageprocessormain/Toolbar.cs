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
        }

        private void AddAdjustmentComponents()
        {
            List<string> m_Adjustments = new List<string> { "Undo", "Brightness", "Blur" };
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
                switch (button.Name)
                {
                    case "Undo":
                        Undo();
                        break;
                    case "Brightness":
                        IAdjustment brightnessAdjustment = new BrightnessAdjustment(m_ImageHandler, m_ImageHub);
                        break;
                    case "Blur":
                        IAdjustment blurAdjustment = new BlurAdjustment(m_ImageHandler, m_ImageHub);
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
            m_ImageHandler.CurrentBitmap = m_ImageHandler.GetPreviousVersion();
            m_ImageHub.CurrentImage.Image = m_ImageHandler.CurrentBitmap;
        }
    }
}
