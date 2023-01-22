namespace WebStoreProject.Storage
{
    public class StorageOrderItem
    {
        public long Id { get; set; }
        public short OrderNum { get; set; }
        public StorageOrder Order { get; set; }
        public sbyte GoodsArticle { get; set; }
        public StorageGoods StorageGoods { get; set; }
        public byte Amount { get; set; }
    }
}
