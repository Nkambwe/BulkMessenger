
namespace BulkMessager { 

    public partial class Program  {

        public static void Main(string[] args) {

            var builder = WebApplication.CreateBuilder(args);
            builder.Logging.AddDebug();
            var startUp = new Startup();

            //..configure service pipeline
            startUp.ConfigureServices(builder.Services);

            //..application builder
            var app = builder.Build();
            startUp.Configure(app);

            //..run
            app.Run();
        }
    }

}