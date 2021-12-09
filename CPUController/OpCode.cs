namespace CPUController
{
    public enum OpCode : byte
    {
        NOP = 0x00,
        HLT = 0x01,
        JMP = 0x04,
        JMC = 0x05,
        JMZ = 0x06,
        JNZ = 0x07,
        LDA = 0x10,
        LDB = 0x11,
        STA = 0x12,
        STB = 0x13,
        STE = 0x14,
        ADD = 0x20,
        SUB = 0x21,
        TAB = 0x30,
        TBA = 0x31,
        TAO = 0x32,
        TBO = 0x33
    }

    public enum AddressableOpCode : byte
    {
        JMP = OpCode.JMP,
        JMC = OpCode.JMC,
        JMZ = OpCode.JMZ,
        JNZ = OpCode.JNZ,
        LDA = OpCode.LDA,
        LDB = OpCode.LDB,
        STA = OpCode.STA,
        STB = OpCode.STB,
        STE = OpCode.STE
    }
}