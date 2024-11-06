using InvestEdge.MarketDataService.API.DTOs;

namespace InvestEdge.MarketDataService.API.Interfaces.ExternalServices;

public interface IAssetExternalService
{
    Task<List<AssetDto>> GetAllAvailableAssets(CancellationToken cancellationToken);
}
