

using System.Net.Http.Json;

namespace FirstLook.Strategies.Invoice;

class PrintOnDemandInvoiceStrategy : IInvoiceStrategy
{
  public async Task GenerateInvoiceAsync(Order order)
  {
    using var client = new HttpClient();
    client.BaseAddress = new Uri("https://printondemand.example.com");
    await client.PostAsJsonAsync("print", order);
  }
}