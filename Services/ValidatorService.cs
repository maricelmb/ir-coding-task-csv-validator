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
        public async Task<ValidationResult> ValidateRecords(User user, CancellationToken cancellationToken )
        {
            //var usersTask = await csvMapper.ParseAsync<User>(csvFile);
            //var jobTitlesTask = await csvMapper.ParseAsync<Job>(jobTitleFile);

            //jobTitleMapper.Initialize(jobTitlesTask);

            //var results = new List<ValidationResult>();
            //int recordsWithMessages = 0;


            //foreach (var user in usersTask)
            //{
            //    try
            //    {
            //        // Process each user
            //        user.JobTitle = jobTitleMapper.MapJobTitle(user.Job_Code);
            //        //throw new Exception("Test exception"); // For testing exception handling
            //        var validation = userValidator.Validate(user);
            //        if (validation.Messages.Any())
            //        {
            //            recordsWithMessages++;
            //        }
            //        results.Add(validation);
            //    }
            //    catch (Exception ex)
            //    {
            //        // Log the exception and continue processing other records
            //    }
            //}

            //ValidationSummary summary = new ValidationSummary(results, recordsWithMessages, usersTask.Count);

            //return summary;

            //Process each user
            user.JobTitle = jobTitleMapper.MapJobTitle(user.Job_Code);     
            var result = userValidator.Validate(user);

            return result;
        }


    }
}
