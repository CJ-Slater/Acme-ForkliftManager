using Application.Requests.Forklifts;
using Domain.Records;
using Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ObstacleController : ControllerBase
    {

        private readonly ILogger<ObstacleController> _logger;
        private readonly ISender _mediator;

        public ObstacleController(ILogger<ObstacleController> logger, ISender mediator)
        {
            _logger = logger;
            _mediator = mediator;   
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllObstacles()
        {
            return Ok(await _mediator.Send(new GetAllObstaclesRequest()));
        }


        [HttpDelete("reset")]
        public async Task<IActionResult> ResetObstacles()
        {
            return Ok(await _mediator.Send(new ResetObstaclesRequest()));
        }

        //Apologies - I had planned to add a call to this in the front-end so that you could set these from the site, but don't think I will have time to do it.
        //If you need to set the obstacles, you can do it in the in memory repo, or on the swagger API.
        [HttpPatch("set-obstacles")]
        public async Task<IActionResult> SetObstacles([FromBody] HashSet<ObstaclePosition> positions)
        {

            return Ok(await _mediator.Send(new SetObstaclesRequest(positions)));
        }
    }
}
