using BulkMessager.Data.Entities;
using BulkMessager.Utils;

namespace BulkMessager.Data.Repositories {

    public class MessageRepository : Repository<Message, long>, IMessageRepository {

        private readonly IApplicationLogger<Message> _logger;
        public MessageRepository(DataContext context, IApplicationLogger<Message> logger) 
            : base(context, logger) {
            _logger = logger;   
        }

    }
}
