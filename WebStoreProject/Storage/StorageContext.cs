using System.Linq;
using WebStoreProject.Model;

namespace WebStoreProject.Storage
{
    public class StorageContext
    {
        public StorageContext()
        {
            Random random = new Random();
            this.Goods = new List<StorageGoods>();
            for (sbyte i = 1; i <= 100; i++)
            {
                this.Goods.Add(
                    new StorageGoods
                    {
                        Article = (sbyte)(i + i),
                        Name = $"GoodN{i}",
                        Price = (i + i ^ i) * 100
                    }); ;
            }
            this.Orders = new List<StorageOrder>();
            this.OrderItems = new List<StorageOrderItem>();
            for (sbyte i = 1; i <= 10; i++)
            {
                var indexOrderItem = 1;
                var order = new StorageOrder
                {
                    Number = i,
                    ClientName = $"Ivanov Ivan Ivanovich{i}",
                    CreateDate = DateTime.Now,
                    Status = (OrderStatus)random.Next(1, 4),
                };
                var goods = this.Goods
                    .Skip(random.Next(1, 50))
                    .Take(random.Next(1, 5))
                    .Select(x => new StorageOrderItem
                    {
                        Id = indexOrderItem++,
                        Order = order,
                        OrderNum = order.Number,
                        GoodsArticle = x.Article,
                        StorageGoods = x,
                        Amount = (byte)random.Next(1, 3),
                    })
                    .ToList();

                foreach(var item in goods)
                    this.OrderItems.Add(item);                
                order.Goods = goods;
                this.Orders.Add(order);
            }                            
        }
                                             
        public List<StorageGoods> Goods { get; set; }
        public List<StorageOrder> Orders { get; set; }
        public List<StorageOrderItem> OrderItems { get; set; }
    }    
}
