using MediatR;
using WebStoreProject.Model;
using WebStoreProject.Storage;


namespace WebStoreProject.Queries.GoodsQueries
{
    public class GetGoodsCommandHandler
        : IRequestHandler<GetOrderssQuery.Request, GetOrderssQuery.Response>

    {
        public GetGoodsCommandHandler(StorageContext storageContext) => _storageContext = storageContext;

        private readonly StorageContext _storageContext;
        public async Task<GetOrderssQuery.Response> Handle(GetOrderssQuery.Request request, CancellationToken cancellationToken)
        {
            var responce = new GetOrderssQuery.Response()
            {
                GoodsList = _storageContext.Goods
                    .Select(x => Mapper.MapGoods(x)).ToList()
            };

            if (responce.GoodsList == null)
                throw new Exception("Ошибка при получении списка товаров!");

            return responce;
        }
    }
}
