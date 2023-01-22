using WebStoreProject.Model;

namespace WebStoreProject.Storage
{
    public class StorageOrder
    {
        public short Number { get; set; }
        public string ClientName { get; set; }
        public DateTime CreateDate { get; set; }
        public OrderStatus Status { get; set; }
        public ICollection<StorageOrderItem> Goods { get; set; }
    }
}
