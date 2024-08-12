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
    public record CreateCouponCommand(CouponRequest Request) : IRequest<BaseResponse<CouponResponse>>;
    public record UpdateCouponCommand(long CouponId, CouponRequest Request) : IRequest<BaseResponse>;
    public record DeleteCouponCommand(long CouponId) : IRequest<BaseResponse>;
}
