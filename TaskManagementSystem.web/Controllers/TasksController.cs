using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using TaskManagementSystem.Application.Tasks.Commands;
using TaskManagementSystem.Application.Tasks.Queries;

namespace TaskManagementSystem.web.Controllers
{
    [ApiController]
    [Route("api/tasks")]
    [Authorize]
    public class TasksController : ControllerBase
    {
        private readonly CreateTaskHandler _createHandler;
        private readonly GetTasksHandler _getTasksHandler;

       
        public TasksController(
            CreateTaskHandler createHandler,
            GetTasksHandler getTasksHandler)
        {
            _createHandler = createHandler;
            _getTasksHandler = getTasksHandler;
        }

        // POST /tasks
        [HttpPost]
        public async Task<IActionResult> Create(CreateTaskCommand cmd)
        {
            //var userId = Guid.Parse(User.FindFirst("sub")!.Value);
            var userIdClaim = User.FindFirst(JwtRegisteredClaimNames.Sub);

            if (userIdClaim is null)
                return Unauthorized("UserId claim missing");

            var userId = Guid.Parse(userIdClaim.Value);


            var id = await _createHandler.Handle(
                cmd with { UserId = userId }
            );

            return Ok(id);
        }

        // GET /tasks
        [Authorize(Roles = "Auditor")]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetTasksQuery query)
        {
            var result = await _getTasksHandler.Handle(query);
            return Ok(result);
        }
    }
}
