namespace BulkMessager.Data.Entities {
    /// <summary>
    /// Enum defining SMS run duration status based on message RunFrom and RunTo properties/>
    /// </summary>
    public enum MessageDuration {
        /// <summary>
        /// Duration not started. Set if RunFrom date is not yet reached
        /// </summary>
        Pending = 100,
        /// <summary>
        /// Duration is started and messages are being sent but not readed tge RunTo date
        /// </summary>
        Running = 200,
        /// <summary>
        /// Duration is completed and RunTo date is passed
        /// </summary>
        Completed = 300,
        /// <summary>
        /// Message sending has been paused before it started or before its ended
        /// </summary>
        Paused = 400
    }

    /// <summary>
    /// Enum defining interval between previous SMS message and Next
    /// </summary>
    public enum Interval {
        /// <summary>
        /// Messages to be sent on daily basis
        /// </summary>
        Daily = 1,
        /// <summary>
        /// Message to be sent on weekly basis
        /// </summary>
        Weekly = 2,
        /// <summary>
        /// Message to be sent on monthly basis
        /// </summary>
        Monthly = 3,
        /// <summary>
        /// Message sent on annual basis
        /// </summary>
        Annually = 4
    }
    
    /// <summary>
    /// Enum defining the current interval status of a message on current sending date
    /// </summary>
    public enum IntervalStatus {
        /// <summary>
        /// Message sent for the current interval
        /// </summary>
        Sent = 1,
        /// <summary>
        /// Message pending for the current interval
        /// </summary>
        Pending = 2
    }

    /// <summary>
    /// Enumeration sets message approval status
    /// </summary>
    public enum MessageApproved { 
        Pending = 0,
        Approved = 1       
    }

    public enum Deleted { 
        NO = 0,
        YES = 1       
    }
}
