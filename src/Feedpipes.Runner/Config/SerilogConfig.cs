using System;
using System.Collections.Generic;
using System.Linq;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using SimpleInjector;

namespace Feedpipes.Runner.Config
{
    internal static class SerilogConfig
    {
        public static void SetupConsoleLogging()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo
                .Console(outputTemplate: @"[{Timestamp:HH:mm:ss} {Level:u3}] [{SourceContext}] {Message:lj}{NewLine}{Exception}")
                .Enrich.With(new SimplifySourceContextLogEventEnricher())
                .MinimumLevel.Verbose()
                .CreateLogger();
        }

        public static void RegisterDependencies(Container container)
        {
            container.RegisterConditional(typeof(ILogger), typeFactoryContext => typeof(LoggerWithSourceContextProxy<>).MakeGenericType(typeFactoryContext.Consumer.ImplementationType), Lifestyle.Singleton, _ => true);
        }

        private class LoggerWithSourceContextProxy<TContext> : ILogger
        {
            private readonly ILogger _innerLogger;

            public LoggerWithSourceContextProxy()
            {
                _innerLogger = Log.Logger.ForContext(typeof(TContext));
            }

            public ILogger ForContext(ILogEventEnricher enricher) => _innerLogger.ForContext(enricher);

            public ILogger ForContext(IEnumerable<ILogEventEnricher> enrichers) => _innerLogger.ForContext(enrichers);

            public ILogger ForContext(string propertyName, object value, bool destructureObjects = false) => _innerLogger.ForContext(propertyName, value, destructureObjects);

            public ILogger ForContext<TSource>() => _innerLogger.ForContext<TSource>();

            public ILogger ForContext(Type source) => _innerLogger.ForContext(source);

            public void Write(LogEvent logEvent)
            {
                _innerLogger.Write(logEvent);
            }

            public void Write(LogEventLevel level, string messageTemplate)
            {
                _innerLogger.Write(level, messageTemplate);
            }

            public void Write<T>(LogEventLevel level, string messageTemplate, T propertyValue)
            {
                _innerLogger.Write(level, messageTemplate, propertyValue);
            }

