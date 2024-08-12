using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Papara.Business.Cqrs.CqrsCommand;
using Papara.Business.Cqrs.CqrsQuery;
using Papara.Schema.Request;
using Papara.Schema.Response;

namespace Papara.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CouponController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CouponController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCoupon([FromBody] CouponRequest request)
        {
            var result = await _mediator.Send(new CreateCouponCommand(request));
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCoupon(long id, [FromBody] CouponRequest request)
        {
            var result = await _mediator.Send(new UpdateCouponCommand(id, request));
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCoupon(long id)
        {
            var result = await _mediator.Send(new DeleteCouponCommand(id));
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCoupons()
        {
            var result = await _mediator.Send(new GetAllCouponsQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCouponById(long id)
        {
            var result = await _mediator.Send(new GetCouponByIdQuery(id));
            return Ok(result);
        }
    }
}
