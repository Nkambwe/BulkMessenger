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
        public static string GetConnectionString()
            => Environment.GetEnvironmentVariable(ConfigParam.ConnectionString);

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
        public static string GetMessageServer()
            => Environment.GetEnvironmentVariable(ConfigParam.MessageServerUrl);
    }
}
