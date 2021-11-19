using Payment.Zvt.Models;
using System;

namespace Payment.Zvt.Parsers
{
    /// <summary>
    /// StatusInformationParser Interface
    /// </summary>
    public interface IStatusInformationParser
    {
        /// <summary>
        /// Parse
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        StatusInformation Parse(byte[] data);
    }
}