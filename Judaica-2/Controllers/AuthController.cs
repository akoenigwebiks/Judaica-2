using Microsoft.AspNetCore.Mvc;

namespace YourNamespace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly JwtTokenGenerator _jwtTokenGenerator;

        public AuthController()
        {
            _jwtTokenGenerator = new JwtTokenGenerator("YourSecretKeyHere", "YourIssuer", "YourAudience");
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel login)
        {
            if (IsValidUser(login))
            {
                var token = _jwtTokenGenerator.GenerateToken(login.Username);
                return Ok(new { token });
            }

            return Unauthorized();
        }

        private bool IsValidUser(LoginModel login)
        {
            // Replace with your user validation logic
            return login.Username == "test" && login.Password == "password";
        }
    }

    public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
