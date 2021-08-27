using MOP.Order.Domain.Entities;
using System;
using System.Collections.Generic;

namespace MOP.Order.API.Application.DTO
{
    public class OrderDTO
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public DateTime StartedIn { get; set; }
        public DateTime? FinishedIn { get; set; }
        public int OrderStatus { get; set; }
        public string Shipping { get; set; }
        public decimal Total { get; set; }
        public string Observation { get; set; }
        public List<OrderItemDTO> Items { get; set; }

        public static OrderDTO ToOrderDTO(OrderModel order)
        {
            var orderDTO = new OrderDTO
            {
                Id = order.Id,
                CustomerId = order.CustomerId,
                StartedIn = order.StartedIn,
                FinishedIn = order.FinishedIn,
                OrderStatus = (int)order.OrderStatus,
                Shipping = order.Shipping.ToString(),
                Total = order.Total,
                Observation = order.Observation,
                Items = new List<OrderItemDTO>()
            };

            foreach (var item in order.Items)
            {
                orderDTO.Items.Add(new OrderItemDTO
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    Discount = (int)item.Discount,
                    DiscountValue = item.DiscountValue
                });
            }

            return orderDTO;
        }
    }
}