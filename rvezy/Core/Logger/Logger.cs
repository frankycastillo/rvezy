using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Serilog.Events;
using Serilog.Parsing;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace rvezy.Core.Logger
{
    public static class LoggerExtensions
    {
        public static LogEventLevel AsSerilogLevel(this LogLevel level)
        {
            var result = LogEventLevel.Debug;
            switch (level)
            {
                case LogLevel.Trace:
                case LogLevel.Debug:
                    result= LogEventLevel.Debug;
                    break;
                case LogLevel.Information:
                    result = LogEventLevel.Information;
                    break;
                case LogLevel.Warning:
                    result = LogEventLevel.Warning;
                    break;
                case LogLevel.Error:
                    result = LogEventLevel.Error;
                    break;
                case LogLevel.Critical:
                    result = LogEventLevel.Fatal;
                    break;
                case LogLevel.None:
                    result = LogEventLevel.Debug;
                    break;
                default:
                    result = LogEventLevel.Debug;
                    break;
            }

            return result;
        }
    }
    
    public class Logger : DisposibleService, ILogger
    {
        private readonly Serilog.ILogger _logger;
        private readonly LogLevel _minLevel;

        public Logger(IConfiguration configuration)
        {
            _logger = Serilog.Log.Logger.ForContext(MethodBase.GetCurrentMethod().DeclaringType);
        }

        public void Trace(string message, string filePath = null, string memberName = null, int lineNumber = 0)
        {
            WriteLog(LogLevel.Trace, message, filePath, memberName, lineNumber);
        }

        public void Debug(string message, string filePath = null, string memberName = null, int lineNumber = 0)
        {
            WriteLog(LogLevel.Debug, message, filePath, memberName, lineNumber);
        }

        public void Info(string message, string filePath = null, string memberName = null, int lineNumber = 0)
        {
            WriteLog(LogLevel.Information, message, filePath, memberName, lineNumber);
        }

        public void Warn(string message, string filePath = null, string memberName = null, int lineNumber = 0)
        {
            WriteLog(LogLevel.Warning, message, filePath, memberName, lineNumber);
        }

        public void Error(string message, string filePath = null, string memberName = null, int lineNumber = 0)
        {
            WriteLog(LogLevel.Error, message, filePath, memberName, lineNumber);
        }

        public void Error(Exception ex, string message, string filePath = null, string memberName = null, int lineNumber = 0)
        {
            message = string.IsNullOrWhiteSpace(message) ? ex.Message : message;
            WriteLog(LogLevel.Error, message, filePath, memberName, lineNumber);
        }

        public void Fatal(string message, string filePath = null, string memberName = null, int lineNumber = 0)
        {
            WriteLog(LogLevel.Critical, message, filePath, memberName, lineNumber);
        }

        public void Fatal(Exception ex, string message, string filePath = null, string memberName = null, int lineNumber = 0)
        {
            message = string.IsNullOrWhiteSpace(message) ? ex.Message : message;
            WriteLog(LogLevel.Critical, message, filePath, memberName, lineNumber);
        }

        public void Log(LogLevel level, string message, string filePath, string memberName, int lineNumber)
        {
            WriteLog(level, message, filePath, memberName, lineNumber);
        }

        private void WriteLog(LogLevel level, string message, string filePath, string memberName, int lineNumber)
        {
            if (level >= _minLevel)
            {
                var serilogLevel = level.AsSerilogLevel();

                var messageTemplate = new MessageTemplate(
                    new List<MessageTemplateToken>
                    {
                        new TextToken(message),
                    });

                var properties = new List<LogEventProperty>();
             
                properties.Add(new LogEventProperty("_callerFileName", new ScalarValue(Path.GetFileNameWithoutExtension(filePath))));
                properties.Add(new LogEventProperty("_callerMemberName", new ScalarValue(memberName)));
                properties.Add(new LogEventProperty("_callerLineNumber", new ScalarValue(lineNumber)));

                  var eventInfo = new LogEvent(DateTimeOffset.Now, serilogLevel, null, messageTemplate, properties);

                _logger.Write(eventInfo);
            }
        }
    }
}