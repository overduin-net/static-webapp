using Microsoft.Extensions.Logging;
using System;

namespace StaticWebApp.Template
{
    public interface ILoggerHelper
    {
        /// <summary>
        /// Log Debug message.
        /// </summary>
        /// <param name="message"></param>
        void LogDebug(string message);

        /// <summary>
        /// Log information message
        /// </summary>
        /// <param name="message"></param>
        void LogInformation(string message);

        /// <summary>
        /// Log Trace
        /// </summary>
        /// <param name="message"></param>
        void LogTrace(string message);

        /// <summary>
        /// Log Warnings
        /// </summary>
        /// <param name="message"></param>
        void LogWarning(string message);

        /// <summary>
        /// Log error messages
        /// </summary>
        /// <param name="message"></param>
        void LogError(string message);
        void LogError(Exception exception, string message);

        /// <summary>
        /// Log critical messages
        /// </summary>
        /// <param name="message"></param>
        void LogCritical(string message);
    }

    public class LoggerHelper : ILoggerHelper
    {
        private readonly ILogger<LoggerHelper> _logger;

        public LoggerHelper(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<LoggerHelper>();
        }

        /// <summary>
        /// Log Debug message.
        /// </summary>
        /// <param name="message"></param>
        public void LogDebug(string message)
        {
            _logger.LogDebug(message);
        }

        /// <summary>
        /// Log information messages
        /// </summary>
        /// <param name="message"></param>
        public void LogInformation(string message)
        {
            _logger.LogInformation(message);
        }

        /// <summary>
        /// Log trace messages
        /// </summary>
        /// <param name="message"></param>
        public void LogTrace(string message)
        {
            _logger.LogTrace(message);
        }

        /// <summary>
        /// Log warning messages.
        /// </summary>
        /// <param name="message"></param>
        public void LogWarning(string message)
        {
            _logger.LogWarning(message);
        }

        /// <summary>
        /// Log error messages.
        /// </summary>
        /// <param name="message"></param>
        public void LogError(string message)
        {
            _logger.LogError(message);            
        }

        /// <summary>
        /// Log error messages.
        /// </summary>
        /// <param name="message"></param>
        public void LogError(Exception exception, string message)
        {
            _logger.LogError(exception, message);
        }

        /// <summary>
        /// Log critical messages.
        /// </summary>
        /// <param name="message"></param>
        public void LogCritical(string message)
        {
            _logger.LogCritical(message);
        }
    }
}
