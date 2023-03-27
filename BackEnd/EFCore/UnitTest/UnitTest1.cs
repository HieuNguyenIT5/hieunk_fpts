
using NuGet.Frameworks;
using WebApplication1.Models;
using EFCore.Logic;

namespace UnitTest;
public class UnitTest1
{
    private static BussinessOrder _bussinessOrder = new BussinessOrder();
    public UnitTest1()
    {
    }
    [Fact]
    public void totalOrderTest()
    {
       List<OrderItem>  items = new List<OrderItem>();
        items.Add(new OrderItem
        {
            OrderId = 1,
            ProductId = 1,
            Units = 3,
            UnitPrice = 10,
        });
        items.Add(new OrderItem
        {
            OrderId = 1,
            ProductId = 4,
            Units = 2,
            UnitPrice = 100,
        });

        var result = _bussinessOrder.totalOrder(items);
        
        Assert.Equal(result, 230);
    }
    [Fact]
    public void VATTest()
    {
        // Arrange
        decimal total = 20000;
        int vat = 10;

        // Act
        var result = _bussinessOrder.VAT(total, vat);

        // Assert
        Assert.True(result == 2000);
    }
}