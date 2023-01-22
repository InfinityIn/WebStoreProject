using MediatR;
using WebStoreProject.Model;

namespace WebStoreProject.Commands.OrderCommands
{
    public class DeleteOrderCommand
    {
        public class Request : IRequest<Response>
        {
            public short OrderNum { get; set; }
        }

        public class Response
        {            
        }
    }
}
