namespace CPUController
{
    public class ValueInstruction : IInstruction
    {
        /// <inheritdoc />
        public byte Address { get; set; }

        /// <inheritdoc />
        public byte Value { get; set; }

        /// <inheritdoc />
        public string Comment { get; set; } = string.Empty;

        /// <summary>
        /// Initializes a new <see cref="ValueInstruction"/>.
        /// </summary>
        public ValueInstruction() { }

        /// <summary>
        /// Initializes a new <see cref="ValueInstruction"/> with the given <paramref name="address"/> and <paramref name="value"/>.
        /// </summary>
        public ValueInstruction(byte address, byte value)
        {
            Address = address;
            Value = value;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return string.IsNullOrWhiteSpace(Comment) ? $"0x{Value:X2}" : $"0x{Value:X2} // {Comment}";
        }

        /// <inheritdoc />
        public byte[] ToBinary()
        {
            return new[] { Value };
        }
    }
}