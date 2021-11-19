using Payment.Zvt.Responses;
using System;

namespace Payment.Zvt.Models
{
    /// <summary>
    /// TlvInfo
    /// </summary>
    public class TlvInfo
    {
        /// <summary>
        /// Tag
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// TryProcess
        /// </summary>
        public Func<byte[], IResponse, bool> TryProcess;

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{this.Description} - {this.Tag}";
        }
    }
}
