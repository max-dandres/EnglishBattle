using EnglishBattle.BLL.Services;
using EnglishBattle.Web.Resources;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace EnglishBattle.Web.Pages.Shared
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        [Display(Name = nameof(SharedResources.Username), ResourceType = typeof(SharedResources))]
        [Required(ErrorMessageResourceName = nameof(SharedResources.UsernameRequired), ErrorMessageResourceType = typeof(SharedResources))]
        public string UserName { get; set; } = "";
        [BindProperty]
        [Display(Name = nameof(SharedResources.Password), ResourceType = typeof(SharedResources))]
        [Required(ErrorMessageResourceName = nameof(SharedResources.PasswordRequired), ErrorMessageResourceType = typeof(SharedResources))]
        public string Password { get; set; } = "";
        [BindProperty]
        [Display(Name = nameof(Login.IsAnonymous), ResourceType = typeof(Login))]
        public bool IsAnonymous { get; set; }
        public string Error { get; set; } = "";

        private readonly PlayerService _playerService;

        public LoginModel(PlayerService playerService)
        {
            _playerService = playerService;
        }

        public void OnGet()
        {
            
        }

        public async Task<IActionResult> OnPost(string? returnUrl = null)
        {
            if (!IsAnonymous && !ModelState.IsValid)
            {
                return Page();
            }

            if (!IsAnonymous && !await _playerService.IsValidPassword(UserName, Password))
            {
                Error = Login.InvalidUsernameOrPassword;

                return Page();
            }

            string role = IsAnonymous ? "Anonymous" : "User";

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, IsAnonymous ? "Anonymous" : UserName),
                new Claim(ClaimTypes.Role, role),
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                //AllowRefresh = <bool>,
                // Refreshing the authentication session should be allowed.

                //ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                // The time at which the authentication ticket expires. A 
                // value set here overrides the ExpireTimeSpan option of 
                // CookieAuthenticationOptions set with AddCookie.

                //IsPersistent = true,
                // Whether the authentication session is persisted across 
                // multiple requests. When used with cookies, controls
                // whether the cookie's lifetime is absolute (matching the
                // lifetime of the authentication ticket) or session-based.

                //IssuedUtc = <DateTimeOffset>,
                // The time at which the authentication ticket was issued.

                //RedirectUri = <string>
                // The full path or absolute URI to be used as an http 
                // redirect response value.
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            return new RedirectToPageResult(returnUrl ?? "Index");
        }
    }
}
