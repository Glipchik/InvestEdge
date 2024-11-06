using InvestEdge.MarketDataService.API;
using InvestEdge.MarketDataService.API.ExternalServices;
using InvestEdge.MarketDataService.API.Interfaces.ExternalServices;
using InvestEdge.MarketDataService.API.Interfaces.Services;
using InvestEdge.MarketDataService.API.Services;

var builder = Host.CreateApplicationBuilder(args);
//builder.Services.AddGrpc();
builder.Services.AddHostedService<Worker>();
builder.Services.AddScoped<IAssetExternalService, AssetExternalService>();
builder.Services.AddScoped<IDataFetchService, DataFetchService>();

var host = builder.Build();
host.Run();
