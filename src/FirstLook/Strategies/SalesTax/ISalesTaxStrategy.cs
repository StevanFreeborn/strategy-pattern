namespace FirstLook.Strategies.SalesTax;

interface ISalesTaxStrategy
{
  decimal GetTaxForOrder(Order order);
}