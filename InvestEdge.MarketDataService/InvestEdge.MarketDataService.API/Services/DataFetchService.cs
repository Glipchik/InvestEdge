using InvestEdge.MarketDataService.API.Generators;
using InvestEdge.MarketDataService.API.Interfaces.ExternalServices;
using InvestEdge.MarketDataService.API.Interfaces.Services;
using InvestEdge.MarketDataService.API.Models;

namespace InvestEdge.MarketDataService.API.Services;

public class DataFetchService(IAssetExternalService assetExternalService) : IDataFetchService
{
    public async Task<List<FetchedAssetModel>> GetUpToDateDataOnAssets(CancellationToken cancellationToken)
    {
        var assets = await assetExternalService.GetAllAvailableAssets(cancellationToken);
        var upToDateAssets = new List<FetchedAssetModel>();

        foreach (var asset in assets)
        {
            var newPrice = asset.Type switch
            {
                "CRYPTO" => PriceGenerator.GeneratePriceForCrypto(asset.Price),
                "TRADITIONAL" => PriceGenerator.GeneratePriceForTraditional(asset.Price),
                _ => throw new NotImplementedException(),
            };
            upToDateAssets.Add(new()
            {
                Id = asset.Id,
                Price = newPrice,
            });
        }

        return upToDateAssets;
    }
}
