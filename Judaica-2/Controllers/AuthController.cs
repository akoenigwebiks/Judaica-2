using Judaica_2.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Judaica_2.Controllers
{
    public class AuthController : Controller
    {
        private readonly JwtTokenGenerator _jwtTokenGenerator;

        public AuthController()
        {
            _jwtTokenGenerator = new JwtTokenGenerator("YourSecretKeyHereYourSecretKeyHereYourSecretKeyHereYourSecretKeyHere", "YourIssuer", "YourAudience");
        }

        // This action is to serve the Login View
        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel loginViewModel)
        {
            if (loginViewModel==null || loginViewModel.Username.IsNullOrEmpty()||loginViewModel.Password.IsNullOrEmpty())
            {
                ModelState.AddModelError("InputEmpty", "Please fill all input errors");
                return View(loginViewModel);
            }

            if (loginViewModel.Username == "test" && loginViewModel.Password == "test")
            {
                string token = _jwtTokenGenerator.GenerateToken(loginViewModel.Username);
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("InvalidCredentials", "Invalid Credentials");

            return View(loginViewModel);
        }
    }
}
