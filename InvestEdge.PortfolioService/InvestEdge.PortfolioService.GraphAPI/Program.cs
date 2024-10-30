using HotChocolate.AspNetCore.Playground;
using HotChocolate.AspNetCore;
using InvestEdge.PortfolioService.GraphAPI.Contexts;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using InvestEdge.PortfolioService.GraphAPI.Enums;
using InvestEdge.PortfolioService.GraphAPI.GraphQL;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddGraphQLServer()
    .AddType<InvestEdge.PortfolioService.GraphAPI.GraphQL.AssetType>()
    .AddType<HoldingType>()
    .AddType<UserType>()
    .AddQueryType<InvestEdge.PortfolioService.GraphAPI.GraphQL.Query>();

builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

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

app.MapGet("/", () => "Hello World!");

app.Run();
