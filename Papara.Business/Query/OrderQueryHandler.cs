using AutoMapper;
using MediatR;
using Papara.Base.Response;
using Papara.Business.Cqrs.CqrsQuery;
using Papara.Data.Entities;
using Papara.Data.UnitOfWork;
using Papara.Schema.Response;

namespace Papara.Business.Query
{
    public class OrderQueryHandler :
        IRequestHandler<GetAllOrdersQuery, BaseResponse<List<OrderResponse>>>,
        IRequestHandler<GetOrderByIdQuery, BaseResponse<OrderResponse>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public OrderQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<BaseResponse<List<OrderResponse>>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
        {
            var orders = await unitOfWork.OrderRepository.GetAll();
            var response = mapper.Map<List<OrderResponse>>(orders);
            return new BaseResponse<List<OrderResponse>>(response);
        }

        public async Task<BaseResponse<OrderResponse>> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var order = await unitOfWork.OrderRepository.GetById(request.OrderId);
            var response = mapper.Map<OrderResponse>(order);
            return new BaseResponse<OrderResponse>(response);
        }
    }

    public class CouponQueryHandler :
        IRequestHandler<GetAllCouponsQuery, BaseResponse<List<CouponResponse>>>,
        IRequestHandler<GetCouponByIdQuery, BaseResponse<CouponResponse>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CouponQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<BaseResponse<List<CouponResponse>>> Handle(GetAllCouponsQuery request, CancellationToken cancellationToken)
        {
            var coupons = await unitOfWork.CouponRepository.GetAll();
            var response = mapper.Map<List<CouponResponse>>(coupons);
            return new BaseResponse<List<CouponResponse>>(response);
        }

        public async Task<BaseResponse<CouponResponse>> Handle(GetCouponByIdQuery request, CancellationToken cancellationToken)
        {
            var coupon = await unitOfWork.CouponRepository.GetById(request.CouponId);
            var response = mapper.Map<CouponResponse>(coupon);
            return new BaseResponse<CouponResponse>(response);
        }
    }
}
