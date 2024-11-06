using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using InvestEdge.PortfolioService.GraphAPI.Contexts;
using Microsoft.EntityFrameworkCore;
using PortfolioClient;

namespace InvestEdge.PortfolioService.GraphAPI.Services;

public class PortfolioGrpcService(ILogger<PortfolioGrpcService> logger, DatabaseContext dbContext) : PortfolioClient.PortfolioExternal.PortfolioExternalBase
{
    public override async Task<Empty> UpdateAssetsPrices(UpdateAssetsPricesRequestModel request, ServerCallContext context)
    {
        foreach (var asset in request.Assets)
        {
            logger.LogInformation("Updating asset with Id {id}", asset.Id);
            var assetToUpdate = await dbContext.Assets.FirstOrDefaultAsync(a => a.Id == Guid.Parse(asset.Id), cancellationToken: context.CancellationToken);
            if (assetToUpdate is null) continue;
            assetToUpdate.Price = decimal.Parse(asset.Price);
            dbContext.Update(assetToUpdate);
            logger.LogInformation("Updated asset with Id {id}", asset.Id);
        }

        return new Empty();
    }
}
