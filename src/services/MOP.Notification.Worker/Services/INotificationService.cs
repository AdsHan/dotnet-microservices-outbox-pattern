using System.Threading.Tasks;

namespace MOP.Notification.Worker.Services
{
    public interface INotificationService
    {
        Task SendAsync(string subject, string content, string toEmail, string toName);
    }
}