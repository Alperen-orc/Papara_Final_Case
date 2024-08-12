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
    public record CreateOrderCommand(OrderRequest Request) : IRequest<BaseResponse<OrderResponse>>;
    public record UpdateOrderCommand(long OrderId, OrderRequest Request) : IRequest<BaseResponse>;
    public record DeleteOrderCommand(long OrderId) : IRequest<BaseResponse>;
    public record AddToCartCommand(long UserId, OrderDetailRequest OrderDetail) : IRequest<BaseResponse<OrderResponse>>;
    public record CompleteOrderCommand(long UserId, string CouponCode) : IRequest<BaseResponse<OrderResponse>>;


}



