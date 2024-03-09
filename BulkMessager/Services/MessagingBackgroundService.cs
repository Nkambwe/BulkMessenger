using AutoMapper;
using BulkMessager.Utils;

namespace BulkMessager.Services {
    public class MessagingBackgroundService : BackgroundService {

        private readonly IMapper _mapper;
        private readonly ApplicationLogger<MessagingBackgroundService> _appLogger;
        
        public MessagingBackgroundService(ILogger<MessagingBackgroundService> logger, IMapper mapper) {
            _appLogger = new(logger);
            _mapper = mapper;

        }

        protected async override Task ExecuteAsync(CancellationToken token) {
            
            int count = 0;
            while (!token.IsCancellationRequested) {
                count++;
                _appLogger.LogInfo($"Sending message{count}");
                _appLogger.LogInfo($"message{count} current interval status set to sent");
                await Task.Delay(1000, token);
            }
        }

        public async override Task StopAsync(CancellationToken cancellationToken) {
            //TODO - update statuses of messages currently to pending status before ending service
            _appLogger.LogInfo("Service stopped..");
            await Task.CompletedTask;
        }

    }
}
