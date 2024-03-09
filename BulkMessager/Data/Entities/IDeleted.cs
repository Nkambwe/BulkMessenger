namespace BulkMessager.Data.Entities {
    /// <summary>
    /// Interface marks message record as deleted
    /// </summary>
    public interface IDeleted {
        bool IsDeleted { get; set; }
    }
}
