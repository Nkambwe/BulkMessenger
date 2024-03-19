namespace BulkMessager.Dtos {
    public class MessageDto {
        public long Id { get; set; }
        public string Message { get; set; }
        public string Status { get; set; }
        public DateTime StartSending { get; set; }
        public DateTime? StopSending { get; set; }
        public string MessageInterval { get; set; }
        public string IntervalStatus { get; set; }
        public string MessageApproved { get; set; }
        public string AddedBy { get; set; }
        public DateTime AddedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public DateTime? LastSent { get; set; }
        public string MessageDeleted { get; set; }
    }
}
