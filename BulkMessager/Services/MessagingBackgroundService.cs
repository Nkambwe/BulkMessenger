using AutoMapper;
using BulkMessager.Settings;
using BulkMessager.Utils;
using System.Threading;

namespace BulkMessager.Services {
    public class MessagingBackgroundService : BackgroundService {

        private readonly ApplicationLogger<MessagingBackgroundService> _appLogger;
        
        public MessagingBackgroundService(ILogger<MessagingBackgroundService> logger) {
            _appLogger = new(logger);
        }

        public async override Task StartAsync(CancellationToken token) {
            await base.StartAsync(token);

           
        }

        protected async override Task ExecuteAsync(CancellationToken token) {           
            
            if (ParamReader.IsDatabaseInstalled() && ParamReader.IsSMSUrlInstalled()){
                int count = 0;
                while (!token.IsCancellationRequested) {
                    count++;
                    _appLogger.LogInfo($"Sending message{count}");
                    _appLogger.LogInfo($"message{count} current interval status set to sent");
                    await Task.Delay(1000, token);
                }
            }

            return;
            
        }

        public async override Task StopAsync(CancellationToken token) {
            _appLogger.LogInfo("Service stopped..");
            await base.StopAsync(token);
        }

    }
}
