using System;
using CODE.Framework.Core.Utilities;
using NLog;
using Logger = NLog.Logger;

namespace LoggingLibrary
{
    public class NLogLogger : ILogger, INLogger
    {

        private Logger _logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Implementation of CODE Framework Logging Mediator for use with NLog
        /// </summary>
        /// <param name="logEvent">String Message for NLog to record</param>
        /// <param name="type">LogEventType from <see cref="LogEventType"/>></param>
        public void Log(string logEvent, LogEventType type)
        {
            switch (type)
            {
                case LogEventType.Undefined:
                    _logger.Info(logEvent);
                    break;
                case LogEventType.Information:
                    _logger.Info(logEvent);
                    break;
                case LogEventType.Warning:
                    _logger.Warn(logEvent);
                    break;
                case LogEventType.Exception:
                    _logger.Fatal(logEvent);
                    break;
                case LogEventType.Error:
                    _logger.Error(logEvent);
                    break;
                case LogEventType.Critical:
                    _logger.Fatal(logEvent);
                    break;
                case LogEventType.Success:
                    _logger.Info(logEvent);
                    break;
                default:
                    break;
            }
        }

        public void Log(object logEvent, LogEventType type)
        {
            var entry = logEvent as NLogEntry;
            Log(entry);
        }

        public LogEventType TypeFilter { get; set; }

        public void Log(NLogEntry entry)
        {
            _logger = LogManager.GetLogger(entry.Source);
            Log(entry.Message, entry.Severity.Convert());
        }



    }

    public class NLogExceptionLogger : IExceptionLogger, INLogger
    {
        private Logger _logger = LogManager.GetCurrentClassLogger();
        public void Log(Exception exception, LogEventType type)
        {
            Log(exception.Message,exception, type);
        }
        public void Log(string leadingText, Exception exception, LogEventType type)
        {
            switch (type)
            {
                case LogEventType.Undefined:
                    _logger.InfoException(leadingText, exception);
                    break;
                case LogEventType.Information:
                    _logger.InfoException(leadingText, exception);
                    break;
                case LogEventType.Warning:
                    _logger.WarnException(leadingText, exception);
                    break;
                case LogEventType.Exception:
                    _logger.FatalException(leadingText, exception);
                    break;
                case LogEventType.Error:
                    _logger.ErrorException(leadingText, exception);
                    break;
                case LogEventType.Critical:
                    _logger.FatalException(leadingText, exception);
                    break;
                case LogEventType.Success:
                    _logger.InfoException(leadingText, exception);
                    break;
                default:
                    break;
            }
        }

        public void Log(NLogEntry entry)
        {
            _logger = LogManager.GetLogger(entry.Source);
            Log(entry.Message, entry.Exception, entry.Severity.Convert());
        }
    }

    public interface INLogger
    {
        void Log(NLogEntry entry);
    }

    public static class LoggerExtensions
    {
        public static void Log(this INLogger logger, string message)
        {
            logger.Log(new NLogEntry(LogLevel.Info,
                message, null, null));
        }

        public static void Log(this INLogger logger, Exception exception)
        {
            logger.Log(new NLogEntry(LogLevel.Error,
                exception.Message, null, exception));
        }

        /// <summary>
        /// Convert CODE Framework LogEventType to NLog LogLevel
        /// </summary>
        public static LogLevel Convert(this LogEventType logEvent)
        {
            switch (logEvent)
            {
                case LogEventType.Undefined:
                    return LogLevel.Info;
                    break;
                case LogEventType.Information:
                    return LogLevel.Info;
                    break;
                case LogEventType.Warning:
                    return LogLevel.Warn;
                    break;
                case LogEventType.Exception:
                    return LogLevel.Error;
                    break;
                case LogEventType.Error:
                    return LogLevel.Error;
                    break;
                case LogEventType.Critical:
                    return LogLevel.Fatal;
                    break;
                case LogEventType.Success:
                    return LogLevel.Debug;
                    break;
                default:
                    return LogLevel.Trace;
                    break;
            }
        }

        /// <summary>
        /// Convert NLog LogLevel to CODE Framework LogEventType
        /// </summary>
        public static LogEventType Convert(this LogLevel logEvent)
        {
            switch (logEvent.Name)
            {
                case "Trace":
                    return LogEventType.Information;
                    break;
                case "Debug":
                    return LogEventType.Information;
                    break;
                case "Info":
                    return LogEventType.Information;
                    break;
                case "Warn":
                    return LogEventType.Warning;
                    break;
                case "Error":
                    return LogEventType.Error;
                    break;
                case "Fatal":
                    return LogEventType.Critical;
                    break;
                default:
                    return LogEventType.Undefined;
                    break;
            }            
        }

        // More methods here.
    }

    public class NLogEntry
    {
        public readonly LogLevel Severity;
        public readonly string Message;
        public readonly string Source;
        public readonly Exception Exception;

        public NLogEntry(LogLevel severity, string message, string source,
            Exception exception)
        {
            if (message == null)
                throw new ArgumentNullException("message");
            if (string.IsNullOrEmpty(message))
                throw new ArgumentException("message");
            if (severity < LogLevel.Debug || severity > LogLevel.Fatal)
                throw new ArgumentOutOfRangeException("severity");

            this.Severity = severity;
            this.Message = message;
            this.Source = source;
            this.Exception = exception;
        }
    }
}
