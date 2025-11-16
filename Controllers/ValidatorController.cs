using ir_coding_task_csv_validator.Models;
using ir_coding_task_csv_validator.Validators;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ir_coding_task_csv_validator.Controllers;
[ApiController]
[Route("[controller]")]
public class ValidatorController : ControllerBase
{
    private readonly ILogger<ValidatorController> _logger;
    private readonly IValidatorService _validatorService;

    public ValidatorController(ILogger<ValidatorController> logger,
        IValidatorService validatorService)
    {
        _logger = logger;
        _validatorService = validatorService;
    }

    [Route("Validate")]
    [HttpPost]
    public async Task<IActionResult> Validate(IFormFile csvFile, IFormFile jobTitleFile)
    {
        if (csvFile == null || csvFile.Length == 0)
            return BadRequest("CSV file is required.");

        if (jobTitleFile == null || jobTitleFile.Length == 0)
            return BadRequest("Job Title File is required.");

        try
        {
            var result = await _validatorService.ValidateRecords(csvFile, jobTitleFile);
            return Ok(result);
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
