using MediatR;
using WebStoreProject.Model;
using WebStoreProject.Storage;


namespace WebStoreProject.Queries.GoodsQueries
{
    public class GetOneGoodsCommandHandler
        : IRequestHandler<GetOneGoodsQuery.Request, GetOneGoodsQuery.Response>

    {
        public GetOneGoodsCommandHandler(StorageContext storageContext) => _storageContext = storageContext;

        private readonly StorageContext _storageContext;
        public async Task<GetOneGoodsQuery.Response> Handle(GetOneGoodsQuery.Request request, CancellationToken cancellationToken)
        {

            var response = new GetOneGoodsQuery.Response()
            {
                Goods = Mapper.MapGoods(_storageContext.Goods.SingleOrDefault(x => x.Article == request.Article))
            };
            if (response.Goods == null)
                throw new Exception("Такого артикула не существует!");
            return response;
        }


    }
}
