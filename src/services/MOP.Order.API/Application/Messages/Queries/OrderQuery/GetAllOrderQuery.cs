using MediatR;
using MOP.Order.API.Application.DTO;
using System.Collections.Generic;

namespace MOP.Order.API.Application.Messages.Queries.OrderQuery
{
    public class GetAllOrderQuery : IRequest<List<OrderDTO>>
    {
    }
}
