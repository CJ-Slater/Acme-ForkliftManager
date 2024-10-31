using API.Requests;
using Application.Requests.Forklift;
using Application.Requests.Forklifts;
using Infrastructure.Services.File;
using Infrastructure.Services.ForkliftServices;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ForkliftController : ControllerBase
    {

        private readonly ILogger<ForkliftController> _logger;
        private readonly ISender _mediator;
        private readonly IForkliftService _forkliftService;
        private readonly IForkliftMovementCommandService<string, BatchMoveForkliftResponse> _forkliftStringMovementService;

        public ForkliftController(ILogger<ForkliftController> logger, ISender mediator, IForkliftService forkliftService, IForkliftMovementCommandService<string, BatchMoveForkliftResponse> forkliftStringMovementService)
        {
            _logger = logger;
            _mediator = mediator;
            _forkliftService = forkliftService;
            _forkliftStringMovementService = forkliftStringMovementService;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllForklifts()
        {
            return Ok(await _mediator.Send(new GetAllForkliftsRequest()));
        }

        [HttpPost("import")]
        public async Task<IActionResult> ImportForklifts(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");
            try
            {
                var fileInput = new FormFileUpload(file);
                var result = await _forkliftService.UploadForkliftFile(fileInput);
                return Ok(result);
            }
            catch (Exception ex) {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = $"An error occurred: {ex.Message}" });
            }
        }

        [HttpDelete("delete-all")]
        public async Task<IActionResult> DeleteAllForklifts()
        {
            return Ok(await _mediator.Send(new DeleteForkliftsRequest()));
        }

        [HttpPatch("move/{forkliftName}")]
        public async Task<IActionResult> MoveForklift(string forkliftName, [FromBody] MoveForklift moveCommand)
        {
            if (string.IsNullOrEmpty(moveCommand.CommandString))
                return BadRequest("No command string given.");
            if (string.IsNullOrEmpty(forkliftName))
                return BadRequest("No forklift name given.");

            return Ok(await _forkliftStringMovementService.ParseMovementAsync(moveCommand.CommandString, forkliftName));
        }
    }
}
