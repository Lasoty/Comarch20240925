namespace ComarchTestExplorer.Services.Interfaces;

public interface IDiscountService
{
    decimal CalculateDiscount(decimal totalAmount, string customerType);
}
