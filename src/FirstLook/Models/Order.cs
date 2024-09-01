using FirstLook.Strategies.Invoice;

namespace FirstLook.Models;

class Order
{
  public string Id { get; init; } = Guid.NewGuid().ToString();
  public ShippingDetails ShippingDetails { get; init; } = new ShippingDetails();
  public List<LineItem> LineItems { get; init; } = [];
  public decimal Total => LineItems.Sum(x => x.Price);
  public ISalesTaxStrategy? SalesTaxStrategy { get; init; } = null;
  public IInvoiceStrategy? InvoiceStrategy { get; init; } = null;
  public List<Payment> Payments { get; init; } = [];
  public ShippingStatus ShippingStatus { get; set; } = ShippingStatus.WaitingForPayment;
  public decimal AmountDue => Total - Payments.Sum(x => x.Amount);

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

  public decimal GetTax(ISalesTaxStrategy? salesTaxStrategy = null)
  {
    var strategy = salesTaxStrategy ?? SalesTaxStrategy;
    
    if (strategy is null)
    {
      throw new InvalidOperationException("Unable to calculate tax. Sales tax strategy is null.");
    }

    return strategy.GetTaxForOrder(this);
  }

  private bool RequiresInvoice => 
    AmountDue > 0 && 
    ShippingStatus is ShippingStatus.WaitingForPayment &&
    Payments.Any(x => x.Provider is PaymentProvider.Invoice);

  public async Task FinalizeAsync()
  {
    if (RequiresInvoice)
    {
      if (InvoiceStrategy is null)
      {
        throw new InvalidOperationException("Unable to finalize order. Invoice strategy is null.");
      }

      await InvoiceStrategy.GenerateInvoiceAsync(this);
      ShippingStatus = ShippingStatus.ReadyForShipment;
      return;
    }

    if (AmountDue > 0)
    {
      throw new InvalidOperationException("Unable to finalize order. Payment is still due.");
    }

    ShippingStatus = ShippingStatus.ReadyForShipment;
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
  public string Id { get; init; } = Guid.NewGuid().ToString();
  public string Product { get; init; } = string.Empty;
  public decimal Price { get; init; }  = 0;
  public ItemType Type { get; init; }
  public int Quantity { get; init; } = 1;
}

class Payment
{
  public PaymentProvider Provider { get; init; }
  public decimal Amount { get; init; }
}

enum PaymentProvider
{
  PayPal,
  CreditCard,
  Invoice,
}

enum ShippingStatus
{
  WaitingForPayment,
  ReadyForShipment,
  Shipped,
  Delivered,
}

enum ItemType
{
  Literature,
  Service,
  Food,
  Hardware,
}