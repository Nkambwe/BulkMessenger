﻿using BulkMessager.Utils;

namespace BulkMessager.Settings {
    /// <summary>
    /// Class reads environmental variables
    /// </summary>
    public static class ParamReader {
        /// <summary>
        /// Gets a value indicating whether database enviroment variable is installed
        /// </summary>
        /// <returns>True if variable exists otherwise False</returns>
        public static bool IsDatabaseInstalled()
            => !string.IsNullOrEmpty(Environment.GetEnvironmentVariable(ConfigParam.ConnectionString));

        /// <summary>
        /// Get database connection string from the enviroment variable
        /// </summary>
        /// <returns>Database connection string</returns>
        public static string GetConnectionString(ApplicationLogger<object> logger) {
            var str = Environment.GetEnvironmentVariable(ConfigParam.ConnectionString);

            if(str == null) {
               logger.LogInfo($"Bulk SMS Connection string Environment variable '{str}' is not set. Set variable and try again");
            }
            
            var result = "";
            try {
                result= Secure.DecryptString(str, ConfigParam.APIKEY);
            } catch (Exception ex) {
                logger.LogError("Source:: ParamReader - An error occurred while trying to decrypt Bulk SMS Connection string variable");
                logger.LogError($"{ex.Message}");
                logger.LogError($"{ex.StackTrace}");
            }
            return result;
        }

        /// <summary>
        /// Gets a value indicating whether SMS server enviroment variable is installed
        /// </summary>
        /// <returns>True if variable exists otherwise False</returns>
        public static bool IsSMSUrlInstalled()
            => !string.IsNullOrEmpty(Environment.GetEnvironmentVariable(ConfigParam.MessageServerUrl));

        /// <summary>
        /// Get Url for SMS client server
        /// </summary>
        /// <returns>Client Url</returns>
        public static string GetMessageServer(ApplicationLogger<object> logger) {
            var str = Environment.GetEnvironmentVariable(ConfigParam.MessageServerUrl);
            
            if(str == null) {
                logger.LogInfo($"Background SMS Service Client URL Environment variable '{str}' is not set. Set variable and try again");
            }
             var result = "";
            try {
                result= Secure.DecryptString(str, ConfigParam.APIKEY);
            } catch (Exception ex) {
                logger.LogError("Source:: ParamReader - An error occurred while trying to decrypt SMS Service Client URL variable");
                logger.LogError($"{ex.Message}");
                logger.LogError($"{ex.StackTrace}");
            }
            return result;
        }
    }
}
