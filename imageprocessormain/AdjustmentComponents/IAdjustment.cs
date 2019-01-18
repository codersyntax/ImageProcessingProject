using System.Windows.Forms;

namespace ImageProcessorMain.AdjustmentComponents
{
    public interface IAdjustment
    {
        Form ShowDialog();

        void AdjustImage(int amount);

        void UpdateImage();
    }
}
