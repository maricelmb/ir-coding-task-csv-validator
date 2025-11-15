using Microsoft.AspNetCore.Components.Forms;

namespace ir_coding_task_csv_validator.Models
{
    public class ValidationResult
    {
        public required User Record { get; set; }
        public List<ValidationMessage> Messages { get; set; } = new();
    }
}
