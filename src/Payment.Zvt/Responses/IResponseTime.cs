using System;

namespace Payment.Zvt.Responses
{
    /// <summary>
    /// IResponseTime
    /// </summary>
    public interface IResponseTime
    {
        /// <summary>
        /// Time
        /// </summary>
        TimeSpan Time { get; set; }
    }
}
