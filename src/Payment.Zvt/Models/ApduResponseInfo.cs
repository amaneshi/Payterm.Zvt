using System.Linq;

namespace Payment.Zvt.Models
{
    /// <summary>
    /// Zvt - Application Protocol Data Unit
    /// </summary>
    public class ApduResponseInfo
    {
        /// <summary>
        /// ControlField
        /// </summary>
        public byte[] ControlField { get; set; }
        /// <summary>
        /// DataLength
        /// </summary>
        public int DataLength { get; set; }
        /// <summary>
        /// DataStartIndex
        /// </summary>
        public int DataStartIndex { get; set; }

        /// <summary>
        /// CanHandle
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool CanHandle(params byte[] data)
        {
            return this.ControlField.SequenceEqual(data);
        }
    }
}
