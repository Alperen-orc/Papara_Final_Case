using MediatR;
using Papara.Base.Response;
using Papara.Schema.Response;


namespace Papara.Business.Cqrs.CqrsQuery
{
    public record GetAllCategoryQuery() : IRequest<BaseResponse<List<CategoryResponse>>>;
    public record GetCategoryByIdQuery(long CategoryId) : IRequest<BaseResponse<CategoryResponse>>;
}
