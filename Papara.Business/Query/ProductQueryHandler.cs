using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Papara.Base.Response;
using Papara.Business.Cqrs.CqrsQuery;
using Papara.Data.DatabaseContext;
using Papara.Data.Entities;
using Papara.Data.UnitOfWork;
using Papara.Schema.Response;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Papara.Business.Query
{
    public class ProductQueryHandler : IRequestHandler<GetAllProductQuery, BaseResponse<List<ProductResponse>>>
    {
        private readonly Context context;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public ProductQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, Context context)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.context = context;
        }

        public async Task<BaseResponse<List<ProductResponse>>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
        {
            // İlk olarak ürünleri getir
            List<Product> entities = await unitOfWork.ProductRepository.GetAll();

            // Her bir ürün için kategorileri yükle
            foreach (var product in entities)
            {
                await context.Entry(product)
                              .Collection(p => p.ProductCategories)
                              .Query()
                              .Include(pc => pc.Category)
                              .LoadAsync();
            }

            var mappedList = mapper.Map<List<ProductResponse>>(entities);
            return new BaseResponse<List<ProductResponse>>(mappedList);
        }

    }
}
