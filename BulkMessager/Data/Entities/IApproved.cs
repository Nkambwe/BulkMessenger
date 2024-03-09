namespace BulkMessager.Data.Entities {
    /// <summary>
    /// Interface marks a message as approved
    /// </summary>
    public interface IApproved {
        bool IsApproved { get; set; }
    }
}
