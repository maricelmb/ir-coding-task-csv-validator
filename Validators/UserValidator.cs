using ir_coding_task_csv_validator.Helpers;
using ir_coding_task_csv_validator.Models;
using ir_coding_task_csv_validator.Validators;

namespace ir_coding_task_csv_validator.Services
{
    public class UserValidator(IJobTitleMapper jobMapper) : IUserValidator
    {
        private const string LengthErrorMessage = "Name is less than four characters.";
        private const string JobCodeErrorMessage = "Invalid job code.";
        private const string SalaryErrorMessage = "Salary not a positive integer.";
        private const string PostcodeMissingMessage = "Missing postcode.";
        private const string PostcodeInvalidMessage = "Postcode not valid for the state.";
        
        public ValidationResult Validate(User record)
        {
            var result = new ValidationResult { UserRecord = record };

            if (record.FirstName?.Length < 4 || record.LastName?.Length < 4)
                result.Messages.Add(new ValidationMessage { Level = Level.Warning, Message = LengthErrorMessage });

           
            if (!jobMapper.IsValidJobCode(record.Job_Code))
                result.Messages.Add(new ValidationMessage { Level = Level.Error, Message = JobCodeErrorMessage });
           

            if (record.Salary < 0)
                result.Messages.Add(new ValidationMessage { Level = Level.Warning, Message = SalaryErrorMessage });

            if (string.IsNullOrWhiteSpace(record.Postcode))
                result.Messages.Add(new ValidationMessage { Level = Level.Warning, Message = PostcodeMissingMessage });
            else if (!IsValidPostCodeForState(record.State, record.Postcode))
                result.Messages.Add(new ValidationMessage { Level = Level.Warning, Message = PostcodeInvalidMessage });

            return result;
        }


        //simplified postcode validation (not actual validation of Australian postcodes)
        private bool IsValidPostCodeForState(string state, string postCode)
        {
            //Todo: expand this to cover all states and actual postcode rules
            //Todo: add constants for state codes
            return state switch
            {
                "NSW" => postCode.StartsWith("2"),
                "VIC" => postCode.StartsWith("3"),
                _ => false // For simplicity, only NSW and VIC are considered valid
            };
        }
    }
}
