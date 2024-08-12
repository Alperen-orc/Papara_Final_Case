using AutoMapper;
using Papara.Data.Dto;
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
            CreateMap<Category, CategoryResponse>();
            CreateMap<CategoryRequest, Category>();

            CreateMap<Product, ProductResponse>()
            .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.ProductCategories.Select(pc => pc.Category).ToList()));
            CreateMap<ProductRequest, Product>();

            CreateMap<UserSignupRequest, User>();
            CreateMap<User, UserResponse>();
            CreateMap<UserSignupResponse, UserResponse>();

            CreateMap<Order, OrderDto>()
            .ForMember(dest => dest.OrderDetails, opt => opt.MapFrom(src => src.OrderDetails));

            CreateMap<OrderDetail, OrderDetailDto>();

            CreateMap<Coupon, CouponResponse>();
            CreateMap<CouponRequest, Coupon>();
        }
    }
}
