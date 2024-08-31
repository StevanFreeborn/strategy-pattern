using FirstLook.Models;

var order = new Order()
{
  ShippingDetails = new ShippingDetails()
  {
    OriginCountry = "Sweden",
    DestinationCountry = "Sweden",
  },
  LineItems = new List<LineItem>()
  {
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
  },
};

Console.WriteLine(order.GetTax());