using System.Text;

namespace FirstLook.Strategies.Invoice;

abstract class InvoiceStrategy : IInvoiceStrategy
{
  public abstract Task GenerateInvoiceAsync(Order order);

  protected string GenerateInvoiceText(Order order)
  {
    var invoice = new StringBuilder();

    invoice.AppendLine($"INVOICE DATE: {DateTime.Now}");
    invoice.AppendLine($"ID|NAME|PRICE|QUANTITY");
    
    foreach (var item in order.LineItems)
    {
      invoice.AppendLine($"{item.Id}|{item.Product}|{item.Price}|{item.Quantity}");
    }

    invoice.AppendLine();
    invoice.AppendLine();

    var tax = order.GetTax();
    var totalWithTax = order.Total + tax;

    invoice.AppendLine($"TAX:         {tax}");
    invoice.AppendLine($"TOTAL:       {order.Total}");
    invoice.AppendLine($"TOTAL w/Tax: {totalWithTax}");

    return invoice.ToString();
  }
}