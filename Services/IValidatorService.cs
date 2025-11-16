using ir_coding_task_csv_validator.Models;

namespace ir_coding_task_csv_validator.Validators
{
    public interface IValidatorService
    {
        Task<ValidationResult> ValidateRecords(User user, CancellationToken cancellationToken);
    }
}
