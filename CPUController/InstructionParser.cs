using System;
using System.Globalization;
using System.Linq;

using CPUController.Core;
using CPUController.Core.Instructions;
using CPUController.Extensions;

namespace CPUController
{
    public static class InstructionParser
    {
        public static IInstruction Parse(string line)
        {
            string[] instructionParts = line.Split("//", 2);
            
            // Store the comment if it is available 
            var comment = string.Empty;
            if (instructionParts.Length > 1)
                comment = instructionParts[1].Trim();
            
            string[] opCodeParts = instructionParts[0].Split(",");

            // Check if there is only a opcode and a value not more data
            if (opCodeParts.Length > 2)
                throw new Exception("To many elements!");

            // Strip all '0x' from the hex data values
            opCodeParts = opCodeParts.Select(p => p.Trim().Replace("0x", string.Empty)).ToArray();

            // Check if the given value or string is a opcode
            if (EnumExtension.Contains<OpCode>(opCodeParts[0]))
            {
                // Parse the opcode
                var opCodeInstruction = new OpCodeInstruction
                {
                    OpCode = Enum.Parse<OpCode>(opCodeParts[0]),
                    Comment = comment
                };

                // Parse the parameter if it exists 
                if (opCodeParts.Length == 2)
                {
                    opCodeInstruction.Parameter = byte.Parse(opCodeParts[1], NumberStyles.HexNumber);
                }

                return opCodeInstruction;
            }

            // Pares and return the value instruction
            return new ValueInstruction
            {
                Value = byte.Parse(opCodeParts[0], NumberStyles.HexNumber),
                Comment = comment
            };
        }
    }
}