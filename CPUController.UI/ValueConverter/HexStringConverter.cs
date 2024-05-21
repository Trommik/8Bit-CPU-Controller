using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Data;

namespace CPUController.UI.ValueConverter
{
    public class HexStringConverter : IValueConverter
    {
        private byte _lastValidValue;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is byte hexValue)
                return "0x" + hexValue.ToString("X2");

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string str)
            {
                string hexString = str.Replace("0x", string.Empty);
                bool valid = byte.TryParse(hexString, NumberStyles.HexNumber, null, out byte hexValue);

                // Return valid value or the last valid value
                return _lastValidValue = valid ? hexValue : _lastValidValue;
            }

            return null;
        }
    }
}