using MediatR;
using WebStoreProject.Model;

namespace WebStoreProject.Queries.GoodsQueries
{
    public class GetOrderssQuery
    {
        public class Request : IRequest<Response>
        {

        }

        public class Response
        {
            public List<Goods> GoodsList { get; set; }
        }
    }
}
