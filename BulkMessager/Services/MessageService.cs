using BulkMessager.Controllers;
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

         public string SayHello() {
            var msg = "Hello send from Messaging Service";
            _logger.LogInfo(msg);
            return msg;
         }
    }
}
