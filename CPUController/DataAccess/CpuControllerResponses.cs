namespace CPUController.DataAccess
{
    public sealed record CpuModeResponse(bool ExecuteMode, bool LoadCodeMode);

    public sealed record CpuCodeResponse(bool LoadCodeMode, byte CodeToLoad, byte CodeLoaded);

    public sealed record CpuInstructionResponse(byte Flags, byte Instruction, byte InstructionStep);

    public sealed record CpuControlWordResponse(int ControlWord);
}