
using System.Net;
using System.Net.Mail;

namespace FirstLook.Strategies.Invoice;

class EmailInvoiceStrategy : InvoiceStrategy
{
  public override async Task GenerateInvoiceAsync(Order order)
  {
    var body = GenerateInvoiceText(order);

    using var client = new SmtpClient("smtp.example.com", 587)
    {
      Credentials = new NetworkCredential("username", "password"),
      EnableSsl = true,
    };

    var message = new MailMessage("test@test.com", "test@test.com", "Invoice", body);

    await client.SendMailAsync(message);
  }
}