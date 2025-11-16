using ir_coding_task_csv_validator.Helpers;
using ir_coding_task_csv_validator.Models;
using ir_coding_task_csv_validator.Validators;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace ir_coding_task_csv_validator.Controllers;
[ApiController]
[Route("[controller]")]
public class ValidatorController : ControllerBase
{
    private readonly ILogger<ValidatorController> _logger;
    private readonly IValidatorService _validatorService;
    private readonly ICsvMapper _csvMapper;
    private readonly IJobTitleMapper _jobMapper;

    public ValidatorController(ILogger<ValidatorController> logger,
        IValidatorService validatorService,
        ICsvMapper mapper,
        IJobTitleMapper jobMapper)
    {
        _logger = logger;
        _validatorService = validatorService;
        _csvMapper = mapper;
        _jobMapper = jobMapper;
    }

    [Route("Validate")]
    [HttpPost]
    public async Task<IActionResult> Validate(IFormFile csvFile, IFormFile jobTitleFile, CancellationToken cancellationToken)
    {
        if (csvFile == null || csvFile.Length == 0)
            return BadRequest("CSV file is required.");

        if (jobTitleFile == null || jobTitleFile.Length == 0)
            return BadRequest("Job Title File is required.");

        try
        {
            //var result = await _validatorService.ValidateRecords(csvFile, jobTitleFile);

            var results = new List<ValidationResult>();
            const int BATCH_SIZE = 800;  // Tune for performance
            var batch = new List<Task<ValidationResult>>(BATCH_SIZE);

            await using var jobStream = jobTitleFile.OpenReadStream();
            var jobTitles = await _csvMapper.ParseAsync<Job>(jobStream).ConfigureAwait(false);
            _jobMapper.Initialize(jobTitles);
            await using var csvStream = csvFile.OpenReadStream();
          
            await foreach (var record in _csvMapper.ParseAsync<User>(csvStream, cancellationToken)
                                               .ConfigureAwait(false))
            {
                // Add validation tasks to batch
                batch.Add(_validatorService.ValidateRecords(record, cancellationToken));

                // Execute when batch is full
                if (batch.Count >= BATCH_SIZE)
                {
                    var completed = await Task.WhenAll(batch).ConfigureAwait(false);
                    results.AddRange(completed);
                    batch.Clear();
                }
            }

            // Handle leftover tasks
            if (batch.Count > 0)
            {
                var completed = await Task.WhenAll(batch).ConfigureAwait(false);
                results.AddRange(completed);
            }


            return Ok(results);
        }
        catch (Exception ex)
        {
            const string GENERIC_ERROR_MESSAGE = "Error occurred while processing request to validate users.";
            _logger.LogError(ex, GENERIC_ERROR_MESSAGE);
            return StatusCode(StatusCodes.Status500InternalServerError, new BaseResponse
            {
                Message = GENERIC_ERROR_MESSAGE
            }); 
        }
    }
}
