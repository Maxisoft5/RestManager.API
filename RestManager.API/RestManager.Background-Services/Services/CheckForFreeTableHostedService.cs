using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RestManager.Background_Services.Queues;
using RestManager.Services.Interfaces;
using System.Threading;

namespace RestManager.Background_Services.Services
{
    public class CheckForFreeTableHostedService : BackgroundService
    {
        private readonly ILogger<CheckForFreeTableHostedService> _logger;
        private readonly IServiceProvider _serviceProvider;
        public CheckForFreeTableQueue TaskQueue { get; }
        public CheckForFreeTableHostedService(CheckForFreeTableQueue taskQueue,
                            ILogger<CheckForFreeTableHostedService> logger,
                            IServiceProvider serviceProvider)
        {
            TaskQueue = taskQueue;
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var workItem = await TaskQueue.DequeueAsync(cancellationToken);
                if (workItem != null)
                {
                    using var scope = _serviceProvider.CreateScope();
                    try
                    {
                        var restManager = scope.ServiceProvider.GetRequiredService<IRestManager>();
                    }
                    catch (Exception ex)
                    {
                        
                    }
                   
                }
                
            }
        }
    }
}
