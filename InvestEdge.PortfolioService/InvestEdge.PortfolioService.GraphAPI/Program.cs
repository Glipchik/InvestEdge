using HotChocolate.AspNetCore;
using HotChocolate.AspNetCore.Playground;
using InvestEdge.PortfolioService.GraphAPI.Contexts;
using InvestEdge.PortfolioService.GraphAPI.GraphQL;
using InvestEdge.PortfolioService.GraphAPI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddGraphQLServer()
    .AddType<AssetType>()
    .AddType<HoldingType>()
    .AddType<UserType>()
    .AddQueryType<Query>();

builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddGrpc();

var app = builder.Build();

var env = builder.Environment;

if (env.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UsePlayground(new PlaygroundOptions
    {
        QueryPath = "/api",
        Path = "/playground"
    });
}

app.MapGraphQL("/api");
app.MapGrpcService<PortfolioGrpcService>();

app.MapGet("/", () => "Hello World!");

app.Run();
