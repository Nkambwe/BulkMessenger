using BulkMessager.Extensions;

namespace BulkMessager {

     public class Startup {

        /// <summary>
        /// Add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services) {      
            //..add service pipeline configurations
            services.ConfigureApplicationServices();
        }

        /// <summary>
        /// Configure the HTTP request pipeline. 
        /// </summary>
        /// <param name="app"></param>
        public void Configure(WebApplication app) {
            app.ConfigureRequestPipeline();
            app.StartApiEngine();
        }
     }
}
