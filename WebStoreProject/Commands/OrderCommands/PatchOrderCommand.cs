using MediatR;
using WebStoreProject.Model;

namespace WebStoreProject.Commands.OrderCommands
{
    public class PatchOrderCommand
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
