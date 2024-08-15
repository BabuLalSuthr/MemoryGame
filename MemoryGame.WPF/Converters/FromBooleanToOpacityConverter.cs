using System.Globalization;
using System.Windows.Data;

namespace MemoryGame.WPF.Converters
{
    public class FromBooleanToOpacityConverter : IValueConverter 
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
            {
                return boolValue ? 1 : 0.1;
            }
            return 0.1;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}
