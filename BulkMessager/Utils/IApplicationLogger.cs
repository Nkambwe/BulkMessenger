namespace BulkMessager.Utils {
    public interface IApplicationLogger<T> {
        string Channel { get; set; }
        string LogId { get; set; }

        void LogCritical(string message);
        void LogError(string message);
        void LogInfo(string message);
        void LogStackTrace(string message);
        void LogWarning(string message);
    }
}