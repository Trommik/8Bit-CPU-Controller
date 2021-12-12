using System.Threading.Tasks;

namespace CPUController.DataAccess
{
    public interface ICpuControllerClient
    {
        /// <summary>
        /// Checks the connection and returns true when the connection is successful. 
        /// </summary>
        /// <returns>True when the connection is successful.</returns>
        Task<bool> CheckConnection();

        /// <summary>
        /// Gets the current cpu <see cref="CpuMode"/>.
        /// </summary>
        /// <returns>The current <see cref="CpuMode"/>.</returns>
        Task<CpuModeResponse> GetMode();

        /// <summary>
        /// Gets the current code status of the cpu. 
        /// </summary>
        /// <returns>The current code status of the cpu. </returns>
        Task<CpuCodeResponse> GetCodeStatus();

        /// <summary>
        /// Gets the current control word of the cpu. 
        /// </summary>
        /// <returns>The current control word of the cpu. </returns>
        Task<CpuControlWordResponse> GetControlWord();

        /// <summary>
        /// Gets the current instruction status of the cpu. 
        /// </summary>
        /// <returns>The current instruction status of the cpu. </returns>
        Task<CpuInstructionResponse> GetInstructionStatus();

        /// <summary>
        /// Sets the cpu mode to the given <paramref name="mode"/>.
        /// </summary>
        /// <param name="mode">The mode to send and set the cpu to. </param>
        Task SetMode(CpuMode mode);

        /// <summary>
        /// Sets the cpu code to the given <paramref name="code"/>.
        /// </summary>
        /// <param name="code">The code to send to the cpu. </param>
        Task SetCode(byte[] code);

        /// <summary>
        /// Resets the cpu. 
        /// </summary>
        Task Reset();
    }
}