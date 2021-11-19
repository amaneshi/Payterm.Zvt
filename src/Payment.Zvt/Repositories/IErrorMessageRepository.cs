namespace Payment.Zvt.Repositories
{
    /// <summary>
    /// IErrorMessageRepository
    /// </summary>
    public interface IErrorMessageRepository
    {
        /// <summary>
        /// GetMessage
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        string GetMessage(byte key);
    }
}
