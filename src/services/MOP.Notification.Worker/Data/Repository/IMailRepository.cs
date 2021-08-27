using MOP.Notification.Worker.Data.Model;
using System.Threading.Tasks;

namespace MOP.Notification.Worker.Data.Repository
{
    public interface IMailRepository
    {
        Task<EmailTemplateModel> GetTemplate(string @event);
    }
}