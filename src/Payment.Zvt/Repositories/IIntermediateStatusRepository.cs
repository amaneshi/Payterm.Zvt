namespace Payment.Zvt.Repositories
{
    /// <summary>
    /// IIntermediateStatusRepository
    /// </summary>
    public interface IIntermediateStatusRepository
    {
        /// <summary>
        /// GetMessage
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        string GetMessage(byte key);
    }
}
