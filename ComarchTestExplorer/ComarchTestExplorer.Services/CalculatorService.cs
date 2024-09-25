using ComarchTestExplorer.Services.Interfaces;

namespace ComarchTestExplorer.Services;

public class CalculatorService : ICalculatorService
{
    public decimal GetGrossFromNet(decimal netValue, decimal tax)
    {
        if (tax < 0)
            throw new ArgumentOutOfRangeException(nameof(tax), "Tax cannot be negative.");

        return netValue + (netValue * tax);
    }
}
