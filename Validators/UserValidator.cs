using ir_coding_task_csv_validator.Helpers;
using ir_coding_task_csv_validator.Models;
using ir_coding_task_csv_validator.Validators;

namespace ir_coding_task_csv_validator.Services
{
    public class UserValidator(IJobTitleMapper jobMapper) : IUserValidator
    {
        public ValidationResult Validate(User record)
        {
            var result = new ValidationResult { UserRecord = record };

            if (record.FirstName?.Length < 4 || record.LastName?.Length < 4)
                result.Messages.Add(new ValidationMessage { Level = Level.Warning, Message = "Name is less than four characters." });

           
            if (!jobMapper.IsValidJobCode(record.Job_Code))
                result.Messages.Add(new ValidationMessage { Level = Level.Error, Message = "Invalid job code." });
           

            if (record.Salary < 0)
                result.Messages.Add(new ValidationMessage { Level = Level.Warning, Message = "Salary not a positive integer." });

            if (string.IsNullOrWhiteSpace(record.Postcode))
                result.Messages.Add(new ValidationMessage { Level = Level.Warning, Message = "Missing postcode." });
            else if (!IsValidPostCodeForState(record.State, record.Postcode))
                result.Messages.Add(new ValidationMessage { Level = Level.Warning, Message = "Postcode not valid for the state." });

            return result;
        }


        //simplified postcode validation
        private bool IsValidPostCodeForState(string state, string postCode)
        {   
            return state switch
            {
                "NSW" => postCode.StartsWith("2"),
                "VIC" => postCode.StartsWith("3"),
                _ => false // For simplicity, only NSW and VIC are considered valid
            };
        }
    }
}
