namespace ir_coding_task_csv_validator.Models
{
    public class ValidationMessage
    {
        public required Level Level { get; set; } // "Error" or "Warning"
        public required string Message { get; set; }
    }
}
