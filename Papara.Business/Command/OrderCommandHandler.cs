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
    public class OrderCommandHandler :
        IRequestHandler<CreateOrderCommand, BaseResponse<OrderResponse>>,
        IRequestHandler<UpdateOrderCommand, BaseResponse>,
        IRequestHandler<DeleteOrderCommand, BaseResponse>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public OrderCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<BaseResponse<OrderResponse>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var mappedOrder = mapper.Map<OrderRequest, Order>(request.Request);
            await unitOfWork.OrderRepository.Insert(mappedOrder);
            await unitOfWork.SaveDatabase();

            var response = mapper.Map<OrderResponse>(mappedOrder);
            return new BaseResponse<OrderResponse>(response);
        }

        public async Task<BaseResponse> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var mappedOrder = mapper.Map<OrderRequest, Order>(request.Request);
            mappedOrder.Id = request.OrderId;
            unitOfWork.OrderRepository.Update(mappedOrder);
            await unitOfWork.SaveDatabase();
            return new BaseResponse();
        }

        public async Task<BaseResponse> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            await unitOfWork.OrderRepository.Delete(request.OrderId);
            await unitOfWork.SaveDatabase();
            return new BaseResponse();
        }
    }
}
