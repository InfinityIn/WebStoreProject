using MediatR;
using WebStoreProject.Model;
using WebStoreProject.Storage;


namespace WebStoreProject.Queries.OrdersQueries
{
    public class GetGoodsCommandHandler
        : IRequestHandler<GetOrdersQuery.Request, GetOrdersQuery.Response>

    {
        public GetGoodsCommandHandler(StorageContext storageContext) => _storageContext = storageContext;

        private readonly StorageContext _storageContext;
        public async Task<GetOrdersQuery.Response> Handle(GetOrdersQuery.Request request, CancellationToken cancellationToken)
        {
            var response = new GetOrdersQuery.Response()
            {
                OrdersList = _storageContext.Orders.Select(x => Mapper.MapOrder(x)).ToList()
            };

            if (response.OrdersList == null)
                throw new Exception("Ошибка при получении списка товаров!");

            return response;
        }
    }
}
