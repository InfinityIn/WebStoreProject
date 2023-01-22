using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebStoreProject.Model;
using WebStoreProject.Queries.GoodsQueries;

namespace WebStoreProject.Controllers
{
    [ApiController]
    
    public class GoodsController : ControllerBase
    {        

        private readonly ILogger<GoodsController> _logger;       
        private readonly IMediator _mediator;

        public GoodsController(ILogger<GoodsController> logger,            
            IMediator mediator)
        {            
            _logger = logger;            
            _mediator = mediator;
        }

        [Route("goods")]
        [HttpGet]
        public async Task<ActionResult<Goods>> Get()
        {
            var query = new GetOrderssQuery.Request();
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [Route("goods/{article}")]
        [HttpGet]
        public async Task<ActionResult<Goods>> GetOne(sbyte article)
        {
            var query = new GetOneGoodsQuery.Request() { Article = article};
            var response = await _mediator.Send(query);
            return Ok(response);
        }
    }
}