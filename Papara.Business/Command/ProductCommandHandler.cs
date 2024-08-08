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
    public class ProductCommandHandler :
     IRequestHandler<CreateProductCommand, BaseResponse<ProductResponse>>,
     IRequestHandler<UpdateProductCommand, BaseResponse>,
     IRequestHandler<DeleteProductCommand, BaseResponse>
    {

        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public ProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<ProductResponse>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var mappedProduct = mapper.Map<ProductRequest, Product>(request.Request);

            // Ensure ProductCategories is initialized
            mappedProduct.ProductCategories = mappedProduct.ProductCategories ?? new List<ProductCategory>();

            // Add associated categories
            foreach (var categoryId in request.Request.CategoryIds)
            {
                var category = await unitOfWork.CategoryRepository.GetById(categoryId);
                if (category != null)
                {
                    mappedProduct.ProductCategories.Add(new ProductCategory
                    {
                        CategoryId = category.Id,
                        ProductId = mappedProduct.Id,
                        Category = category,
                        Product = mappedProduct
                    });
                }
            }

            await unitOfWork.ProductRepository.Insert(mappedProduct);
            await unitOfWork.SaveDatabase();

            var response = mapper.Map<ProductResponse>(mappedProduct);
            return new BaseResponse<ProductResponse>(response);
        }
        public async Task<BaseResponse> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var mappedProduct = mapper.Map<ProductRequest, Product>(request.Request);
            mappedProduct.Id = request.ProductId;
            unitOfWork.ProductRepository.Update(mappedProduct);
            await unitOfWork.SaveDatabase();
            return new BaseResponse();

        }

        public async Task<BaseResponse> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            await unitOfWork.ProductRepository.Delete(request.ProductId);
            await unitOfWork.SaveDatabase();
            return new BaseResponse();
        }
    }

}
