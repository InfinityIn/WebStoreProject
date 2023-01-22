using MediatR;
using WebStoreProject.Model;
using WebStoreProject.Storage;


namespace WebStoreProject.Queries.OrdersQueries
{
    public class GetOrdersForDateQueryHandler
        : IRequestHandler<GetOrdersForDateQuery.Request, GetOrdersForDateQuery.Response>

    {
        public GetOrdersForDateQueryHandler(StorageContext storageContext) => _storageContext = storageContext;

        private readonly StorageContext _storageContext;
        public async Task<GetOrdersForDateQuery.Response> Handle(GetOrdersForDateQuery.Request request, CancellationToken cancellationToken)
        {
            if (!request.Date.HasValue)
                throw new Exception("Некорректное значение даты");
            var response = new GetOrdersForDateQuery.Response()
            {
                OrdersList = _storageContext.Orders
                .Where(x => x.CreateDate == request.Date.Value)
                .Select(x => Mapper.MapOrder(x)).ToList()
            };

            if (response.OrdersList == null)
                throw new Exception($"Ошибка при получении списка товаров на дату {request.Date.ToString()}!");

            return response;
        }
    }
}
