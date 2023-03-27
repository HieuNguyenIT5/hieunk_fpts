
using WebApplication1.Models;

namespace EFCore.Logic
{
    public class BussinessOrder
    {
        public decimal VAT(decimal total, int vat)
        {
            var totalOutVAT = total / vat;
            return totalOutVAT;
        }
        public decimal totalOrder(List<OrderItem> items)
        {
            decimal total = 0;
            foreach (var item in items)
            {
                total += (item.UnitPrice * item.Units);
            }
            return total;
        }
    }
}
