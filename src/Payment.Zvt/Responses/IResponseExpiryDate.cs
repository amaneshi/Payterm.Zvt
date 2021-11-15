namespace Payment.Zvt.Responses
{
    /// <summary>
    /// IResponseExpiryDate
    /// </summary>
    public interface IResponseExpiryDate
    {
        /// <summary>
        /// ExpiryDateYear
        /// </summary>
        public int ExpiryDateYear { get; set; }
        /// <summary>
        /// ExpiryDateMonth
        /// </summary>
        public int ExpiryDateMonth { get; set; }
    }
}
