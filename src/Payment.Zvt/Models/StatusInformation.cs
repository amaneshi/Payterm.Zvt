using Payment.Zvt.Responses;
using System;

namespace Payment.Zvt.Models
{
    /// <summary>
    /// StatusInformation
    /// </summary>
    public class StatusInformation :
        IResponse,
        IResponseErrorMessage,
        IResponseAdditionalText,
        IResponseTerminalIdentifier,
        IResponseAmount,
        IResponseCardName,
        IResponseCardholderAuthentication,
        IResponseCardTechnology,
        IResponseTime,
        IResponseCurrencyCode,
        IResponseReceiptNumber,
        IResponseTraceNumber,
        IResponseVuNumber,
        IResponseAidAuthorisationAttribute
    {
        /// <summary>
        /// ErrorMessage
        /// </summary>
        public string ErrorMessage { get; set; }
        /// <summary>
        /// TerminalIdentifier
        /// </summary>
        public int TerminalIdentifier { get; set; }
        /// <summary>
        /// AdditionalText
        /// </summary>
        public string AdditionalText { get; set; }
        /// <summary>
        /// CardName
        /// </summary>
        public string CardName { get; set; }
        /// <summary>
        /// Amount
        /// </summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// CardholderAuthentication
        /// </summary>
        public string CardholderAuthentication { get; set; }
        /// <summary>
        /// CardTechnology
        /// </summary>
        public string CardTechnology { get; set; }
        /// <summary>
        /// Time
        /// </summary>
        public TimeSpan Time { get; set; }
        /// <summary>
        /// CurrencyCode
        /// </summary>
        public int CurrencyCode { get; set; }
        /// <summary>
        /// ReceiptNumber
        /// </summary>
        public int ReceiptNumber { get; set; }
        /// <summary>
        /// TraceNumber
        /// </summary>
        public int TraceNumber { get; set; }
        /// <summary>
        /// VuNumber
        /// </summary>
        public string VuNumber { get; set; }
        /// <summary>
        /// AidAuthorisationAttribute
        /// </summary>
        public string AidAuthorisationAttribute { get; set; }
    }
}
