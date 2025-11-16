namespace ir_coding_task_csv_validator.Models
{
    /// <summary>
    /// Represents a validation message with a severity level and message content.
    /// </summary>
    public class ValidationMessage
    {
        public required Level Level { get; set; } // "Error" or "Warning"
        public required string Message { get; set; }
    }
}
