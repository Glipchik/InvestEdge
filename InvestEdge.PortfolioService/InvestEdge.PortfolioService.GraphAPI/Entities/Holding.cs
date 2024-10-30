namespace InvestEdge.PortfolioService.GraphAPI.Entities;

public class Holding
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid AssetId { get; set; }
    public decimal Amount { get; set; }
}
