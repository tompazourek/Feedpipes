using Serilog;
using SimpleInjector;
using SimpleInjector.Lifestyles;

namespace Feedpipes.Runner.Config
{
    internal static class SimpleInjectorConfig
    {
        private static readonly ILogger Log = Serilog.Log.ForContext(typeof(SimpleInjectorConfig));

        public static Container SetupContainer()
        {
            var container = new Container();

            container.Options.DefaultLifestyle = Lifestyle.Transient;
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            SerilogConfig.RegisterDependencies(container);

            Log.Information("Container verification started.");
            container.Verify();
            Log.Information("Container verification finished.");

            return container;
        }
    }
}