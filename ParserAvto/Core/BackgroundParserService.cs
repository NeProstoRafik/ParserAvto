namespace ParserAvto.Core
{
    public class BackgroundParserService : BackgroundService
    {
        private readonly IParserSettings settings;
        private readonly IServiceProvider _provider;
        private readonly PageLoader pageLoader;
        private readonly IParser avtoParser;
        private readonly ILogger<BackgroundParserService> logger;

        public BackgroundParserService(IParserSettings settings, IServiceProvider provider, ILogger<BackgroundParserService> logger)
        {
            this.settings = settings;

            _provider = provider;
            this.logger = logger;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {

                using (IServiceScope scope = _provider.CreateScope())
                {
                    var scopedProvider = scope.ServiceProvider;

                    var page = scope.ServiceProvider.GetRequiredService<PageLoader>();
                    var par = scope.ServiceProvider.GetRequiredService<IParser>();
                    var doc = await page.GetDocument(settings);

                    par.Parse(doc);

                    logger.LogInformation("прошел круггг!!!!!!");

                    await Task.Delay(TimeSpan.FromSeconds(1000000), stoppingToken);


                }
            }

        }
    }
}
