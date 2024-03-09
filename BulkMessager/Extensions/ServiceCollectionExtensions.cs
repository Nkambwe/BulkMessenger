using AutoMapper;
using BulkMessager.Services;
using BulkMessager.Settings;
using BulkMessager.Utils;
using Microsoft.EntityFrameworkCore;
using System.Net;
using static System.Net.Mime.MediaTypeNames;

namespace BulkMessager.Extensions {
    /// <summary>
    /// Class contains extension methods for Microsoft.Extensions.DependencyInjection.IServiceCollection interface
    /// </summary
    public static class ServiceCollectionExtensions {

        public static void ConfigureApplicationServices(this IServiceCollection services, WebApplication app) {
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

            //..db connection
            services.ConnectDatabase(app);

            //..add link for Mail API
            services.AddMailClient(app);
            
            //..add background service to process messaging
            services.AddHostedService<MessagingBackgroundService>();
            
            //..add Auto Mapper Configurations
            services.AddAutoMapper();

            //add accessor to HttpContext
            services.AddHttpContextAccessor();

        }

        
        public static void AddMailClient(this IServiceCollection services, WebApplication app) {

            services.AddHttpClient("MESSAGESERVER", c => {
                c.BaseAddress = new Uri(ParamReader.GetMessageServer());
            });
        }

        /// <summary>
        /// Connect database if conection string is found
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        public static void ConnectDatabase(this IServiceCollection services, WebApplication app) {

            services.AddDbContext<DataContext>(o =>
                o.UseSqlServer(ParamReader.GetConnectionString()));
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
            //services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            //services.AddScoped<IMessageRepository, MessageRepository>();

            //..register services
            //services.AddScoped<IMessageService, MessageService>();

        }
    }
}
