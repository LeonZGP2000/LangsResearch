using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SDating.Exceptions;
using System;

namespace SDating.Controllers
{
    public class AuthController : Controller
    {
        public IAuthentificator auth { get; set; }

        public AuthController()
        {
            auth = new Authentificator();
        }

        [AllowAnonymous]
        public IActionResult Index(string errorMesage = null)
        {
            if (!String.IsNullOrEmpty(errorMesage))
                ViewData["AuthError"] = errorMesage;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string login, string password)
        {
            //Auth
            if (!User.Identity.IsAuthenticated)
            {
                try
                {
                    auth.Authentificate(login, password, HttpContext);
                }
                catch(AuthException e)
                {                    
                    return RedirectToAction("index", "auth", new { errorMesage= e.Message });
                }
            }

            //redirect to dating
            return RedirectToAction("index", "dating");
        }


        [HttpGet]
        public IActionResult Logout()
        {
            auth.LogOut(HttpContext).ConfigureAwait(false);

            //redirect to dating
            return RedirectToAction("index", "dating");
        }
    }
}
