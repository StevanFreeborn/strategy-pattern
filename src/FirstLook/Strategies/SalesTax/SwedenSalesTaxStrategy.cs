namespace FirstLook.Strategies.SalesTax;

class SwedenSalesTaxStrategy : ISalesTaxStrategy
{
  public decimal GetTaxForOrder(Order order)
  {
    if (order.IsGoingTo("Sweden") is false)
    {
      throw new InvalidOperationException(
        "Invalid order destination. This strategy is only applicable to Sweden."
      );
    }

    return order.IsDomestic ? order.Total * 0.25m : 0m;
  }
}