using MediatR;
using WebStoreProject.Model;

namespace WebStoreProject.Commands.OrderCommands
{
    public class AddOrderCommand
    {
        public class Request : IRequest<Response>
        {
            public Order Order { get; set; }
            public short OrderNum { get; set; }
        }

        public class Response
        {            
        }
    }
}
