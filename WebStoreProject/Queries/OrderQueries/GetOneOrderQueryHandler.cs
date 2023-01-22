using MediatR;
using WebStoreProject.Model;
using WebStoreProject.Storage;


namespace WebStoreProject.Queries.OrdersQueries
{
    public class GetOneGoodsCommandHandler
        : IRequestHandler<GetOneOrderQuery.Request, GetOneOrderQuery.Response>

    {
        public GetOneGoodsCommandHandler(StorageContext storageContext) => _storageContext = storageContext;

        private readonly StorageContext _storageContext;
        public async Task<GetOneOrderQuery.Response> Handle(GetOneOrderQuery.Request request, CancellationToken cancellationToken)
        {
            var response = new GetOneOrderQuery.Response()
            {
                Order = Mapper.MapOrder(_storageContext.Orders.SingleOrDefault(x => x.Number == request.OrderNum))
            };
            
            if (response.Order == null)
                throw new Exception("Такого заказа не существует!");

            return response;
        }
    }
}
