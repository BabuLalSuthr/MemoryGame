using Prism.Mvvm;
using System.Drawing;

namespace MemoryGame.WPF.ViewModels
{
    public class ColorNodeViewModel : BindableBase
    {
        private bool isActive;
        private Color fillColor;

        public ColorNodeViewModel(Color fillColor)
        {
            this.fillColor = fillColor;
        }

        public bool IsActive
        {
            get { return isActive; }
            set { SetProperty(ref isActive, value); }
        }

        public Color FillColor
        {
            get { return fillColor; }
        }
    }
}
