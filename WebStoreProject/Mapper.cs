using WebStoreProject.Model;
using WebStoreProject.Storage;

namespace WebStoreProject
{
    public static class Mapper
    {

        public static Goods MapGoods(StorageGoods item) =>
                 item != null
                 ? new Goods
                 {
                     Article = item.Article,
                     Name = item.Name,
                     Price = item.Price
                 }
                 : null;

        public static Order MapOrder(StorageOrder item) =>
                item != null
                ? new Order
                {
                    Number = item.Number,
                    ClientName = item.ClientName,
                    Status = item.Status,
                    Goods = MapGoodsInOrder(item.Goods.ToList())
                } 
                : null;

        public static StorageGoods MapStorageGoods(Goods item) =>
                 item != null
                 ? new StorageGoods
                 {
                     Article = item.Article,
                     Name = item.Name,
                     Price = item.Price
                 }
                 : null;       

        public static List<OrderItem> MapGoodsInOrder(List<StorageOrderItem> storageOrderItems) => 
            storageOrderItems.Select(x => new OrderItem { Goods = MapGoods(x.StorageGoods), Amount = x.Amount }).ToList();        
    }
}
