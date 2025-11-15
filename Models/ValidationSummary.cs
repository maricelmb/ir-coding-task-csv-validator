namespace ir_coding_task_csv_validator.Models
{
    public class ValidationSummary
    {
        public List<ValidationResult> UserValidationRecords { get; }

        // Total number of records processed
        public int TotalCount { get; }

        // Number of records that have at least one validation message
        public int RecordsWithMessages { get;}

        public ValidationSummary(List<ValidationResult> records, int recordsWithMessages)
        {
            UserValidationRecords = records;
            TotalCount = records.Count;
            RecordsWithMessages = recordsWithMessages;
        }
    }
}
