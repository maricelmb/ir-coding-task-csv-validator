using ir_coding_task_csv_validator.Helpers;
using ir_coding_task_csv_validator.Models;
using ir_coding_task_csv_validator.Validators;
using Microsoft.AspNetCore.Mvc;

namespace ir_coding_task_csv_validator.Controllers;
[ApiController]
[Route("[controller]")]
public class ValidatorController : ControllerBase
{
    private readonly ILogger<ValidatorController> _logger;
    private readonly IValidatorService _validatorService;
    private readonly ICsvMapper _csvMapper;

    public ValidatorController(ILogger<ValidatorController> logger,
        IValidatorService validatorService,
        ICsvMapper csvMapper)
    {
        _logger = logger;
        _validatorService = validatorService;
        _csvMapper = csvMapper;
    }

    [Route("Validate")]
    [HttpPost]
    public async Task<IActionResult> Validate(IFormFile csvFile, IFormFile jobTitleFile)
    {
        if (csvFile.Length == 0)
            return BadRequest("CSV file is required.");

        if (jobTitleFile.Length == 0)
            return BadRequest("Job Title File is required.");

        try
        {
            var csvFileRows = await _csvMapper.ReadAsList(csvFile);
            if (csvFileRows.Count == 0)
                return BadRequest("CSV file is empty or could not be read.");

            var jobTitleFileRows = await _csvMapper.ReadAsList(jobTitleFile);
            if (jobTitleFileRows.Count == 0)
                return BadRequest("Job Title file is empty or could not be read.");

            var result = _validatorService.ValidateRecords(csvFileRows, jobTitleFileRows);

            return Ok(result);
        }
        catch (InvalidDataException ex)
        {
            _logger.LogError(ex, "Mapping error occurred while processing request to validate users.");
            return BadRequest(new BaseResponse
            {
                Message = ex.Message
            });
        }
        catch (Exception ex)
        {
            const string GenericErrorMessage = "Error occurred while processing request to validate users.";
            _logger.LogError(ex, GenericErrorMessage);
            return StatusCode(StatusCodes.Status500InternalServerError, new BaseResponse
            {
                Message = GenericErrorMessage
            }); 
        }
    }
}
