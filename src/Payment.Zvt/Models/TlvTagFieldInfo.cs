namespace Payment.Zvt.Models
{
    /// <summary>
    /// TlvTagFieldInfo
    /// </summary>
    public class TlvTagFieldInfo
    {
        /// <summary>
        /// ClassType
        /// </summary>
        public TlvTagFieldClassType ClassType { get; set; }
        /// <summary>
        /// DataObjectType
        /// </summary>
        public TlvTagFieldDataObjectType DataObjectType { get; set; }
        /// <summary>
        /// TagNumber
        /// </summary>
        public int TagNumber { get; set; }
        /// <summary>
        /// Tag
        /// </summary>
        public string Tag { get; set; }
        /// <summary>
        /// NumberOfBytesThatCanBeSkipped
        /// </summary>
        public int NumberOfBytesThatCanBeSkipped { get; set; }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"ClassType:{this.ClassType} DataObjectType:{this.DataObjectType} TagNumber:{this.TagNumber} Tag:{this.Tag} NumberOfBytesThatCanBeSkipped:{this.NumberOfBytesThatCanBeSkipped}";
        }
    }
}
