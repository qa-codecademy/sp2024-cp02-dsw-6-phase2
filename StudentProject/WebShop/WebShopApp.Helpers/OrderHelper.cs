using WebShopApp.Domain.Models;

public static class OrderHelper
{
    public static decimal CalculateTotalAmount(Orderr order)
    {
        if (order == null || order.OrderItems == null)
        {
            return 0;
        }

        return order.OrderItems.Sum(item => item.Quantity * item.Product.Price);
    }
}
