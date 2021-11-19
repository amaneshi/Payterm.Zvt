namespace Payment.Zvt.Models
{
    /// <summary>
    /// PrintLineInfo
    /// </summary>
    public class PrintLineInfo
    {
        /// <summary>
        /// IsTextCentred
        /// </summary>
        public bool IsTextCentred { get; set; }
        /// <summary>
        /// IsDoubleWidth
        /// </summary>
        public bool IsDoubleWidth { get; set; }
        /// <summary>
        /// IsDoubleHeight
        /// </summary>
        public bool IsDoubleHeight { get; set; }
        /// <summary>
        /// IsLastLine
        /// </summary>
        public bool IsLastLine { get; set; }
        /// <summary>
        /// Text
        /// </summary>
        public string Text { get; set; }
    }
}
