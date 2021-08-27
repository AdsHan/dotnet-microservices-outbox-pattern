using MOP.Core.DomainObjects;
using System.Collections.Generic;

namespace MOP.Order.Domain.Entities
{
    public class CustomerModel : BaseEntity, IAggregateRoot
    {
        // EF Construtor
        public CustomerModel()
        {

        }

        public CustomerModel(string name, string phone, string email, string cep, string state, string city)
        {
            Name = name;
            Phone = phone;
            Email = email;
            CEP = cep;
            State = state;
            City = city;
        }

        public string Name { get; private set; }
        public string Phone { get; private set; }
        public string Email { get; private set; }
        public string CEP { get; private set; }
        public string State { get; private set; }
        public string City { get; private set; }

        // EF Relação        
        public List<OrderModel> Orders { get; private set; }

    }
}