            public void Write<T0, T1>(LogEventLevel level, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
            {
                _innerLogger.Write(level, messageTemplate, propertyValue0, propertyValue1);
            }

            public void Write<T0, T1, T2>(LogEventLevel level, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
            {
                _innerLogger.Write(level, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
            }

            public void Write(LogEventLevel level, string messageTemplate, params object[] propertyValues)
            {
                _innerLogger.Write(level, messageTemplate, propertyValues);
            }

            public void Write(LogEventLevel level, Exception exception, string messageTemplate)
            {
                _innerLogger.Write(level, exception, messageTemplate);
            }

            public void Write<T>(LogEventLevel level, Exception exception, string messageTemplate, T propertyValue)
            {
                _innerLogger.Write(level, exception, messageTemplate, propertyValue);
            }

            public void Write<T0, T1>(LogEventLevel level, Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
            {
                _innerLogger.Write(level, exception, messageTemplate, propertyValue0, propertyValue1);
            }

            public void Write<T0, T1, T2>(LogEventLevel level, Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
            {
                _innerLogger.Write(level, exception, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
            }

            public void Write(LogEventLevel level, Exception exception, string messageTemplate, params object[] propertyValues)
            {
                _innerLogger.Write(level, exception, messageTemplate, propertyValues);
            }

            public bool IsEnabled(LogEventLevel level) => _innerLogger.IsEnabled(level);

            public void Verbose(string messageTemplate)
            {
                _innerLogger.Verbose(messageTemplate);
            }

            public void Verbose<T>(string messageTemplate, T propertyValue)
            {
                _innerLogger.Verbose(messageTemplate, propertyValue);
            }

            public void Verbose<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
            {
                _innerLogger.Verbose(messageTemplate, propertyValue0, propertyValue1);
            }

            public void Verbose<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
            {
                _innerLogger.Verbose(messageTemplate, propertyValue0, propertyValue1, propertyValue2);
            }

            public void Verbose(string messageTemplate, params object[] propertyValues)
            {
                _innerLogger.Verbose(messageTemplate, propertyValues);
            }

            public void Verbose(Exception exception, string messageTemplate)
            {
                _innerLogger.Verbose(exception, messageTemplate);
            }

            public void Verbose<T>(Exception exception, string messageTemplate, T propertyValue)
            {
                _innerLogger.Verbose(exception, messageTemplate, propertyValue);
            }

            public void Verbose<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
            {
                _innerLogger.Verbose(exception, messageTemplate, propertyValue0, propertyValue1);
            }

            public void Verbose<T0, T1, T2>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
            {
                _innerLogger.Verbose(exception, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
            }

            public void Verbose(Exception exception, string messageTemplate, params object[] propertyValues)
            {
                _innerLogger.Verbose(exception, messageTemplate, propertyValues);
            }

            public void Debug(string messageTemplate)
            {
                _innerLogger.Debug(messageTemplate);
            }

            public void Debug<T>(string messageTemplate, T propertyValue)
            {
                _innerLogger.Debug(messageTemplate, propertyValue);
            }

            public void Debug<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
            {
                _innerLogger.Debug(messageTemplate, propertyValue0, propertyValue1);
            }

            public void Debug<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
            {
                _innerLogger.Debug(messageTemplate, propertyValue0, propertyValue1, propertyValue2);
            }

            public void Debug(string messageTemplate, params object[] propertyValues)
            {
                _innerLogger.Debug(messageTemplate, propertyValues);
            }

            public void Debug(Exception exception, string messageTemplate)
            {
                _innerLogger.Debug(exception, messageTemplate);
            }

            public void Debug<T>(Exception exception, string messageTemplate, T propertyValue)
            {
                _innerLogger.Debug(exception, messageTemplate, propertyValue);
            }

            public void Debug<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
            {
                _innerLogger.Debug(exception, messageTemplate, propertyValue0, propertyValue1);
            }

            public void Debug<T0, T1, T2>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
            {
                _innerLogger.Debug(exception, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
            }

            public void Debug(Exception exception, string messageTemplate, params object[] propertyValues)
            {
                _innerLogger.Debug(exception, messageTemplate, propertyValues);
            }

            public void Information(string messageTemplate)
            {
                _innerLogger.Information(messageTemplate);
            }

            public void Information<T>(string messageTemplate, T propertyValue)
            {
                _innerLogger.Information(messageTemplate, propertyValue);
            }

            public void Information<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
            {
                _innerLogger.Information(messageTemplate, propertyValue0, propertyValue1);
            }

            public void Information<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
            {
                _innerLogger.Information(messageTemplate, propertyValue0, propertyValue1, propertyValue2);
            }

            public void Information(string messageTemplate, params object[] propertyValues)
            {
                _innerLogger.Information(messageTemplate, propertyValues);
            }

            public void Information(Exception exception, string messageTemplate)
            {
                _innerLogger.Information(exception, messageTemplate);
            }

            public void Information<T>(Exception exception, string messageTemplate, T propertyValue)
            {
                _innerLogger.Information(exception, messageTemplate, propertyValue);
            }

            public void Information<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
            {
                _innerLogger.Information(exception, messageTemplate, propertyValue0, propertyValue1);
            }

            public void Information<T0, T1, T2>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
            {
                _innerLogger.Information(exception, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
            }

            public void Information(Exception exception, string messageTemplate, params object[] propertyValues)
            {
                _innerLogger.Information(exception, messageTemplate, propertyValues);
            }

            public void Warning(string messageTemplate)
            {
                _innerLogger.Warning(messageTemplate);
            }

            public void Warning<T>(string messageTemplate, T propertyValue)
            {
                _innerLogger.Warning(messageTemplate, propertyValue);
            }

            public void Warning<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
            {
                _innerLogger.Warning(messageTemplate, propertyValue0, propertyValue1);
            }

            public void Warning<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
            {
                _innerLogger.Warning(messageTemplate, propertyValue0, propertyValue1, propertyValue2);
            }

            public void Warning(string messageTemplate, params object[] propertyValues)
            {
                _innerLogger.Warning(messageTemplate, propertyValues);
            }

            public void Warning(Exception exception, string messageTemplate)
            {
                _innerLogger.Warning(exception, messageTemplate);
            }

            public void Warning<T>(Exception exception, string messageTemplate, T propertyValue)
            {
                _innerLogger.Warning(exception, messageTemplate, propertyValue);
            }

            public void Warning<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
            {
                _innerLogger.Warning(exception, messageTemplate, propertyValue0, propertyValue1);
            }

            public void Warning<T0, T1, T2>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
            {
                _innerLogger.Warning(exception, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
            }

            public void Warning(Exception exception, string messageTemplate, params object[] propertyValues)
            {
                _innerLogger.Warning(exception, messageTemplate, propertyValues);
            }

            public void Error(string messageTemplate)
            {
                _innerLogger.Error(messageTemplate);
            }

            public void Error<T>(string messageTemplate, T propertyValue)
            {
                _innerLogger.Error(messageTemplate, propertyValue);
            }

            public void Error<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
            {
                _innerLogger.Error(messageTemplate, propertyValue0, propertyValue1);
            }

            public void Error<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
            {
                _innerLogger.Error(messageTemplate, propertyValue0, propertyValue1, propertyValue2);
            }

            public void Error(string messageTemplate, params object[] propertyValues)
            {
                _innerLogger.Error(messageTemplate, propertyValues);
            }

            public void Error(Exception exception, string messageTemplate)
            {
                _innerLogger.Error(exception, messageTemplate);
            }

            public void Error<T>(Exception exception, string messageTemplate, T propertyValue)
            {
                _innerLogger.Error(exception, messageTemplate, propertyValue);
            }

            public void Error<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
            {
                _innerLogger.Error(exception, messageTemplate, propertyValue0, propertyValue1);
            }

            public void Error<T0, T1, T2>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
            {
                _innerLogger.Error(exception, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
            }

            public void Error(Exception exception, string messageTemplate, params object[] propertyValues)
            {
                _innerLogger.Error(exception, messageTemplate, propertyValues);
            }

            public void Fatal(string messageTemplate)
            {
                _innerLogger.Fatal(messageTemplate);
            }

            public void Fatal<T>(string messageTemplate, T propertyValue)
            {
                _innerLogger.Fatal(messageTemplate, propertyValue);
            }

            public void Fatal<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
            {
                _innerLogger.Fatal(messageTemplate, propertyValue0, propertyValue1);
            }

            public void Fatal<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
            {
                _innerLogger.Fatal(messageTemplate, propertyValue0, propertyValue1, propertyValue2);
            }

            public void Fatal(string messageTemplate, params object[] propertyValues)
            {
                _innerLogger.Fatal(messageTemplate, propertyValues);
            }

            public void Fatal(Exception exception, string messageTemplate)
            {
                _innerLogger.Fatal(exception, messageTemplate);
            }

            public void Fatal<T>(Exception exception, string messageTemplate, T propertyValue)
            {
                _innerLogger.Fatal(exception, messageTemplate, propertyValue);
            }

            public void Fatal<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
            {
                _innerLogger.Fatal(exception, messageTemplate, propertyValue0, propertyValue1);
            }

            public void Fatal<T0, T1, T2>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
            {
                _innerLogger.Fatal(exception, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
            }

            public void Fatal(Exception exception, string messageTemplate, params object[] propertyValues)
            {
                _innerLogger.Fatal(exception, messageTemplate, propertyValues);
            }

            public bool BindMessageTemplate(string messageTemplate, object[] propertyValues, out MessageTemplate parsedTemplate, out IEnumerable<LogEventProperty> boundProperties) => _innerLogger.BindMessageTemplate(messageTemplate, propertyValues, out parsedTemplate, out boundProperties);

            public bool BindProperty(string propertyName, object value, bool destructureObjects, out LogEventProperty property) => _innerLogger.BindProperty(propertyName, value, destructureObjects, out property);
        }

        /// <summary>
        /// Only shows the class name in the "SourceContext" parameter, not the fully-qualified name.
        /// </summary>
        private class SimplifySourceContextLogEventEnricher : ILogEventEnricher
        {
            public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
            {
                const string propertyNameSourceContext = "SourceContext";

                if (!logEvent.Properties.TryGetValue(propertyNameSourceContext, out var existingSourceContextValue))
                    return;

                if (!(existingSourceContextValue is ScalarValue existingScalarValue))
                    return;

                if (!(existingScalarValue.Value is string existingStringValue))
                    return;

                var beforeGenericPart = existingStringValue.Split('`').First();
                var lastClassName = beforeGenericPart.Split('.').Last();

                logEvent.AddOrUpdateProperty(propertyFactory.CreateProperty(propertyNameSourceContext, lastClassName));
            }
        }
    }
}