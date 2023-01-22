using MediatR;
using WebStoreProject.Model;

namespace WebStoreProject.Queries.OrdersQueries
{
    public class GetOrdersQuery
    {
        public class Request : IRequest<Response>
        {

        }

        public class Response
        {
            public List<Order> OrdersList { get; set; }
        }
    }
}
