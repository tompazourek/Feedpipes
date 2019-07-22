using System.Threading;
using System.Threading.Tasks;
using Serilog;

namespace Feedpipes.Runner
{
    internal class Runner
    {
        public Runner(ILogger log)
        {
            Log = log;
        }

        private ILogger Log { get; }

        public async Task Run(CancellationToken token)
        {
            Log.Information("Hello world!");
        }
    }
}