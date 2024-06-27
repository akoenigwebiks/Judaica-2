using Judaica_2.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Judaica_2.Controllers
{
    public class AuthController : Controller
    {

        // This action is to serve the Login View
        [HttpGet]
        public IActionResult Login()
        {
            return View("Login", new LoginViewModel());
        }
    }
}
