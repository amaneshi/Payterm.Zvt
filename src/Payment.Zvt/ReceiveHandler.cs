using Microsoft.Extensions.Logging;
using Payment.Zvt.Helpers;
using Payment.Zvt.Models;
using Payment.Zvt.Parsers;
using Payment.Zvt.Repositories;
using System;

namespace Payment.Zvt
{
    /// <summary>
    /// ReceiveHandler
    /// </summary>
    public class ReceiveHandler : IReceiveHandler
    {
        private readonly ILogger _logger;
        private readonly IErrorMessageRepository _errorMessageRepository;

        private readonly IPrintLineParser _printLineParser;
        private readonly IPrintTextBlockParser _printTextBlockParser;
        private readonly IStatusInformationParser _statusInformationParser;
        private readonly IIntermediateStatusInformationParser _intermediateStatusInformationParser;

        /// <inheritdoc />
        public event Action<PrintLineInfo> LineReceived;

        /// <inheritdoc />
        public event Action<ReceiptInfo> ReceiptReceived;

        /// <inheritdoc />
        public event Action<StatusInformation> StatusInformationReceived;

        /// <inheritdoc />
        public event Action<string> IntermediateStatusInformationReceived;

        /// <inheritdoc />
        public event Action CompletionReceived;

        /// <inheritdoc />
        public event Action<string> AbortReceived;

        /// <inheritdoc />
        public event Action NotSupportedReceived;

        /// <summary>
        /// Buffer for split messages
        /// </summary>
        private byte[] _buffer;

        /// <summary>
        /// Buffered package to parse
        /// </summary>
        private byte[] _package;

        /// <summary>
        /// ReceiveHandler
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="errorMessageRepository"></param>
        /// <param name="intermediateStatusRepository"></param>
        /// <param name="printLineParser"></param>
        /// <param name="printTextBlockParser"></param>
        /// <param name="statusInformationParser"></param>
        /// <param name="intermediateStatusInformationParser"></param>
        public ReceiveHandler(
            ILogger logger,
            IErrorMessageRepository errorMessageRepository,
            IIntermediateStatusRepository intermediateStatusRepository,
            IPrintLineParser printLineParser = default,
            IPrintTextBlockParser printTextBlockParser = default,
            IStatusInformationParser statusInformationParser = default,
            IIntermediateStatusInformationParser intermediateStatusInformationParser = default)
        {
            this._logger = logger;
            this._errorMessageRepository = errorMessageRepository;

            this._printLineParser = printLineParser == default
                ? new PrintLineParser(logger)
                : printLineParser;

            this._printTextBlockParser = printTextBlockParser == default
                ? new PrintTextBlockParser(logger, errorMessageRepository)
                : printTextBlockParser;

            this._statusInformationParser = statusInformationParser == default
                ? new StatusInformationParser(logger, errorMessageRepository)
                : statusInformationParser;

            this._intermediateStatusInformationParser = intermediateStatusInformationParser == default
                ? new IntermediateStatusInformationParser(logger, intermediateStatusRepository, errorMessageRepository)
                : intermediateStatusInformationParser;
        }

        /// <inheritdoc />
        public bool ProcessData(byte[] data)
        {
            var apduInfo = ApduHelper.GetApduInfo(data);
            if (apduInfo.ControlField == null)
            {
                return false;
            }

            if (data.Length != apduInfo.DataStartIndex + apduInfo.DataLength)
            {
                if (CheckBuffer(apduInfo, data, out var fragment))
                {
                    return ProcessData(this._package);
                }
                if (fragment)
                {
                    this._logger.LogWarning($"{nameof(ProcessData)} - Apdu data part possible fragmentation");
                    return true;
                }
                this._logger.LogError($"{nameof(ProcessData)} - Apdu data part corrupt");
                return false;
            }

            var apduData = data.Slice(apduInfo.DataStartIndex, apduInfo.DataLength);

            //Status Information
            if (apduInfo.CanHandle(0x04, 0x0F))
            {
                var statusInformation = this._statusInformationParser.Parse(apduData);
                this.StatusInformationReceived?.Invoke(statusInformation);
                return true;
            }

            //Intermediate Status Information
            if (apduInfo.CanHandle(0x04, 0xFF))
            {
                var intermediateStatusInformation = this._intermediateStatusInformationParser.GetMessage(data);
                this.IntermediateStatusInformationReceived?.Invoke(intermediateStatusInformation);
                return true;
            }

            //Print Line
            if (apduInfo.CanHandle(0x06, 0xD1))
            {
                //Use apdu length info, length is hardcoded
                var printLineInfo = this._printLineParser.Parse(apduData);
                this.LineReceived?.Invoke(printLineInfo);
                return true;
            }

            //Print Text-Block
            if (apduInfo.CanHandle(0x06, 0xD3))
            {
                var receipt = this._printTextBlockParser.Parse(apduData);
                this.ReceiptReceived?.Invoke(receipt);
                return true;
            }

            //Command not supported (2.67 Other Commands)
            if (apduInfo.CanHandle(0x84, 0x83))
            {
                this._logger.LogDebug($"{nameof(ProcessData)} - 'Command not supported' received");
                this.NotSupportedReceived?.Invoke();
                return true;
                //TODO: Process this event and return an other error
            }

            //Completion (3.2 Completion)
            if (apduInfo.CanHandle(0x06, 0x0F))
            {
                this._logger.LogDebug($"{nameof(ProcessData)} - 'Completion' received");
                this.CompletionReceived?.Invoke();
                return true;
            }

            //Abort (3.3 Abort)
            if (apduInfo.CanHandle(0x06, 0x1E))
            {
                var errorCode = apduData[0];
                var errorMessage = this._errorMessageRepository.GetMessage(errorCode);

                this._logger.LogDebug($"{nameof(ProcessData)} - 'Abort' received {errorMessage}");
                this.AbortReceived?.Invoke(errorMessage);

                return true;
            }

            return false;
        }

        private bool CheckBuffer(ApduResponseInfo apduInfo, byte[] data, out bool fragment)
        {
            fragment = false;
            if (this._buffer == null
                && (apduInfo.CanHandle(0x04, 0x0F)
                || apduInfo.CanHandle(0x04, 0xFF)
                || apduInfo.CanHandle(0x06, 0xD1)
                || apduInfo.CanHandle(0x06, 0xD3)
                || apduInfo.CanHandle(0x84, 0x83)
                || apduInfo.CanHandle(0x06, 0x0F)
                || apduInfo.CanHandle(0x06, 0x1E))
                )
            {
                this._buffer = data;
                fragment = true;
                return false;
            }

            var newBuffer = new byte[this._buffer.Length + data.Length];
            Array.Copy(this._buffer, newBuffer, this._buffer.Length);
            Array.Copy(data, 0, newBuffer, this._buffer.Length, data.Length);
            var newApduInfo = ApduHelper.GetApduInfo(newBuffer);
            var packageLength = newApduInfo.DataStartIndex + newApduInfo.DataLength;
            if (newBuffer.Length < packageLength)
            {
                this._buffer = newBuffer;
                return false;
            }
            if (newBuffer.Length >= packageLength)
            {
                this._package = new byte[packageLength];
                Array.Copy(newBuffer, this._package, packageLength);
                if (newBuffer.Length == packageLength)
                {
                    this._buffer = null;
                }
                else
                {
                    var newBufferLength = newBuffer.Length - packageLength;
                    this._buffer = new byte[newBufferLength];
                    Array.Copy(newBuffer, packageLength, this._buffer, 0, newBufferLength);
                }
            }

            return true;
        }
    }
}
