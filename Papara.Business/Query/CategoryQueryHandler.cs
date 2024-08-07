using AutoMapper;
using MediatR;
using Papara.Base.Response;
using Papara.Business.Cqrs.CqrsQuery;
using Papara.Data.Entities;
using Papara.Data.UnitOfWork;
using Papara.Schema.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Papara.Business.Query
{
    
    public class CategoryQueryHandler:IRequestHandler<GetAllCategoryQuery,BaseResponse<List<CategoryResponse>>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CategoryQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<BaseResponse<List<CategoryResponse>>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
        {
            List<Category> entities=await unitOfWork.CategoryRepository.GetAll();
            var mappedList=mapper.Map<List<CategoryResponse>>(entities);
            return new BaseResponse<List<CategoryResponse>>(mappedList);
        }
    }
}
