using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Papara.Business.Cqrs.CqrsCommand;
using Papara.Schema.Request;

namespace Papara.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly IMediator mediator;

        public AuthController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> Signup([FromBody] UserSignupRequest request)
        {
            var result = await mediator.Send(new SignupUserCommand(request));
            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }
            return Ok(result.Data);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserRequest request)
        {
            var result = await mediator.Send(new LoginUserCommand(request));
            if (!result.IsSuccess)
            {
                return Unauthorized(result.Message);
            }
            return Ok(result.Data);
        }
    }
}
