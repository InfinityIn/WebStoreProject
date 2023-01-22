using MediatR;
using WebStoreProject.Model;
using WebStoreProject.Storage;


namespace WebStoreProject.Commands.OrderCommands
{
    public class AddOrderCommandHandler
        : IRequestHandler<AddOrderCommand.Request, AddOrderCommand.Response>
    {
        public AddOrderCommandHandler(StorageContext storageContext) => _storageContext = storageContext;

        private readonly StorageContext _storageContext;
        public async Task<AddOrderCommand.Response> Handle(AddOrderCommand.Request request, CancellationToken cancellationToken)
        {
            if (request.Order == null)
                throw new Exception("Пустой объект заказа при попытке добавления");
            if (request.Order.Number != request.OrderNum)
                throw new Exception("Номера заказов не совпадают");

            if (OrderExtensions.NumberOfGoodsUnits(request.Order) > 10)
                throw new BusinessLogicException("В одном заказе можно указать не больше 10 единиц товаров");
            if (OrderExtensions.OrderTotalPrice(request.Order) > 15000)
                throw new BusinessLogicException("Сумма заказа не должна превышать 15000 у.е.");

            foreach (var item in request.Order.Goods)
                if (!_storageContext.Goods.Any(x => x.Article == item.Goods.Article))
                    throw new BusinessLogicException($"В заказе товар с артикулом {item.Goods.Article} не найден в базе");

            var newOrder = AddOrder(request.Order);

            AddOrderItems(request.Order.Goods, newOrder);             

            return new AddOrderCommand.Response() { };
        }

        private StorageOrder AddOrder(Order order)
        {
            var newOrder = new StorageOrder()
            {
                Number = order.Number,
                ClientName = order.ClientName,
                Status = order.Status
            };

            _storageContext.Orders.Add(newOrder);

            return newOrder;
        }

        private void AddOrderItems(IEnumerable<OrderItem> items, StorageOrder order)
        {
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
