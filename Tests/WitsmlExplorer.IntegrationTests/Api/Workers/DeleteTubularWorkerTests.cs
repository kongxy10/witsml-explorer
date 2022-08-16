using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using Serilog;

using WitsmlExplorer.Api.Jobs;
using WitsmlExplorer.Api.Jobs.Common;
using WitsmlExplorer.Api.Services;
using WitsmlExplorer.Api.Workers;

using Xunit;

namespace WitsmlExplorer.IntegrationTests.Api.Workers
{
    public class DeleteTubularWorkerTests
    {
        private readonly DeleteTubularsWorker _worker;

        public DeleteTubularWorkerTests()
        {
            var configuration = ConfigurationReader.GetConfig();
            var witsmlClientProvider = new WitsmlClientProvider(configuration);
            var loggerFactory = (ILoggerFactory)new LoggerFactory();
            loggerFactory.AddSerilog(Log.Logger);
            var logger = loggerFactory.CreateLogger<DeleteTubularsJob>();
            var logger2 = loggerFactory.CreateLogger<DeleteUtils>();
            var deleteUtils = new DeleteUtils(logger2, witsmlClientProvider);
            _worker = new DeleteTubularsWorker(logger, witsmlClientProvider, deleteUtils);
        }

        [Fact(Skip = "Should only be run manually")]
        public async Task DeleteTubular()
        {
            var job = new DeleteTubularsJob
            {
                ToDelete = new TubularReferences
                {
                    WellUid = "8c77de13-4fad-4b2e-ba3d-7e6b0e35a394",
                    WellboreUid = "44e7a064-c2f2-4a3a-9259-5ab92085e110",
                    TubularUids = new string[] { "2YA2M49" }
                }
            };
            await _worker.Execute(job);
        }
    }
}
