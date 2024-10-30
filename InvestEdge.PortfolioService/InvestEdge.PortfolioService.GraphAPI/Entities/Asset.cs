using InvestEdge.PortfolioService.GraphAPI.Enums;

namespace InvestEdge.PortfolioService.GraphAPI.Entities;

public class Asset
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public AssetType Type { get; set; }
    public decimal Price { get; set; }
}
