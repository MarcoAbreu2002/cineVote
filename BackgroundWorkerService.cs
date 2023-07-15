using cineVote.Models.Domain;
using cineVote.Repositories.Abstract;


namespace cineVote
{
    public class BackgroundWorkerService : BackgroundService
    {
        private int executionCount = 0;

        private readonly ILogger<BackgroundWorkerService> _logger;
        private readonly IServiceProvider _serviceProvider;

        private readonly IHttpContextAccessor _httpContextAccessor;

        private Timer? _timer;

        public BackgroundWorkerService(ILogger<BackgroundWorkerService> logger, IServiceProvider serviceProvider, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _httpContextAccessor = httpContextAccessor;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Timed Hosted Service running.");

            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));

            await Task.CompletedTask;
        }

        private async void DoWork(object? state)
        {
            using (IServiceScope scope = _serviceProvider.CreateScope())
            {

                ICompetitionManager CompetitionController = scope.ServiceProvider.GetRequiredService<ICompetitionManager>();

                AppDbContext context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                HttpContext httpContext = _httpContextAccessor.HttpContext;

                if (httpContext == null)
                {
                    // Log an error or handle the case where httpContext is null.
                    _logger.LogError("HttpContext is null.");
                    return;
                }

                // Check if the user is authenticated
                if (httpContext.User.Identity.IsAuthenticated)
                {
                    string userName = httpContext.User.Identity.Name;

                    if (context == null)
                    {
                        // Log an error or handle the case where context is null.
                        _logger.LogError("AppDbContext is null.");
                        return;
                    }

                    var notifications = context.Notifications.Where(n => n.userName == userName).ToList();

                    httpContext.Items["notifications"] = notifications;
                    // ... Rest of your code ...
                }

                if (context == null)
                {
                    // Log an error or handle the case where context is null.
                    _logger.LogError("AppDbContext is null.");
                    return;
                }

                List<Competition> competitions = context.Competitions.ToList();

                DateTime localTime = DateTime.Now;

                foreach (var competition in competitions)
                {
                    if (competition.StartDate < localTime)
                    {
                        if (competition.EndDate > localTime && competition.IsPublic == false)
                        {
                            // Notify the observer
                            CompetitionController.startCompetition(competition);
                            NotifyObserver(competition);
                        }
                        else if (competition.EndDate < localTime && competition.IsPublic == true)
                        {
                            CompetitionController.finishCompetition(competition);
                            NotifyObserver(competition);
                        }
                    }
                }
            }
        }

        private void NotifyObserver(Competition competition)
        {
            _logger.LogInformation("Found this competition: {CompetitionDate}", competition.IsPublic);
        }
    }
}
