namespace CPUController
{
    public class Config
    {
        /// <summary>
        /// The rest api endpoint. 
        /// </summary>
        public string Endpoint { get; set; } = string.Empty;

        /// <summary>
        /// The refresh rate in milliseconds.
        /// </summary>
        public int RefreshRate { get; set; }
    }
}