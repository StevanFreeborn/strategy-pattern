namespace FirstLook.Strategies.Invoice;

interface IInvoiceStrategy
{
  Task GenerateInvoiceAsync(Order order);
}