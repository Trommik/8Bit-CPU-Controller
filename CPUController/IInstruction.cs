namespace CPUController
{
    public interface IInstruction
    {
        /// <summary>
        /// The memory address of the instruction. 
        /// </summary>
        byte Address { get; set; }
        
        /// <summary>
        /// The memory value of the instruction. 
        /// </summary>
        byte Value { get; }

        /// <summary>
        /// Returns a byte array that represents the current instruction. 
        /// </summary>
        /// <returns>A byte array that represents the current instruction. </returns>
        byte[] ToBinary();
    }
}