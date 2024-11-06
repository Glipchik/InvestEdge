namespace InvestEdge.MarketDataService.API.DTOs;

public class AssetDto
{
    public Guid Id { get; set; }
    public string Type { get; set; } = string.Empty;
    public decimal Price { get; set; }
}
