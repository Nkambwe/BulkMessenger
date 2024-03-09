namespace BulkMessager.Utils {

    /// <summary>
    /// Class handles application logs for a given object
    /// </summary>
    /// <typeparam name="T">Type of object to log</typeparam>
    public class ApplicationLogger<T> {

        private readonly ILogger<T> _logger;
        private readonly string _fileName;
        private readonly bool _isFolder = false;

        public string LogId { get; set; }
        public string Channel { get; set; }

        public ApplicationLogger(ILogger<T> logger) {
            _logger = logger;
            _fileName = null;
        }

        public ApplicationLogger(ILogger<T> logger, string fileName, bool isFolder = false) {
            _logger = logger;
            _fileName = fileName;
            _isFolder = isFolder;
        }

        public void LogInfo(string message) {
            var logId = GenerateLogId();
            _logger.LogInformation("{LogId} {Message}", logId, message);

            //..log to file
            var filePath = GetFilePath();
            var msg = GenerateMessage(logId, message, Enum.GetName(typeof(LogLevel), LogLevel.Information));
            try {
                using StreamWriter file = new(filePath, true);
                file.WriteLine(msg);
            } catch (Exception ex) {
                LogStackTrace(ex.StackTrace);
            }
        }

        public void LogError(string message) {
            var logId = GenerateLogId();
            _logger.LogError("{LogId} {Message}", logId, message);

            //..log to file
            var filePath = GetFilePath();
            var msg = GenerateMessage(logId, message, Enum.GetName(typeof(LogLevel), LogLevel.Error));
            try {
                using StreamWriter file = new(filePath, true);
                file.WriteLine(msg);
            } catch (Exception ex) {
                LogStackTrace(ex.StackTrace);
            }

        }

        public void LogCritical(string message) {
            var logId = GenerateLogId();
            _logger.LogWarning("{LogId} {Message}", logId, message);

            //..log to file
            var filePath = GetFilePath();
            var msg = GenerateMessage(logId, message, Enum.GetName(typeof(LogLevel), LogLevel.Critical));
            try {
                using StreamWriter file = new(filePath, true);
                file.WriteLine(msg);
            } catch (Exception ex) {
                LogStackTrace(ex.StackTrace);
            }

        }

        public void LogStackTrace(string message) {
            var logId = GenerateLogId();
            _logger.LogTrace("{LogId} {Message}", logId, message);

            //..log to file
            var filePath = GetFilePath();
            var msg = GenerateMessage(logId, message, Enum.GetName(typeof(LogLevel), LogLevel.Trace));
            try {
                using StreamWriter file = new(filePath, true);
                file.WriteLine(msg);
            } catch (Exception ex1) {
                try {
                    using StreamWriter file = new("An error occurred while writing to log file", true);
                    file.WriteLine($"{ex1.Message}");
                    _logger.LogTrace("{LogId} {Message}", logId, ex1.StackTrace);
                } catch (Exception) {
                    return;
                }

            }
        }

        public void LogWarning(string message) {
            var logId = GenerateLogId();
            _logger.LogWarning("{LogId} {Message}", logId, message);

            //..log to file
            var filePath = GetFilePath();
            var msg = GenerateMessage(logId, message, Enum.GetName(typeof(LogLevel), LogLevel.Warning));
            try {
                using StreamWriter file = new(filePath, true);
                file.WriteLine(msg);
            } catch (Exception ex) {
                LogStackTrace(ex.StackTrace);
            }

        }

        #region Helper Methods

        private string GenerateMessage(string logId, string message, string type) {
            var timeIn = $"{DateTime.Now:yyyy.MM.dd HH:mm:ss} {GetAbbrev()}";
            return $"{timeIn}:{type}\t\t{(Channel != null ? $"CHANNEL: {Channel}\t" : "")}{logId}\t{message}";
        }

        private string GetFilePath() {
            // ... processing time
            var date = DateTime.Now.ToString("yyyy-MM-dd");

            //..generate file name
            string filename = @$"{(_fileName != null ? _isFolder ? @$"{_fileName}\Bulk_sms_Log_{date}" : $"{_fileName}_{date}" : $"Bulk_sms_Log_{date}")}.log";

            string filepath = @$"C:\Logs\BulkSms";
            try {
                //..create directory if not found
                if (!Directory.Exists(filepath)) {
                    Directory.CreateDirectory(filepath);
                }

                //..file path
                filepath = @$"{filepath}\{filename}";
                if (!File.Exists(filepath)) {
                    File.Create(filepath).Close();
                }
            } catch (Exception ex) {

                try {
                    //..file path
                    filepath = @$"{filepath}\{filename}";
                    if (!File.Exists(filepath)) {
                        File.Create(filepath).Close();
                    }

                    using StreamWriter file = new(filepath, true);
                    file.WriteLine($"Error creating log file >> {ex.Message}");
                } catch (Exception ex1) {
                    try {
                        using StreamWriter file = new("Critical Error! Something really bad happened when creating log file", true);
                        file.WriteLine($"{ex1.Message}");
                    } catch (Exception) {
                        return null;
                    }

                }
            }

            return filepath;
        }

        private static string GetAbbrev() {
            char[] delim = { ' ' };

            var tZone = TimeZoneInfo.FindSystemTimeZoneById("E. Africa Standard Time");
            var words = tZone.StandardName.Split(delim, StringSplitOptions.RemoveEmptyEntries);
            var abbrev = string.Empty;
            foreach (string chaStr in words) {
                abbrev = $"{abbrev}{chaStr[0]}";
            }

            return abbrev;
        }

        private static string GenerateLogId() {
            Random random = new();
            const string randomise = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(randomise, 11)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        #endregion

    }
}
