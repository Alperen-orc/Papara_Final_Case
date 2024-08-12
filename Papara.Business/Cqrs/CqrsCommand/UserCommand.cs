using MediatR;
using Papara.Base.Response;
using Papara.Schema.Request;
using Papara.Schema.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Papara.Business.Cqrs.CqrsCommand
{
    public record LoginUserCommand(UserRequest Request) : IRequest<BaseResponse<UserResponse>>;
    public record SignupUserCommand(UserSignupRequest Request) : IRequest<BaseResponse<UserSignupResponse>>;


}
