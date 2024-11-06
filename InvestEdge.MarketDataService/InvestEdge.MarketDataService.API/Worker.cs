using Grpc.Net.Client;
using InvestEdge.MarketDataService.API.Interfaces.Services;
using PortfolioClient;
using static PortfolioClient.PortfolioExternal;

namespace InvestEdge.MarketDataService.API;

public class Worker(ILogger<Worker> logger, IServiceProvider serviceProvider) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (logger.IsEnabled(LogLevel.Information))
            {
                logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            }
            await Task.Delay(1000, stoppingToken);

            using var scope = serviceProvider.CreateScope();
            var dataFetchService = scope.ServiceProvider.GetService<IDataFetchService>()!;
            var upToDateAssetsPrices = await dataFetchService.GetUpToDateDataOnAssets(stoppingToken);
            scope.Dispose();

            using var channel = GrpcChannel.ForAddress("https://localhost:7105");
            var client = new PortfolioExternalClient(channel);
            var updateModel = new UpdateAssetsPricesRequestModel();
            foreach (var asset in upToDateAssetsPrices)
            {
                updateModel.Assets.Add(new FetchedAssetModel() 
                {
                    Id = asset.Id.ToString(),
                    Price = asset.Price.ToString(),
                });
            }
            await client.UpdateAssetsPricesAsync(updateModel, cancellationToken: stoppingToken);
        }
    }
}
