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
            CreateMap<Product, ProductResponse>();
            CreateMap<ProductRequest, Product>();
        }
    }
}
