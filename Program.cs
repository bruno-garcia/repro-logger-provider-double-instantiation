using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace LoggerProviderDoubleInstantiation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureLogging(l => l.Services.AddSingleton<ILoggerProvider, SomeLoggerProvider>())
                .Configure(a =>
                {
                    a.Use(async (context, next) =>
                        {
                            await context.Response.WriteAsync("Instances created: " + SomeLoggerProvider.InstanceCount);
                        });
                })
                .Build();
    }

    internal class SomeLoggerProvider : ILoggerProvider
    {
        public static int InstanceCount = 0;
        public static int DisposeCount = 0;

        public SomeLoggerProvider()
        {
            InstanceCount++;
            Console.WriteLine("Creating instance. Total: " + InstanceCount);
        }

        public void Dispose()
        {
            DisposeCount++;
            Console.WriteLine("Dispose called. Total: " + DisposeCount);
        }

        public ILogger CreateLogger(string categoryName) => new Logger();

        private class Logger : ILogger
        {
            public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
            { }

            public bool IsEnabled(LogLevel logLevel) => false;

            public IDisposable BeginScope<TState>(TState state) => null;

        }
    }
}
