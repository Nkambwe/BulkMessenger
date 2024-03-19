using BulkMessager.Controllers;
using BulkMessager.Data.Entities;
using BulkMessager.Data.Repositories;
using BulkMessager.Utils;

namespace BulkMessager.Services {

    public class MessageService : IMessageService {

        private readonly IApplicationLogger<MessagingController> _logger;
        private readonly IMessageRepository _repo;

         public MessageService(IApplicationLogger<MessagingController> logger,
             IMessageRepository repo) {
            _logger = logger;
            _repo = repo;
            _logger.Channel = "SMS_SERVICE";
         }

        public async Task<bool> CreatMessageAsync(Message message) 
            => await _repo.InsertAsync(message);

        public bool Exists(long id) 
            => _repo.Exists(id);

        public async Task<bool> ExistsAsync(long id)
            => await _repo.ExistsAsync(m => m.Id == id);

        public Message FindMessage(long id) => _repo.Find(m => m.Id == id);

        public async Task<Message> FindMessageAsync(long id)
            => await _repo.FindByIdAsync(id);

        public IList<Message> GetAllMessages() 
            => _repo.GetAll();

        public async Task<IList<Message>> GetAllMessagesAsync()
            => await _repo.GetAllAsync();

        public string SayHello() {
            var msg = "Hello send from Messaging Service";
            _logger.LogInfo(msg);
            return msg;
         }
    }
}
