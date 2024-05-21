using System;
using System.Windows.Markup;

namespace CPUController.UI.MarkupExtensions
{
    internal sealed class IntExtension : MarkupExtension
    {
        /// <summary>
        /// The integer value to provide. 
        /// </summary>
        public int Value { get; set; }

        public IntExtension(int value)
        {
            Value = value;
        }

        /// <inheritdoc />
        public override object ProvideValue(IServiceProvider sp)
        {
            return Value;
        }
    };
}