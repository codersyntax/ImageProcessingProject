using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// https://docs.microsoft.com/en-us/dotnet/api/system.drawing.bitmap?view=netframework-4.7.2
// https://www.codeproject.com/Articles/9727/Image-Processing-Lab-in-C#_comments 
//https://softwarebydefault.com/2013/03/22/bitmap-swap-argb/


namespace ImageProcessorMain.AdjustmentComponents
{

    internal class FilterAdjustment : IAdjustment
    {
        private ImageHandler m_ImageHandler;
        private ImageHub m_ImageHub;
        private Form m_BrightnessDialog;
        private TrackBar m_BrightnessSlider;
        private Label m_TrackBarValueLabel;
        private Button m_OkButton;

        internal FilterAdjustment(ImageHandler imageHandler, ImageHub imageHub)
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
            m_BrightnessDialog = new Form();
            m_BrightnessDialog.Text = "Choose Filter";
            m_BrightnessDialog.ShowIcon = false;
            m_BrightnessDialog.ShowInTaskbar = false;
            m_BrightnessDialog.MinimizeBox = false;
            m_BrightnessDialog.MaximizeBox = false;
            m_BrightnessDialog.StartPosition = FormStartPosition.CenterScreen;
            m_BrightnessDialog.Size = new Size(200, 200);
            throw new NotImplementedException();
        }

        public void UpdateImage()
        {
            throw new NotImplementedException();
        }
    }

    internal interface IAdjustment
    {
    }
}
