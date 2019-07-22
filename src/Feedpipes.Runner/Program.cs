using System;
using System.Threading;
using System.Threading.Tasks;
using Feedpipes.Runner.Config;
using Serilog;

namespace Feedpipes.Runner
{
    internal class Program
    {
        private static ILogger Log;

        private static async Task Main()
        {
            SerilogConfig.SetupConsoleLogging();

            Log = Serilog.Log.ForContext(typeof(Program));
            Log.Information("Program started.");

            AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
            {
                Log.Error(e.ExceptionObject as Exception, "Unhandled exception occurred.");
                Log.Information("Press any key to exit.");
                Console.ReadKey();
                Environment.Exit(1);
            };

            var cancellationTokenSource = new CancellationTokenSource();
            Console.CancelKeyPress += (_, e) =>
            {
                Log.Warning("Cancel key pressed, terminating...");
                e.Cancel = true; // prevent the process from terminating
                cancellationTokenSource.Cancel();
            };

            using (var container = SimpleInjectorConfig.SetupContainer())
            {
                var runner = container.GetInstance<Runner>();
                await runner.Run(cancellationTokenSource.Token);
            }

            Log.Information("--- Press any key to exit ---");
            Console.ReadKey();
        }
    }
}