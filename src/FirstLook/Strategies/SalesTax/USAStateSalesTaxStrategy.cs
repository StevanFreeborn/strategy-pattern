namespace FirstLook.Strategies.SalesTax;

class USAStateSalesTaxStrategy : ISalesTaxStrategy
{
  public decimal GetTaxForOrder(Order order)
  {
    if (order.IsGoingTo("USA") is false)
    {
      throw new InvalidOperationException(
        "Invalid order destination. This strategy is only applicable to USA."
      );
    }

    return order.ShippingDetails.DestinationState switch
    {
      "la" => order.Total * 0.095m,
      "ny" => order.Total * 0.04m,
      "nyc" => order.Total * 0.045m,
      _ => 0m,
    };
  }
}