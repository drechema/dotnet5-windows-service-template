using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace WorkerService
{
    public class Worker : BackgroundService
    {
        private readonly WorkerSettings settings;

        public Worker(WorkerSettings settings) {
            this.settings = settings;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            Log.Information("-------- Starting Service ----------");
            return base.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            Log.Information("-------- Closing Service ----------");
            return base.StopAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                Log.Information("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
