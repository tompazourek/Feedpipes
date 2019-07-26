using System.Threading;
using System.Threading.Tasks;
using Serilog;

#pragma warning disable 1998
namespace Feedpipes.Runner
{
    internal class Runner
    {
        public Runner(ILogger log)
        {
            Log = log;
        }

        private ILogger Log { get; }

        public async Task Run(CancellationToken token) => Log.Information("Hello world!");
    }
}