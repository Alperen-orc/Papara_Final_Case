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
    public record CreateCategoryCommand(CategoryRequest Request):IRequest<BaseResponse<CategoryResponse>>;
    public record UpdateCategoryCommand(long CategoryId,CategoryRequest Request):IRequest<BaseResponse>;
    public record DeleteCategoryCommand(long CategoryId):IRequest<BaseResponse>;
}
