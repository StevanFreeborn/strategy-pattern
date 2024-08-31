using FirstLook.Strategies.SalesTax;

namespace FirstLook.Models;

class Order
{
  public ShippingDetails ShippingDetails { get; init; } = new ShippingDetails();
  public List<LineItem> LineItems { get; init; } = [];
  public decimal Total => LineItems.Sum(x => x.price);
  public ISalesTaxStrategy? SalesTaxStrategy { get; init; } = null;

  public bool IsDomestic => string.Equals(
    ShippingDetails.OriginCountry, 
    ShippingDetails.DestinationCountry, 
    StringComparison.InvariantCultureIgnoreCase
  );

  public bool IsGoingTo(string dest) => string.Equals(
    ShippingDetails.DestinationCountry, 
    dest, 
    StringComparison.InvariantCultureIgnoreCase
  );

  public decimal GetTax()
  {
    if (SalesTaxStrategy is null)
    {
      throw new InvalidOperationException("Unable to calculate tax. Sales tax strategy is null.");
    }

    return SalesTaxStrategy.GetTaxForOrder(this);
  }
}

class ShippingDetails
{
  public string OriginCountry { get; init; } = string.Empty;
  public string DestinationCountry { get; init; } = string.Empty;
  public string DestinationState { get; init; } = string.Empty;
}

class LineItem
{
  public string Product { get; init; } = string.Empty;
  public decimal price { get; init; }  = 0;
  public ItemType Type { get; init; }
}

enum ItemType
{
  Literature,
  Service,
}