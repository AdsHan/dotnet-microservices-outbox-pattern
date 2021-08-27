using MongoDB.Driver;
using MOP.Notification.Worker.Data.Model;
using System.Threading.Tasks;

namespace MOP.Notification.Worker.Data.Repository
{
    public class MailRepository : IMailRepository
    {

        private readonly IMongoCollection<EmailTemplateModel> _collection;

        public MailRepository(IMongoDatabase database)
        {
            _collection = database.GetCollection<EmailTemplateModel>("email-templates");
        }

        public async Task<EmailTemplateModel> GetTemplate(string @event)
        {
            return await _collection.Find(c => c.Event == @event).SingleOrDefaultAsync();
        }

    }
}