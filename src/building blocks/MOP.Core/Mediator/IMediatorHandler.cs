using MOP.Core.Commands;
using MOP.Core.Communication;
using System.Threading.Tasks;

namespace MOP.Core.Mediator
{
    public interface IMediatorHandler
    {
        Task<BaseResult> SendCommand<T>(T command) where T : Command;
        Task<object> SendQuery<T>(T query);
    }
}