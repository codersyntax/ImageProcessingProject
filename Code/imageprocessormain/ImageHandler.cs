using System.Collections.Generic;
using System.Drawing;

namespace ImageProcessorMain
{
    public class ImageHandler
    {
        private string _bitmapPath;
        private Bitmap _currentBitmap;
        private Bitmap _bitmapbeforeProcessing;

        public Stack<Bitmap> undoButtonStack = new Stack<Bitmap>();

        public Bitmap CurrentBitmap
        {
            get
            {
                if (_currentBitmap == null)
                    _currentBitmap = new Bitmap(1, 1);
                return _currentBitmap;
            }
            set { _currentBitmap = value; }
        }

        public string BitmapPath
        {
            get { return _bitmapPath; }
            set { _bitmapPath = value; }
        }

        public void SaveBitmap(string saveFilePath)
        {
            _bitmapPath = saveFilePath;
            if (System.IO.File.Exists(saveFilePath))
                System.IO.File.Delete(saveFilePath);
            _currentBitmap.Save(saveFilePath);
        }

        public void SetPreviousVersion()
        {
            _bitmapbeforeProcessing = _currentBitmap;
        }

        public Bitmap GetPreviousVersion()
        {
            
            return _bitmapbeforeProcessing;
        }

        public void AddBitMapToStack(Bitmap versionToAddToStack)
        {
            undoButtonStack.Push(versionToAddToStack);
        }

        public Bitmap PopUndoStack()
        {
            return undoButtonStack.Pop();
        }


    }
}
