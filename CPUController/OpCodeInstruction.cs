using CPUController.Extensions;

namespace CPUController
{
    public class OpCodeInstruction : IInstruction
    {
        private OpCode _opCode;

        /// <inheritdoc />
        public byte Address { get; set; }

        /// <inheritdoc />
        public byte Value { get; private set; }

        /// <inheritdoc />
        public string Comment { get; set; } = string.Empty;

        /// <summary>
        /// The opcode of this instruction. 
        /// </summary>
        public OpCode OpCode
        {
            get => _opCode;
            set
            {
                _opCode = value;
                OnOpCodeChanged();
            }
        }

        private void OnOpCodeChanged()
        {
            Value = (byte)OpCode;
            NeedsParameter = EnumExtension.Contains<AddressableOpCode>((byte)OpCode);
        }

        /// <summary>
        /// The parameter of this opcode instruction. 
        /// </summary>
        public byte Parameter { get; set; }

        /// <summary>
        /// True when the current <see cref="OpCode"/> needs a parameter. 
        /// </summary>
        public bool NeedsParameter { get; private set; }

        /// <summary>
        /// Initializes a new <see cref="OpCodeInstruction"/>.
        /// </summary>
        public OpCodeInstruction() { }

        /// <summary>
        /// Initializes a new <see cref="OpCodeInstruction"/> with the given <paramref name="address"/> and <paramref name="opcode"/>.
        /// </summary>
        public OpCodeInstruction(byte address, OpCode opcode)
        {
            Address = address;
            OpCode = opcode;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            string command = NeedsParameter ? $"{OpCode}, 0x{Parameter:X2}" : $"{OpCode}";

            return string.IsNullOrWhiteSpace(Comment) ? command : $"{command} // {Comment}";
        }

        /// <inheritdoc />
        public byte[] ToBinary()
        {
            return NeedsParameter ? new[] { Value, Parameter } : new[] { Value };
        }
    }
}