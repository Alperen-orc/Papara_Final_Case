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
    public record GetAllOrdersQuery : IRequest<BaseResponse<List<OrderResponse>>>;
    public record GetOrderByIdQuery(long OrderId) : IRequest<BaseResponse<OrderResponse>>;
    public record ViewCartQuery(long UserId) : IRequest<BaseResponse<OrderResponse>>;



}
