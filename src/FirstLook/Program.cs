using FirstLook.Strategies.Invoice;

var order = new Order()
{
  SalesTaxStrategy = new SwedenSalesTaxStrategy(),
  InvoiceStrategy = new FileInvoiceStrategy(),
  Payments = [
    new Payment()
    {
      Amount = 100m,
      Provider = PaymentProvider.Invoice,
    },
  ],
  ShippingDetails = new ShippingDetails()
  {
    OriginCountry = "Sweden",
    DestinationCountry = "Sweden",
  },
  LineItems =
  [
    new LineItem()
    {
      Product = "Book",
      Price = 100m,
      Type = ItemType.Literature,
    },
    new LineItem()
    {
      Product = "Service",
      Price = 100m,
      Type = ItemType.Service,
    },
  ],
};

Console.WriteLine(order.GetTax());
Console.WriteLine(order.GetTax(new SwedenSalesTaxStrategy()));
await order.FinalizeAsync();