using Payment.Zvt.Responses;
using System;

namespace Payment.Zvt.Models
{
    /// <summary>
    /// BmpInfo
    /// </summary>
    public class BmpInfo
    {
        /// <summary>
        /// Id
        /// </summary>
        public byte Id { get; set; }
        /// <summary>
        /// DataLength
        /// </summary>
        public int DataLength { get; set; }

        /// <summary>
        /// CalculateDataLength
        /// </summary>
        public Func<byte[], int> CalculateDataLength { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// TryParse
        /// </summary>
        public Func<byte[], IResponse, bool> TryParse;

        /// <summary>
        /// ToString
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{this.Description} - {this.Id:X2}";
        }
    }
}
