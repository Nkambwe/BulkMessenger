using System.ComponentModel.DataAnnotations.Schema;

namespace BulkMessager.Data.Entities {
    [Table("Sms_Message")]
    public class Message : DomainEntity<long>, IApproved {
        /// <summary>
        /// Get/Set message content
        /// </summary>
        [Column("Msg_Content")]
        public string Text { get; set; }
        /// <summary>
        /// Get/Set message run duration based on RunFrom and RunTo dates. 
        /// </summary>
        /// <remarks>Once RunTo date is reached, Duration is set to Completed by the system</remarks>
        [Column("Msg_Duration")]
        public MessageDuration Duration { get; set; }
        /// <summary>
        /// Get/Set the start date to which message will be sent
        /// </summary>
        [Column("Msg_From")]
        public DateTime RunFrom { get; set; }
        /// <summary>
        /// Get/Set last date message is expected to be sent
        /// </summary>
        [Column("Msg_To")]
        public DateTime? RunTo { get; set; }
        /// <summary>
        /// Get/Set interval between on previous message and next message to be sent. <see cref="Interval" enumeration/>
        /// </summary>
        [Column("Msg_Interval")]
        public Interval Interval { get; set; }
        /// <summary>
        /// Get/Set whether current interval message has been sent
        /// </summary>
        [Column("Msg_Interval_status")]
        public IntervalStatus IntervalStatus { get; set; }
        /// <summary>
        /// Get/Set whether message has been approved
        /// </summary>
        [Column("Msg_Approved")]
        public bool IsApproved { get; set; }
        /// <summary>
        /// Get/Set the next date message is to be sent
        /// </summary>
        [Column("Msg_next_send_date")]
        public DateTime? NextSendDate { get; set; }
        /// <summary>
        /// Get/Set user who added message
        /// </summary>
        [Column("Msg_Added_by")]
        public string AddedBy { get; set; }
        /// <summary>
        /// Get/Set date when message was added
        /// </summary>
        [Column("Msg_Added_on")]
        public DateTime AddedOn { get; set; }
        /// <summary>
        /// Get/Set user who last modified this message
        /// </summary>
        [Column("Msg_Modified_by")]
        public string LastModifiedBy { get; set; }
        /// <summary>
        /// Get/Set date when message was last modified
        /// </summary>
         [Column("Msg_Modified_on")]
        public DateTime? LastModifiedOn { get; set; }
    }
}
