using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Application.Auth;

namespace TaskManagementSystem.web.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly LoginHandler _loginHandler;

        public AuthController(LoginHandler loginHandler)
        {
            _loginHandler = loginHandler;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest req)
        {
            var result = await _loginHandler.Handle(req);

            if (result == null)
                return Unauthorized();

            return Ok(result);
        }
    }
}
