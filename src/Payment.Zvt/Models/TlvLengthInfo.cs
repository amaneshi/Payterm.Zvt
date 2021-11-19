namespace Payment.Zvt.Models
{
    /// <summary>
    /// TlvLengthInfo
    /// </summary>
    public class TlvLengthInfo
    {
        /// <summary>
        /// Successful
        /// </summary>
        public bool Successful { get; set; }

        /// <summary>
        /// Length
        /// </summary>
        public int Length { get; set; }

        /// <summary>
        /// NumberOfBytesThatCanBeSkipped
        /// </summary>
        public int NumberOfBytesThatCanBeSkipped { get; set; }
    }
}
