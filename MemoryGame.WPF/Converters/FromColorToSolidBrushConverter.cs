using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace MemoryGame.WPF.Converters
{
    public class FromColorToSolidBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is System.Drawing.Color color)
            {
                System.Windows.Media.Color c = new Color() { A = 255, R = color.R, G = color.G, B = color.B };
                return new System.Windows.Media.SolidColorBrush(c);
            }
            return new SolidColorBrush(Colors.Transparent);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}
