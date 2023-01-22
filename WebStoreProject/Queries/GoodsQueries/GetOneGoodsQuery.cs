using MediatR;
using WebStoreProject.Model;

namespace WebStoreProject.Queries.GoodsQueries
{
    public class GetOneGoodsQuery
    {
        public class Request : IRequest<Response>
        {
            public sbyte Article { get; set; }
        }

        public class Response
        {
            public Goods Goods { get; set; }
        }
    }
}
