using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using TaskManagementSystem.Application.Tasks.Commands;
using TaskManagementSystem.Application.Tasks.Queries;
using System.Security.Claims;

namespace TaskManagementSystem.web.Controllers
{
    [ApiController]
    [Route("api/tasks")]
    [Authorize]
    public class TasksController : ControllerBase
    {
        private readonly CreateTaskHandler _createHandler;
        private readonly GetTasksHandler _getTasksHandler;
        private readonly CompleteTaskHandler _complete;
        private readonly ArchiveTaskHandler _archive;
        private readonly DeleteTaskHandler _delete;


        public TasksController(
            CreateTaskHandler createHandler,
            GetTasksHandler getTasksHandler,
            CompleteTaskHandler complete,
            ArchiveTaskHandler archive,
            DeleteTaskHandler delete)
        {
            _createHandler = createHandler;
            _getTasksHandler = getTasksHandler;
            _complete = complete;
            _archive = archive;
            _delete = delete;
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
        // PUT /api/tasks/{id}/complete
        [HttpPut("{id}/complete")]
        public async Task<IActionResult> Complete(Guid id)
        {
            var role = User.FindFirst(ClaimTypes.Role)?.Value;
            var isAdmin = role == "Admin";


            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value
       ?? User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value
       ?? throw new UnauthorizedAccessException("UserId missing")
   );

            await _complete.Handle(id, userId, isAdmin); 
            return NoContent();
        }

        // PUT /api/tasks/{id}/archive
        [HttpPut("{id}/archive")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Archive(Guid id)
        {
            await _archive.Handle(id);
            return NoContent();
        }

        // DELETE /api/tasks/{id} (Soft delete)
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _delete.Handle(id);
            return NoContent();
        }

    }
}
