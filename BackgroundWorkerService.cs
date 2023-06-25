using cineVote.Models.Domain;

namespace cineVote
{
    public class BackgroundWorkerService : IHostedService
    {
        readonly ILogger<BackgroundWorkerService> _logger;
        private readonly AppDbContext? _context;

        public BackgroundWorkerService(AppDbContext context,ILogger<BackgroundWorkerService> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Service started");
            while (!cancellationToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at:{time}", DateTimeOffset.Now);
                await Task.Delay(1000, cancellationToken);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Service stopped");
            return Task.CompletedTask;
        }
    }
}