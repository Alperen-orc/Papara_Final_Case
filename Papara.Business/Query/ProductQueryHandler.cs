using AutoMapper;
using MediatR;
using Papara.Base.Response;
using Papara.Business.Cqrs.CqrsQuery;
using Papara.Data.Entities;
using Papara.Data.UnitOfWork;
using Papara.Schema.Response;


namespace Papara.Business.Query
{
    public class ProductQueryHandler : IRequestHandler<GetAllProductQuery, BaseResponse<List<ProductResponse>>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public ProductQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<BaseResponse<List<ProductResponse>>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
        {
            List<Product> entities = await unitOfWork.ProductRepository.GetAll();
            var mappedList = mapper.Map<List<ProductResponse>>(entities);
            return new BaseResponse<List<ProductResponse>>(mappedList);
        }
    }

}
