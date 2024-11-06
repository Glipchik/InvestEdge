using InvestEdge.MarketDataService.API.Models;

namespace InvestEdge.MarketDataService.API.Interfaces.Services;

public interface IDataFetchService
{
    Task<List<FetchedAssetModel>> GetUpToDateDataOnAssets(CancellationToken cancellationToken);
}
