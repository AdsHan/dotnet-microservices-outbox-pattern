using MOP.Core.Commands;
using System;

namespace MOP.Order.API.Application.Messages.Commands.OrderCommand
{

    public class DeleteOrderCommand : Command
    {
        public DeleteOrderCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}