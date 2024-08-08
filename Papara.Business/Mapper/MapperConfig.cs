using AutoMapper;
using Papara.Data.Entities;
using Papara.Schema.Request;
using Papara.Schema.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Papara.Business.Mapper
{
    public class MapperConfig:Profile
    {
        public MapperConfig()
        {
            // Category Mappings
            CreateMap<Category, CategoryResponse>();
            CreateMap<CategoryRequest, Category>();

            // Product Mappings
            CreateMap<Product, ProductResponse>()
                .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.ProductCategories.Select(pc => new CategoryResponse
                {
                    Id = pc.Category.Id,
                    Name = pc.Category.Name
                })));

            CreateMap<ProductRequest, Product>()
                .ForMember(dest => dest.ProductCategories, opt => opt.Ignore());

            // ProductCategory Mappings
            CreateMap<ProductCategory, ProductCategoryResponse>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));
            CreateMap<ProductCategoryRequest, ProductCategory>();
        }
    }
}
