using MediatR;
using WebStoreProject.Model;

namespace WebStoreProject.Queries.OrdersQueries
{
    public class GetOneOrderQuery
    {
        public class Request : IRequest<Response>
        {
            public short OrderNum { get; set; }
        }

        public class Response
        {
            public Order Order { get; set; }
        }
    }
}
