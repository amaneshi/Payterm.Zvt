using Payment.Zvt.Models;
using Payment.Zvt.Responses;
using System;

namespace Payment.Zvt.Parsers
{
    /// <summary>
    /// ITlvParser
    /// </summary>
    public interface ITlvParser
    {
        /// <summary>
        /// Parse
        /// </summary>
        /// <param name="data"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        bool Parse(byte[] data, IResponse response);
        /// <summary>
        /// GetLength
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        TlvLengthInfo GetLength(byte[] data);

    }
}