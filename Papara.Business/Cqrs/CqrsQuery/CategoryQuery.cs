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
    public record GetAllCategoryQuery() : IRequest<BaseResponse<List<CategoryResponse>>>;
    public record GetCategoryByIdQuery(long CategoryId) : IRequest<BaseResponse<CategoryResponse>>;
}
