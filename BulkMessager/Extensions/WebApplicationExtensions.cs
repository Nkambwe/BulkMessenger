﻿using BulkMessager.Settings;
using BulkMessager.Utils;
using Microsoft.AspNetCore.HttpOverrides;
using static System.Net.Mime.MediaTypeNames;

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

        public static void StartApiEngine(this WebApplication app) {
            // Get ILogger from the service provider
            var loggerFactory = app.Services.GetRequiredService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger<WebApplication>();

            //log application start
            ApplicationLogger<WebApplication> appLogger = new(logger);

            //make sure database connection string is set
            if (!ParamReader.IsDatabaseInstalled()){
                appLogger.LogError("Connection string variables has not been set. Use variable name 'MESSENGER_CONNECTIONSTRING' to set connection string in System Variables.");
                return;
            }
             
            //..make sure SMS Client URL is set
            if (!ParamReader.IsSMSUrlInstalled()) {
                appLogger.LogError("SMS server variables have not been set. Use variable variable name 'MESSENGER_SMSSERVERURL' to set SMS Messaging server URL");
                return;
            }

            //..log application start
            appLogger.LogInfo("Service started..");

        }

     }
}
