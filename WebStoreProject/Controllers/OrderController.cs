using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebStoreProject.Commands.OrderCommands;
using WebStoreProject.Model;
using WebStoreProject.Queries.GoodsQueries;
using WebStoreProject.Queries.OrdersQueries;

namespace WebStoreProject.Controllers
{
    [ApiController]
    
    public class OrderController : ControllerBase
    {        

        private readonly ILogger<GoodsController> _logger;       
        private readonly IMediator _mediator;

        public OrderController(ILogger<GoodsController> logger,            
            IMediator mediator)
        {            
            _logger = logger;            
            _mediator = mediator;
        }

        [Route("orders")]
        [HttpGet]
        public async Task<ActionResult<Order>> Get()
        {
            var query = new GetOrdersQuery.Request();
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [Route("orders/{ordernum}")]
        [HttpGet]
        public async Task<ActionResult<Order>> GetOne(short ordernum)
        {
            var query = new GetOneOrderQuery.Request() { OrderNum = ordernum };
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [Route("orders/{ordernum}")]
        [HttpPost]
        public async Task<ActionResult> AddOrder([FromBody] Order order, [FromRoute] short ordernum)
        {
            var query = new AddOrderCommand.Request() { Order = order, OrderNum = ordernum };
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [Route("orders/{ordernum}")]
        [HttpPut]
        public async Task<ActionResult<Order>> PutOrder([FromBody] Order order, [FromRoute] short ordernum)
        {
            var query = new PutOrderCommand.Request() { Order = order, OrderNum = ordernum };
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [Route("orders/{ordernum}")]
        [HttpPatch]
        public async Task<ActionResult<Order>> PatchOrder([FromBody] Order order, [FromRoute] short ordernum)
        {
            var query = new PatchOrderCommand.Request() { Order = order, OrderNum = ordernum };
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [Route("orders/{ordernum}")]
        [HttpDelete]
        public async Task<ActionResult<Order>> Delete(short ordernum)
        {
            var query = new DeleteOrderCommand.Request() { OrderNum = ordernum };
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [Route("orders/forDate/{date}")]
        [HttpGet]
        public async Task<ActionResult<Order>> GetByDate(DateTime date)
        {
            var query = new GetOrdersForDateQuery.Request() { Date = date };
            var response = await _mediator.Send(query);
            return Ok(response);
        }
    }
}