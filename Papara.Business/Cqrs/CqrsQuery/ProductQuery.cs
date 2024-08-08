using MediatR;
using Papara.Base.Response;
using Papara.Schema.Request;
using Papara.Schema.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Papara.Business.Cqrs.CqrsQuery
{

        public record GetAllProductQuery() : IRequest<BaseResponse<List<ProductResponse>>>;
        public record GetProductByIdQuery(long ProductId) : IRequest<BaseResponse<ProductResponse>>;
}
