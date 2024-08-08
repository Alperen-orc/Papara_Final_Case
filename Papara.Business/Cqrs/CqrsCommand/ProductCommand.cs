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
    public record CreateProductCommand(ProductRequest Request) : IRequest<BaseResponse<ProductResponse>>;
    public record UpdateProductCommand(long ProductId, ProductRequest Request) : IRequest<BaseResponse>;
    public record DeleteProductCommand(long ProductId) : IRequest<BaseResponse>;
}
