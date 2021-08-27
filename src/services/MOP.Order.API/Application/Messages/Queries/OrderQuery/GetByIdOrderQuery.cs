using MediatR;
using MOP.Order.API.Application.DTO;
using System;

namespace MOP.Order.API.Application.Messages.Queries.OrderQuery
{
    public class GetByIdOrderQuery : IRequest<OrderDTO>
    {
        public GetByIdOrderQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }
}
