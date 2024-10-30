using InvestEdge.PortfolioService.GraphAPI.Contexts;
using InvestEdge.PortfolioService.GraphAPI.Entities;

namespace InvestEdge.PortfolioService.GraphAPI.GraphQL;

public class Query
{
    private readonly DatabaseContext dbContext;

    public Query(DatabaseContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public IQueryable<Asset> GetAssets([Service] DatabaseContext dbContext) => dbContext.Assets;
    public IQueryable<Holding> GetHoldings([Service] DatabaseContext dbContext) => dbContext.Holdings;
    public IQueryable<User> GetUsers([Service] DatabaseContext dbContext) => dbContext.Users;
}