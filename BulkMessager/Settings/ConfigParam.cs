namespace BulkMessager.Settings {
    public class ConfigParam {
        /// <summary>
        /// Gets the name of an environment variable with connection string settings
        /// </summary>
        public static string ConnectionString => "YOUR_CON_ENV_VAR_NAME";

        /// <summary>
        /// Gets the name of an environment variable for SMS messaging server url
        /// </summary>
        public static string MessageServerUrl => "YOUR_MESSAGE_CLIENT_URL_ENV_VAR_NAME";

        public const string APIKEY = "YOUR_ENCRPTION_KEY";
    }
}
