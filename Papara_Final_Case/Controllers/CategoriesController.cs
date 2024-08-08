using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Papara.Base.Response;
using Papara.Business.Cqrs.CqrsCommand;
using Papara.Business.Cqrs.CqrsQuery;
using Papara.Schema.Request;
using Papara.Schema.Response;

namespace Papara.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IMediator mediator;
        public CategoriesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<BaseResponse<List<CategoryResponse>>> Get()
        {
            var query = new GetAllCategoryQuery();
            var result=await mediator.Send(query);
            return result;
        }

        [HttpPost]
        public async Task<BaseResponse<CategoryResponse>> Post([FromBody] CategoryRequest value)
        {
            var operation = new CreateCategoryCommand(value);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPut("{categoryId}")]
        public async Task<BaseResponse> Put(long categoryId, [FromBody] CategoryRequest value)
        {
            var query = new UpdateCategoryCommand(categoryId, value);
            var result = await mediator.Send(query);
            return result;
        }

        [HttpDelete("{categoryId}")]
        public async Task<BaseResponse> Delete(long categoryId)
        {
            var query = new DeleteCategoryCommand(categoryId);
            var result = await mediator.Send(query);
            return result;
        }
    }
}
