using BulkMessager.Settings;
using BulkMessager.Utils;

namespace BulkMessager.Services {
    public class MessagingBackgroundService : BackgroundService {

        private readonly IServiceScopeFactory _scopeFactory;
        private IApplicationLogger<MessagingBackgroundService> _logger;
        private IMessageService _messageService;

        public MessagingBackgroundService(IServiceScopeFactory scopeFactory){
            _scopeFactory = scopeFactory;
        }

         public async override Task StartAsync(CancellationToken token) {
            //register services
            using (var scope = _scopeFactory.CreateScope()) {
                _logger = scope.ServiceProvider.GetRequiredService<IApplicationLogger<MessagingBackgroundService>>();
                _logger.Channel = "SMS_BACKGROUND_WORKER";

                _messageService = scope.ServiceProvider.GetRequiredService<IMessageService>();
            }

            _logger.LogInfo("SMS Background woker service started...");
            _logger.LogInfo("SMS Background woker service registered LOG CHANNEL is 'SMS_BACKGROUND_WORKER' ");
            await base.StartAsync(token);
        }

        protected async override Task ExecuteAsync(CancellationToken token) {           
             _logger.LogInfo("SMS Background woker calling Repository method:: ...........................");
             
            var msg = _messageService.SayHello();
            _logger.LogInfo($"CALLED FROM BACKGROUND SERVICE :: {msg}");
            if (ParamReader.IsDatabaseInstalled() && ParamReader.IsSMSUrlInstalled()){
                int count = 0;
                while (!token.IsCancellationRequested) {
                    count++;
                    _logger.LogInfo($"MESSAGE NO.{count}");
                    SayHello(count);
                    await Task.Delay(1000, token);
                }
            }

            return;
            
        }

        public string SayHello(int id) {
            var msg =  $"Hello again the {id} time from background service";
            _logger.LogInfo(msg);
            return msg;
         }

        public async override Task StopAsync(CancellationToken token) {
            _logger.LogInfo("Background worker Service stopped..");
            await base.StopAsync(token);
        }

    }
}
