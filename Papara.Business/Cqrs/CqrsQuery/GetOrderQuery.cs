using MediatR;
using Papara.Base.Response;
using Papara.Schema.Response;


namespace Papara.Business.Cqrs.CqrsQuery
{
    public record GetOrderQuery(long OrderId) : IRequest<BaseResponse<OrderResponse>>;

}
