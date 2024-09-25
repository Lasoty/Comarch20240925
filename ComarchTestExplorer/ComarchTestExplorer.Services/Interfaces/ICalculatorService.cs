namespace ComarchTestExplorer.Services.Interfaces;

public interface ICalculatorService
{
    /// <summary>
    /// Returns gross value from net value and tax.
    /// </summary>
    /// <param name="netValue"></param>
    /// <param name="tax"></param>
    /// <returns></returns>
    decimal GetGrossFromNet(decimal netValue, decimal tax);
}
