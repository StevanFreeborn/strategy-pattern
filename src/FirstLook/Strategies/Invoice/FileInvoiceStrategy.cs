namespace FirstLook.Strategies.Invoice;

class FileInvoiceStrategy : InvoiceStrategy
{
  public override async Task GenerateInvoiceAsync(Order order)
  {
    var body = GenerateInvoiceText(order);

    await File.WriteAllTextAsync($"invoice_{order.Id}.txt", body);
  }
}