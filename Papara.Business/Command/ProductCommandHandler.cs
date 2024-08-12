using AutoMapper;
using MediatR;
using Papara.Base.Response;
using Papara.Business.Cqrs.CqrsCommand;
using Papara.Data.Entities;
using Papara.Data.UnitOfWork;
using Papara.Schema.Request;
using Papara.Schema.Response;
using System.Threading;
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
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<BaseResponse<ProductResponse>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var mappedProduct = mapper.Map<ProductRequest, Product>(request.Request);

            // ProductCategories listesini başlat
            mappedProduct.ProductCategories = new List<ProductCategory>();

            // İlgili kategorileri ekle
            foreach (var categoryId in request.Request.CategoryIds)
            {
                var category = await unitOfWork.CategoryRepository.GetById(categoryId);
                if (category != null)
                {
                    mappedProduct.ProductCategories.Add(new ProductCategory
                    {
                        CategoryId = category.Id,
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
            var existingProduct = await unitOfWork.ProductRepository.GetById(request.ProductId);
            if (existingProduct == null)
            {
                return new BaseResponse("Product not found.");
            }

            mapper.Map(request.Request, existingProduct);

            // Mevcut kategorileri temizle
            existingProduct.ProductCategories.Clear();

            // Yeni kategorileri ekle
            foreach (var categoryId in request.Request.CategoryIds)
            {
                var category = await unitOfWork.CategoryRepository.GetById(categoryId);
                if (category != null)
                {
                    existingProduct.ProductCategories.Add(new ProductCategory
                    {
                        CategoryId = category.Id,
                        Product = existingProduct
                    });
                }
            }

            unitOfWork.ProductRepository.Update(existingProduct);
            await unitOfWork.SaveDatabase();
            return new BaseResponse();
        }

        public async Task<BaseResponse> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await unitOfWork.ProductRepository.GetById(request.ProductId);
            if (product == null)
            {
                return new BaseResponse("Product not found.");
            }

            await unitOfWork.ProductRepository.Delete(request.ProductId);
            await unitOfWork.SaveDatabase();
            return new BaseResponse();
        }
    }
}
