using BulkMessager.Data.Entities;

namespace BulkMessager.Services {
    public interface IMessageService {
        string SayHello();
        bool Exists(long id);
        Task<bool> ExistsAsync(long id);
        Message FindMessage(long id);
        Task<Message> FindMessageAsync(long id);
        IList<Message> GetAllMessages();
        Task<IList<Message>> GetAllMessagesAsync();

        Task<bool> CreatMessageAsync(Message message);
    }
}
