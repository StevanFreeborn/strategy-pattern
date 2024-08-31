namespace FirstLook.Models;

class Order
{
  public ShippingDetails ShippingDetails { get; init; } = new ShippingDetails();
  public List<LineItem> LineItems { get; init; } = [];
  public decimal Total => LineItems.Sum(x => x.price);
  private bool IsGoingToSweden => string.Equals(
    ShippingDetails.DestinationCountry, 
    "Sweden", 
    StringComparison.InvariantCultureIgnoreCase
  );

  private bool IsDomestic => string.Equals(
    ShippingDetails.OriginCountry, 
    ShippingDetails.DestinationCountry, 
    StringComparison.InvariantCultureIgnoreCase
  );

  private bool IsGoingToUS => string.Equals(
    ShippingDetails.DestinationCountry, 
    "us", 
    StringComparison.InvariantCultureIgnoreCase
  );

  public decimal GetTax()
  {
    if (IsGoingToSweden && IsDomestic)
    {
      return Total * 0.25m;
    }

    if (IsGoingToUS)
    {
      return ShippingDetails.DestinationState switch
      {
        "la" => Total * 0.095m,
        "ny" => Total * 0.04m,
        "nyc" => Total * 0.045m,
        _ => 0m,
      };
    }

    return 0m;
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