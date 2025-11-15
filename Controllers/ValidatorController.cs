using Microsoft.AspNetCore.Mvc;

namespace ir_coding_task_csv_validator.Controllers;
[ApiController]
[Route("[controller]")]
public class ValidatorController : ControllerBase
{
    private readonly ILogger<ValidatorController> _logger;

    public ValidatorController(ILogger<ValidatorController> logger)
    {
        _logger = logger;
    }

    [Route("Validate")]
    [HttpPost]
    public IActionResult Validate(IFormFile csvFile, IFormFile jobTitleFile)
    {
        /* 
         * To Do
         */
        return Ok();

    }
}
