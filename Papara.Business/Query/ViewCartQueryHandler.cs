using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Papara.Base.Response;
using Papara.Business.Cqrs.CqrsQuery;
using Papara.Data.Entities;
using Papara.Data.UnitOfWork;
using Papara.Schema.Response;

namespace Papara.Business.Query
{
    public class ViewCartQueryHandler : IRequestHandler<ViewCartQuery, BaseResponse<OrderResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ViewCartQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<OrderResponse>> Handle(ViewCartQuery request, CancellationToken cancellationToken)
        {
            var order = await _unitOfWork.OrderRepository.GetCartByUserId(request.UserId);
            if (order == null)
            {
                return new BaseResponse<OrderResponse>("Cart not found.");
            }

            var response = _mapper.Map<OrderResponse>(order);
            return new BaseResponse<OrderResponse>(response);
        }
    }
}
