using ir_coding_task_csv_validator.Helpers;
using ir_coding_task_csv_validator.Models;
using ir_coding_task_csv_validator.Validators;
using System.IO;

namespace ir_coding_task_csv_validator.Services
{
    public class ValidatorService(IUserValidator userValidator, 
                                  ICsvMapper csvMapper,
                                  IJobTitleMapper jobTitleMapper,
                                  IConfiguration config) : IValidatorService
    {  
        private const string CsvExpectedHeaderConfigKey = "CsvExpectedHeader";
        public ValidationSummary ValidateRecords(List<string> userCsvRows, List<string> jobTitleRows)
        {
            var userCsvExpectedHeaders = config.GetSection(CsvExpectedHeaderConfigKey).Get<List<string>>() ?? new List<string>();
            var usersTask = csvMapper.ParseAsync<User>(userCsvRows, true, userCsvExpectedHeaders);

            //to do: set the expected headers for job titles
            var jobTitlesTask = csvMapper.ParseAsync<Job>(jobTitleRows, false, new List<string>());
          
            jobTitleMapper.Initialize(jobTitlesTask);

            var results = new List<ValidationResult>();
            int recordsWithMessages = 0;
            

            foreach (var user in usersTask)
            {
                try
                {
                    // Process each user
                    user.JobTitle = jobTitleMapper.MapJobTitle(user.Job_Code);
                    //throw new Exception("Test exception"); // For testing exception handling
                    var validation = userValidator.Validate(user);
                    if (validation.Messages.Any())
                    {
                        recordsWithMessages++;
                    }
                    results.Add(validation);
                }
                catch (Exception ex)
                {
                    // Log the exception and continue processing other records
                }
            }

            ValidationSummary summary = new ValidationSummary(results, recordsWithMessages, usersTask.Count);

            return summary;
        }

       
    }
}
