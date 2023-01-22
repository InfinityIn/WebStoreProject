namespace WebStoreProject.Model
{
    public enum OrderStatus
    {
        Registered,
        Formed,
        Completed,
        Cancelled
    }
    public class Order
    {
        public short Number { get; set; }
        public string ClientName { get; set; }
        public OrderStatus Status { get; set; }
        public List<OrderItem> Goods { get; set; }
    }

    public static class OrderExtensions
    {
        public static decimal OrderTotalPrice(Order order) => order.Goods.Sum(x => x.Goods.Price);
        public static int NumberOfGoodsUnits(Order order) => order.Goods.Sum(x => x.Amount);
    }
}
