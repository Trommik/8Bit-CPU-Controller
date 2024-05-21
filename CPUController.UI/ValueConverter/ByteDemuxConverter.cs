using System;
using System.Globalization;
using System.Windows.Data;

namespace CPUController.UI.ValueConverter
{
    public class ByteDemuxConverter : IValueConverter
    {
        private int _lastIntValue; 
        
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not int intValue || parameter is not int address)
                throw new ArgumentException($"Invalid value type or parameter type, expected {typeof(int)}!");

            // Check that the address is in the range of the byte demux
            if (address >= 8)
                throw new ArgumentException($"Parameter value '{address}' is not supported!", nameof(parameter));
            
            // Save the intValue to convert back
            _lastIntValue = intValue;
            
            // Check if the intValue is the current address
            return intValue == address; 
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not bool boolValue || parameter is not int address)
                throw new ArgumentException($"Invalid value type or parameter type, expected {typeof(int)}!");

            // Check that the address is in the range of the byte demux
            if (address >= 8)
                throw new ArgumentException($"Parameter value '{address}' is not supported!", nameof(parameter));

            // Set the intValue to the current address or zero 
            if (boolValue)
                return _lastIntValue = address;
            else
                return _lastIntValue = 0;
        }
    }
}