using MediatR;
using WebStoreProject.Model;
using WebStoreProject.Storage;


namespace WebStoreProject.Commands.OrderCommands
{
    public class DeleteOrderCommandHandler
        : IRequestHandler<DeleteOrderCommand.Request, DeleteOrderCommand.Response>
    {
        public DeleteOrderCommandHandler(StorageContext storageContext) => _storageContext = storageContext;

        private readonly StorageContext _storageContext;
        public async Task<DeleteOrderCommand.Response> Handle(DeleteOrderCommand.Request request, CancellationToken cancellationToken)
        {
            var order = _storageContext.Orders
                .SingleOrDefault(x => x.Number == request.OrderNum);

            if (order != null && order.Status != OrderStatus.Registered)
                throw new BusinessLogicException("Удаление заказа не в статусе Зарегистрирован запрещено");
                
            _storageContext.Orders.RemoveAll(x => x.Number == request.OrderNum);

            _storageContext.OrderItems.RemoveAll(x => x.OrderNum == request.OrderNum);     

            return new DeleteOrderCommand.Response() { };
        }        
    }
}
