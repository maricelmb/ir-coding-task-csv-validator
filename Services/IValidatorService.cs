using ir_coding_task_csv_validator.Models;

namespace ir_coding_task_csv_validator.Validators
{
    public interface IValidatorService
    {
        /// <summary>
        /// Assign the Job Title to each user based on the Job Code before validation.
        /// Validates the records from the provided CSV file and job title file.  
        /// </summary>
        ///<param name="userCsvRows">Csv Rows from user csv file</param>
        ///<param name="jobTitleRows">Job Title rows</param>
        /// <returns>Returns all processed rows and its validation messages</returns>
        ValidationSummary ValidateRecords(List<string> userCsvRows, List<string> jobTitleRows);
    }
}
