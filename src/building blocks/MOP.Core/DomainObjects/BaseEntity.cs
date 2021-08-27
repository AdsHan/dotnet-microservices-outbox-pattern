using MOP.Core.Enums;
using System;

namespace MOP.Core.DomainObjects
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }
        public EntityStatusEnum Status { get; set; }
        public DateTime DateCreateAt { get; private set; }
        public DateTime? DateDeleteAt { get; private set; }

        protected BaseEntity()
        {

            DateCreateAt = DateTime.Now;
            Status = EntityStatusEnum.Active;
        }

        public void Delete()
        {
            if (Status == EntityStatusEnum.Active)
            {
                Status = EntityStatusEnum.Inactive;
                DateDeleteAt = DateTime.Now;
            }
        }
    }
}