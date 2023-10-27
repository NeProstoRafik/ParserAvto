using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using ParserAvto.Core.PersonBuild;
using ParserAvto.Models;
using System.Security.Claims;

namespace ParserAvto.Controllers
{
    public class UserController : Controller
    {
        private readonly PersonBuilder personBuilder;
        public UserController(PersonBuilder personBuilder)
        {
            this.personBuilder = personBuilder;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Login() => View();
        [HttpPost]
        public async Task<IActionResult> Login(Person model)
        {
            var user = personBuilder.Get(model);

            var Claims = new List<Claim>
            {
                 new Claim(ClaimsIdentity.DefaultNameClaimType, model.Name),
                 new Claim(ClaimsIdentity.DefaultRoleClaimType, "person")
            };

            var claimsIdentity = new ClaimsIdentity(Claims, "Cookies");
            await HttpContext.SignInAsync("Cookies", new System.Security.Claims.ClaimsPrincipal(claimsIdentity));

            return RedirectToAction("Privacy", "Home");

            return View(model); // Вернуть представление для повторного ввода данных при неудачной аутентификации.
        }
        public async Task<IActionResult> Registr() => View();

        [HttpPost]
        public async Task<IActionResult> Registr(Person model)
        {
            personBuilder.Create(model);
            return RedirectToAction("Login");
        }

        [HttpPost]
        public IActionResult GetUser([FromBody] User name)
        {
            var user = personBuilder.Get(name);

            return Json(new { username = user.Name });
        }

        [HttpPost]
        public IActionResult Submit([FromBody] User data)
        {
            var user = personBuilder.Get(data);
            user.ChatId = data.Number;
            personBuilder.Edit(user);
            return Ok();
        }
        public IActionResult LogOut()
        {

            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");

        }
    }
}
