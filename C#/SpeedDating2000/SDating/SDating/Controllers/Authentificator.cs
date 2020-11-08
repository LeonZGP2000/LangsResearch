using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using SDating.Exceptions;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SDating.Controllers
{
    public interface IAuthentificator
    {
        void Authentificate(string login, string password, HttpContext context);
        Task LogOut(HttpContext context);
    }

    public class Authentificator : IAuthentificator
    {
        /// <summary>
        /// Authentification
        /// </summary>
        public void Authentificate(string login, string password, HttpContext context)
        {
            //new claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, login)
            };

            //check user login/password
            if (login.ToLower() != "admin" && password != "555787878")
                throw new AuthException("Incorrect login/password");

            //new idintity
            var id = new ClaimsIdentity(claims,
                "AplicationCookie",
                ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);

            //set auth cookie
            context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(id));
        }

        /// <summary>
        /// Log out
        /// </summary>
        public async Task LogOut(HttpContext context)
        {
            await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}
