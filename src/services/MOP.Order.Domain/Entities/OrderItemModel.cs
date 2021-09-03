using MOP.Core.DomainObjects;
using MOP.Order.Domain.Enum;
using System;

namespace MOP.Order.Domain.Entities
{
    public class OrderItemModel : BaseEntity
    {
        // EF Construtor
        public OrderItemModel()
        {

        }

        public OrderItemModel(Guid orderId, Guid productId, int quantity, decimal unitPrice, DiscountType discount, decimal discountValue)
        {
            OrderId = orderId;
            ProductId = productId;
            Quantity = quantity;
            UnitPrice = unitPrice;
            Discount = discount;
            DiscountValue = discountValue;
        }

        public Guid? OrderId { get; private set; }
        public Guid ProductId { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }
        public DiscountType Discount { get; private set; }
        public decimal DiscountValue { get; private set; }

        // EF Relação        
        public OrderModel Order { get; private set; }
        public ProductModel Product { get; private set; }

        public decimal CalculateTotal()
        {
            var discount = CalculateTotalDiscount();

            var total = (Quantity * UnitPrice) - discount;

            return total < 0 ? 0 : total;
        }

        internal decimal CalculateTotalDiscount()
        {
            if (DiscountValue == 0) return 0;

            decimal discount = 0;

            if (Discount == DiscountType.Percentage)
            {
                discount = ((Quantity * UnitPrice) * DiscountValue) / 100;
            }
            else
            {
                discount = DiscountValue;
            }
            return discount;
        }

        public void Update(int quantity, decimal unitPrice, DiscountType discount, decimal discountValue)
        {
            Quantity = quantity;
            UnitPrice = unitPrice;
            Discount = discount;
            DiscountValue = discountValue;
        }
    }
}