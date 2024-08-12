using MediatR;
using Microsoft.AspNetCore.Mvc;
using Papara.Base.Response;
using Papara.Business.Cqrs.CqrsQuery;
using Papara.Schema.Response;
using Papara.Business.Query;
using Papara.Business.Cqrs.CqrsCommand;
using Papara.Schema.Request;
using Microsoft.AspNetCore.Authorization;

namespace Papara.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Admin")]

    public class ProductsController : ControllerBase
    {
        private readonly IMediator mediator;
        public ProductsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<BaseResponse<List<ProductResponse>>> Get()
        {
            var query = new GetAllProductQuery();
            var result = await mediator.Send(query);
            return result;
        }

        [HttpPost]
        public async Task<BaseResponse<ProductResponse>> Post([FromBody] ProductRequest value)
        {
            var operation = new CreateProductCommand(value);
            var result = await mediator.Send(operation);
            return result;
        }
    }
}
