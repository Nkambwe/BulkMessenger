using BulkMessager.Settings;
using BulkMessager.Utils;
using Microsoft.AspNetCore.HttpOverrides;

namespace BulkMessager.Extensions {

    /// <summary>
    /// Class contains extension methods for Microsoft.AspNetCore.Builder.WebApplication class
    /// </summary>
    public static class WebApplicationExtensions {

        public static void ConfigureRequestPipeline(this WebApplication app) {

            if (app.Environment.IsDevelopment()) {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            if (!app.Environment.IsDevelopment()) {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.Use(async (context, next) => {
                context.Request.Headers.AcceptEncoding = "utf-16";
                await next.Invoke();
            });

            app.UseForwardedHeaders(new ForwardedHeadersOptions {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor |
                ForwardedHeaders.XForwardedProto
            });

            app.AllowHttpsRedirect();
            app.UseStaticFiles();
            app.ApplicationAuth();
            app.UseMvcControllers();
        }
       
        public static void ApplicationAuth(this WebApplication app) {
            app.UseAuthorization();
        }
        
        public static void AllowHttpsRedirect(this WebApplication app) {
            app.UseHttpsRedirection();
        }

        public static void UseMvcControllers(this WebApplication app) {
             app.MapControllers();
        }
        
        public static void UseStaticFiles(this WebApplication app) {
             app.MapControllers();
        }

     }
}
