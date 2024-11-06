namespace InvestEdge.MarketDataService.API.Generators;

public static class PriceGenerator
{
    private static readonly Random _random = new Random();

    public static decimal GeneratePriceForCrypto(decimal value)
    {
        // Calculate 5% of the input value
        decimal range = value * 0.05m;

        // Determine the minimum and maximum bounds
        decimal minValue = value - range;
        decimal maxValue = value + range;

        // Generate a random decimal value within the range
        decimal randomValue = (decimal)_random.NextDouble() * (maxValue - minValue) + minValue;

        return randomValue;
    }

    public static decimal GeneratePriceForTraditional(decimal value)
    {
        // Calculate 1% of the input value
        decimal range = value * 0.01m;

        // Determine the minimum and maximum bounds
        decimal minValue = value - range;
        decimal maxValue = value + range;

        // Generate a random decimal value within the range
        decimal randomValue = (decimal)_random.NextDouble() * (maxValue - minValue) + minValue;

        return randomValue;
    }
}
