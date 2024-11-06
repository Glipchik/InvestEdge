using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using GraphQL;
using GraphQL.Query.Builder;
using InvestEdge.MarketDataService.API.DTOs;
using GraphQL.Client.Abstractions;
using InvestEdge.MarketDataService.API.Interfaces.ExternalServices;

namespace InvestEdge.MarketDataService.API.ExternalServices;

public class AssetExternalService : IAssetExternalService
{
    private readonly GraphQLHttpClient _client;

    public AssetExternalService()
    {
        // Initialize the client with the endpoint URL and the serializer
        _client = new GraphQLHttpClient("https://localhost:7105/api", new NewtonsoftJsonSerializer());
    }

    // Method to send a GraphQL query request
    public async Task<List<AssetDto>> GetAllAvailableAssets(CancellationToken cancellationToken)
    {
        // Define the structure of the query
        //var query = new Query<AssetsResponse>("assets")
        //        .AddField<AssetDto>(a => a.Assets, assetQuery => assetQuery
        //            .AddField(a => a.Id)
        //            .AddField(a => a.Type)
        //        );

        var query = new GraphQLRequest
        {
            Query = @"
                {
                  assets {
                    id
                    type
                    price
                  }
                }"
        };

        // Build and execute the query
        var response = await _client.SendQueryAsync<AssetsResponse>(query, cancellationToken: cancellationToken);
        return response.Data?.Assets ?? [];
    }
}
