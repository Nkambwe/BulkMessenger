using BulkMessager.Data.Entities;

namespace BulkMessager.Data.Repositories {
    public interface IMessageRepository : IRepository<Message, long> {
    }
}
