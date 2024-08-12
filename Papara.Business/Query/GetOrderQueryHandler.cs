using AutoMapper;
using MediatR;
using Papara.Base.Response;
using Papara.Business.Cqrs.CqrsQuery;
using Papara.Data.UnitOfWork;
using Papara.Schema.Response;

namespace Papara.Business.Query
{
    public class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, BaseResponse<OrderResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetOrderQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<OrderResponse>> Handle(GetOrderQuery request, CancellationToken cancellationToken)
        {
            // Siparişi al
            var order = await _unitOfWork.OrderRepository.GetById(request.OrderId);

            if (order == null)
            {
                return new BaseResponse<OrderResponse>("Order not found");
            }

            // Response oluştur
            var response = _mapper.Map<OrderResponse>(order);
            return new BaseResponse<OrderResponse>(response);
        }
    }
}
