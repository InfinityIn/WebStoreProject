using MediatR;
using WebStoreProject.Model;
using WebStoreProject.Storage;


namespace WebStoreProject.Commands.OrderCommands
{
    public class PutOrderCommandHandler
        : IRequestHandler<PutOrderCommand.Request, PutOrderCommand.Response>
    {
        public PutOrderCommandHandler(StorageContext storageContext) => _storageContext = storageContext;

        private readonly StorageContext _storageContext;
        public async Task<PutOrderCommand.Response> Handle(PutOrderCommand.Request request, CancellationToken cancellationToken)
        {
            if (request.Order == null)
                throw new Exception("Пустой объект заказа при попытке обновления");
            if (request.Order.Number != request.OrderNum)
                throw new Exception("Номера заказов не совпадают");

            if (request.Order != null && request.Order.Status != OrderStatus.Registered)
                throw new BusinessLogicException("Изменение заказа не в статусе Зарегистрирован запрещено");

            if (OrderExtensions.NumberOfGoodsUnits(request.Order) > 10)
                throw new BusinessLogicException("В одном заказе можно указать не больше 10 единиц товаров");
            if (OrderExtensions.OrderTotalPrice(request.Order) > 15000)
                throw new BusinessLogicException("Сумма заказа не должна превышать 15000 у.е.");

            foreach (var item in request.Order.Goods)
                if (!_storageContext.Goods.Any(x => x.Article == item.Goods.Article))
                    throw new BusinessLogicException($"В заказе товар с артикулом {item.Goods.Article} не найден в базе");

            var newOrder = UpdateOrder(request.Order);

            UpdateOrderItems(request.Order.Goods, newOrder);             

            return new PutOrderCommand.Response() { };
        }

        private StorageOrder UpdateOrder(Order order)
        {            
            var oldOrder = _storageContext.Orders
                .SingleOrDefault(x => x.Number == order.Number);

            if (oldOrder == null)
                throw new Exception("В базе не найден заказ с указанным номером");

            _storageContext.Orders.Remove(oldOrder);

            var newOrder = new StorageOrder()
            {
                Number = order.Number,
                ClientName = order.ClientName,
                Status = order.Status
            };

            _storageContext.Orders.Add(newOrder);

            return newOrder;
        }

        private void UpdateOrderItems(IEnumerable<OrderItem> items, StorageOrder order)
        {            
            _storageContext.OrderItems.RemoveAll(x => x.OrderNum == order.Number);
            foreach(var item in items)
            {
                _storageContext.OrderItems.Add(new StorageOrderItem()
                {
                    Id = _storageContext.OrderItems.OrderBy(x => x.Id).Last().Id + 1,
                    GoodsArticle = item.Goods.Article,
                    StorageGoods = Mapper.MapStorageGoods(item.Goods),
                    OrderNum = order.Number,
                    Order = order
                }) ;
            }

            order.Goods = _storageContext.OrderItems
                .Where(x => x.OrderNum == order.Number)
                .ToList();
        }
    }
}
