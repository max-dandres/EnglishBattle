using EnglishBattle.BLL.DTOs;
using EnglishBattle.BLL.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EnglishBattle.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly GameService _gameService;
        private readonly ILogger<IndexModel> _logger;

        public IEnumerable<IrregularVerbDto> Verbs { get; set; } = null!;

        public IndexModel(GameService gameService, ILogger<IndexModel> logger)
        {
            _gameService = gameService;
            _logger = logger;
        }

        public async void OnGet()
        {
            Verbs = await _gameService.GetAllVerbs();
        }

        public IActionResult OnPostSetCulture(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return new RedirectToPageResult(returnUrl);
        }

        public async Task<IActionResult> OnGetLogout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return new RedirectToPageResult("Index");
        }
    }
}