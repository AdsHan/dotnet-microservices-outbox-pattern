using MediatR;
using MOP.Order.API.Application.DTO;
using MOP.Order.Domain.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MOP.Order.API.Application.Messages.Queries.OrderQuery
{
    public class OrderQueryHandler :
        IRequestHandler<GetAllOrderQuery, List<OrderDTO>>,
        IRequestHandler<GetByIdOrderQuery, OrderDTO>
    {
        private readonly IOrderRepository _orderRepository;

        public OrderQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<List<OrderDTO>> Handle(GetAllOrderQuery request, CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.GetAllAsync();

            return orders.Select(OrderDTO.ToOrderDTO).ToList();
        }

        public async Task<OrderDTO> Handle(GetByIdOrderQuery request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(request.Id);

            if (order == null) return null;

            return OrderDTO.ToOrderDTO(order);
        }
    }
}
