using AutoMapper;
using MediatR;
using Papara.Base.Response;
using Papara.Business.Cqrs.CqrsCommand;
using Papara.Data.Entities;
using Papara.Data.UnitOfWork;
using Papara.Schema.Request;
using Papara.Schema.Response;

namespace Papara.Business.Command
{
    public class CouponCommandHandler :
        IRequestHandler<CreateCouponCommand, BaseResponse<CouponResponse>>,
        IRequestHandler<UpdateCouponCommand, BaseResponse>,
        IRequestHandler<DeleteCouponCommand, BaseResponse>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CouponCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<BaseResponse<CouponResponse>> Handle(CreateCouponCommand request, CancellationToken cancellationToken)
        {
            var mappedCoupon = mapper.Map<CouponRequest, Coupon>(request.Request);
            await unitOfWork.CouponRepository.Insert(mappedCoupon);
            await unitOfWork.SaveDatabase();

            var response = mapper.Map<CouponResponse>(mappedCoupon);
            return new BaseResponse<CouponResponse>(response);
        }

        public async Task<BaseResponse> Handle(UpdateCouponCommand request, CancellationToken cancellationToken)
        {
            var mappedCoupon = mapper.Map<CouponRequest, Coupon>(request.Request);
            mappedCoupon.Id = request.CouponId;
            unitOfWork.CouponRepository.Update(mappedCoupon);
            await unitOfWork.SaveDatabase();
            return new BaseResponse();
        }

        public async Task<BaseResponse> Handle(DeleteCouponCommand request, CancellationToken cancellationToken)
        {
            await unitOfWork.CouponRepository.Delete(request.CouponId);
            await unitOfWork.SaveDatabase();
            return new BaseResponse();
        }
    }
}
