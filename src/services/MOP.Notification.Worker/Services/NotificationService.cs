using MOP.Notification.Worker.Configuration;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace MOP.Notification.Worker.Services
{
    public class NotificationService : INotificationService
    {

        private readonly WorkerSettings _workerSettings;

        public NotificationService(WorkerSettings workerSettings)
        {
            _workerSettings = workerSettings;
        }

        public async Task SendAsync(string subject, string content, string toEmail, string toName)
        {
            var from = new MailAddress(_workerSettings.NotificationFromEmail, _workerSettings.NotificationFromName);
            var to = new MailAddress(toEmail, toName);

            var message = new MailMessage(from, to);

            message.Subject = subject;
            message.Body = content;
            message.IsBodyHtml = true;

            // No caso do smtp do Google lembrar:
            // 1) Habilitar a opção "acesso a app menos seguro" 
            // 2) Desabilitar verificação em duas etapas

            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential(_workerSettings.NotificationFromEmail, "SuaSenhaSecreta"),
                EnableSsl = true
            };

            //client.Send(message);
        }
    }
}