using MediatR;
using WebStoreProject.Model;

namespace WebStoreProject.Queries.OrdersQueries
{
    public class GetOrdersForDateQuery
    {
        public class Request : IRequest<Response>
        {
            public DateTime? Date { get; set; }
        }

        public class Response
        {
            public List<Order> OrdersList { get; set; }
        }
    }
}
