using Microsoft.AspNetCore.Mvc;

namespace Judaica_2.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiAuthController : ControllerBase
    {
        private readonly JwtTokenGenerator _jwtTokenGenerator;

        public ApiAuthController()
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
