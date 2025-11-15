using ir_coding_task_csv_validator.Helpers;
using ir_coding_task_csv_validator.Models;
using ir_coding_task_csv_validator.Validators;
using System.IO;

namespace ir_coding_task_csv_validator.Services
{
    public class ValidatorService(IUserValidator userValidator, 
                                  ICsvMapper csvMapper,
                                  IJobTitleMapper jobTitleMapper) : IValidatorService
    {  
        public async Task<ValidationSummary> ValidateRecords(IFormFile csvFile, IFormFile jobTitleFile)
        {
            var usersTask = await csvMapper.ParseAsync<User>(csvFile);
            var jobTitlesTask = await csvMapper.ParseAsync<Job>(jobTitleFile);
          
            jobTitleMapper.Initialize(jobTitlesTask);

            var results = new List<ValidationResult>();
            int recordsWithMessages = 0;
            

            foreach (var user in usersTask)
            {
                // Process each user
                user.JobTitle = jobTitleMapper.MapJobTitle(user.Job_Code);
                var validation = userValidator.Validate(user);
                if (validation.Messages.Any())
                {
                    recordsWithMessages++;
                }
                results.Add(validation);
            }

            ValidationSummary summary = new ValidationSummary(results, recordsWithMessages);

            return summary;
        }

       
    }
}
