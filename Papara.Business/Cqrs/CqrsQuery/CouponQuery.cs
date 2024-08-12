using MediatR;
using Papara.Base.Response;
using Papara.Schema.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Papara.Business.Cqrs.CqrsQuery
{
    public record GetAllCouponsQuery : IRequest<BaseResponse<List<CouponResponse>>>;
    public record GetCouponByIdQuery(long CouponId) : IRequest<BaseResponse<CouponResponse>>;
}
