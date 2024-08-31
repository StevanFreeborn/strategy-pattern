var order = new Order()
{
  SalesTaxStrategy = new SwedenSalesTaxStrategy(),
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
      price = 100m,
      Type = ItemType.Literature,
    },
    new LineItem()
    {
      Product = "Service",
      price = 100m,
      Type = ItemType.Service,
    },
  ],
};

Console.WriteLine(order.GetTax());