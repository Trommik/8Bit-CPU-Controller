using System;
using System.Globalization;
using System.Linq;

using CPUController.Extensions;

namespace CPUController
{
    public static class InstructionParser
    {
        public static IInstruction Parse(string line)
        {
            string[]? parts = line.Split(",");

            if (parts.Length > 2)
                throw new Exception("To many elements!");

            // Strip all '0x' from the hex values
            parts = parts.Select(p => p.Trim().Replace("0x", string.Empty)).ToArray();

            // Check if the given value or string is a opcode
            if (EnumExtension.Contains<OpCode>(parts[0]))
            {
                // Parse the opcode
                var opCodeInstruction = new OpCodeInstruction
                {
                    OpCode = Enum.Parse<OpCode>(parts[0])
                };

                // Parse the parameter if it exists 
                if (parts.Length == 2)
                {
                    opCodeInstruction.Parameter = byte.Parse(parts[1], NumberStyles.HexNumber);
                }

                return opCodeInstruction;
            }

            // Pares and return the value instruction
            return new ValueInstruction
            {
                Value = byte.Parse(parts[0], NumberStyles.HexNumber)
            };
        }
    }
}