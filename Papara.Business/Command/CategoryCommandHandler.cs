using AutoMapper;
using MediatR;
using Papara.Base.Response;
using Papara.Business.Cqrs.CqrsCommand;
using Papara.Data.Entities;
using Papara.Data.UnitOfWork;
using Papara.Schema.Request;
using Papara.Schema.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Papara.Business.Command
{
    public class CategoryCommandHandler :
        IRequestHandler<CreateCategoryCommand, BaseResponse<CategoryResponse>>,
        IRequestHandler<UpdateCategoryCommand, BaseResponse>,
        IRequestHandler<DeleteCategoryCommand, BaseResponse>
    {

        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CategoryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<CategoryResponse>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var mappedCategory = mapper.Map<CategoryRequest, Category>(request.Request);
            await unitOfWork.CategoryRepository.Insert(mappedCategory);
            await unitOfWork.SaveDatabase();

            var response = mapper.Map<CategoryResponse>(mappedCategory);
            return new BaseResponse<CategoryResponse>(response);
        }

        public Task<BaseResponse> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
