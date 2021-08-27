using MOP.Core.DomainObjects;
using MOP.Order.Domain.Enum;
using System.Collections.Generic;

namespace MOP.Order.Domain.Entities
{
    public class ProductModel : BaseEntity, IAggregateRoot
    {
        // EF Construtor
        public ProductModel()
        {

        }
        public ProductModel(string barCode, string description, decimal price, ProductGroupType productGroup)
        {
            BarCode = barCode;
            Description = description;
            Price = price;
            ProductGroup = productGroup;
        }

        public string BarCode { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public ProductGroupType ProductGroup { get; private set; }

        // EF Relação        
        public List<OrderItemModel> Items { get; private set; }

    }
}