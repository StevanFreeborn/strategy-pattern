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

    var totalTax = 0m;

    foreach (var item in order.LineItems)
    {
      var taxRate = item.Type switch
      {
        ItemType.Food => 0.06m,
        ItemType.Literature => 0.08m,
        ItemType.Service or ItemType.Hardware => 0.25m,
        _ => throw new InvalidOperationException("Invalid item type."),
      };

      totalTax += item.Price * taxRate * item.Price;
    }

    return totalTax;
  }
}