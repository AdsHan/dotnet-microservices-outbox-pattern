using MediatR;
using MOP.Core.Communication;
using System;

namespace MOP.Core.Commands
{
    public abstract class Command : IRequest<BaseResult>
    {
        protected Command()
        {
            BaseResult = new BaseResult();
        }

        public BaseResult BaseResult { get; set; }

        public virtual bool Validate()
        {
            throw new NotImplementedException();
        }
    }
}