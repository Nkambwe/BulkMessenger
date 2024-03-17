using AutoMapper;
using BulkMessager.Data;
using BulkMessager.Data.Repositories;
using BulkMessager.Services;
using BulkMessager.Settings;
using BulkMessager.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net;

namespace BulkMessager.Extensions {
    /// <summary>
    /// Class contains extension methods for Microsoft.Extensions.DependencyInjection.IServiceCollection interface
    /// </summary
    public static class ServiceCollectionExtensions {

        public static void ConfigureApplicationServices(this IServiceCollection services) {
            //let the operating system decide what TLS protocol version to use
            ServicePointManager.SecurityProtocol = SecurityProtocolType.SystemDefault;

            //..configure API controllers
            services.ConfigureMvc();

            //..swagger
            services.AddSwaggerGen();

            //..add logging
            services.AddLogging();

            //..register repositories
            services.LoadRegister();

            var loggerFactory = LoggerFactory.Create(builder => {
                builder.AddConsole(); 
                builder.AddDebug();   
            });
            var iLoger = loggerFactory.CreateLogger<ApplicationLogger<object>>();
            ApplicationLogger<object> logger = new(iLoger);
            logger.LogInfo("Bulk SMS service started..");

            //..db connection
            services.ConnectDatabase(logger);

            //..add link for Mail API
            services.AddMailClient(logger);
            
            //..add background service to process messaging
            services.AddHostedService<MessagingBackgroundService>();
            
            //..add Auto Mapper Configurations
            services.AddAutoMapper();

            //add accessor to HttpContext
            services.AddHttpContextAccessor();

        }

        public static void AddMailClient(this IServiceCollection services, ApplicationLogger<object> logger) {

            var url = ParamReader.GetMessageServer(logger);
            if(url == null) {
               return;
            }

            try {
                var secureStr = Secure.EncryptString(url, ConfigParam.APIKEY);
                logger.LogInfo($"Background service client URL value >> {secureStr}");
            } catch (Exception ex) {
                logger.LogError("Source:: Bulk SMS Service - An error occurred while trying to encrypt variable");
                logger.LogError($"{ex.Message}");
                logger.LogError($"{ex.StackTrace}");
            }
            
            services.AddHttpClient("MESSAGESERVER", c => {
                c.BaseAddress = new Uri(url);
            });
        }

        /// <summary>
        /// Connect database if conection string is found
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        public static void ConnectDatabase(this IServiceCollection services, ApplicationLogger<object> logger) {
            var connectionString = ParamReader.GetConnectionString(logger);
            if(connectionString == null) {
               return;
            } 

            try {
                var secureStr = Secure.EncryptString(connectionString, ConfigParam.APIKEY);
                logger.LogInfo($"Datebase connection string value :: {secureStr}");
            } catch (Exception ex) {
                logger.LogError("Source:: Bulk SMS Service - An error occurred while trying to encrypt variable");
                logger.LogError($"{ex.Message}");
                logger.LogError($"{ex.StackTrace}");
            }

            services.AddDbContextFactory<DataContext>(o => 
            o.UseSqlServer(connectionString));
        }

        public static void ConfigureMvc(this IServiceCollection services) {
            services.AddRazorPages();
            services.AddControllers()
                .AddXmlSerializerFormatters()
                .AddXmlDataContractSerializerFormatters();
            services.AddEndpointsApiExplorer();
        }

        /// <summary>
        /// Configure API authentication
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        public static void ConfigureAuthenticationService(this IServiceCollection services) {
            services.AddAuthentication();
        }

        /// <summary>
        /// Register automapper to the pipeline
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        public static void AddAutoMapper(this IServiceCollection services) {
            var mappingConfig = new MapperConfiguration(mc => {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
        }

        /// <summary>
        /// Register HttpContextAccessor to the pipeline
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        public static void AddHttpContextAccessor(this IServiceCollection services)
            => services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        /// <summary>
        /// Register service dependecies
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        public static void LoadRegister(this IServiceCollection services) {

            //..repositories
            services.AddScoped(typeof(IApplicationLogger<>), typeof(ApplicationLogger<>));
            services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
            services.AddScoped<IMessageRepository, MessageRepository>();

            //..register services
            services.AddScoped<IMessageService, MessageService>();

        }
    }
}
