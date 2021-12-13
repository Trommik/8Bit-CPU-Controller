using System;
using System.Globalization;
using System.Windows.Data;

namespace CPUController.UI.ValueConverter
{
    public class BitValueConverter : IValueConverter
    {
        private int _lastIntValue; 
        
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not int intValue || parameter is not int bitNumber)
                throw new ArgumentException($"Invalid value type or parameter type, expected {typeof(int)}!");

            // Check that the bit number is in the range of an int
            if (bitNumber >= 32)
                throw new ArgumentException($"Parameter value '{bitNumber}' is not supported!", nameof(parameter));
            
            // Save the intValue to convert back
            _lastIntValue = intValue;
            
            // Check if the bit at the given bitNumber is set in the intValue
            return (_lastIntValue & (1 << bitNumber)) != 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not bool boolValue || parameter is not int bitNumber)
                throw new ArgumentException($"Invalid value type or parameter type, expected {typeof(int)}!");

            // Check that the bit number is in the range of an int
            if (bitNumber >= 32)
                throw new ArgumentException($"Parameter value '{bitNumber}' is not supported!", nameof(parameter));

            // Set or unset the bit at the given bitNumber
            if (boolValue)
                return _lastIntValue |= 1 << bitNumber;
            else
                return _lastIntValue &= ~(1 << bitNumber);
        }
    }
}