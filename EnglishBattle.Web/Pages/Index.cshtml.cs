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
        public List<IEnumerable<IrregularVerbDto>> Verbs { get; private set; } = new();

        private readonly GameService _gameService;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(GameService gameService, ILogger<IndexModel> logger)
        {
            _gameService = gameService;
            _logger = logger;
        }

        public async void OnGet()
        {
            var verbs = await _gameService.GetAllVerbsAsync();

            List<IrregularVerbDto> easyVerbs = new();
            List<IrregularVerbDto> mediumVerbs = new();
            List<IrregularVerbDto> hardVerbs = new();

            foreach (var verb in verbs)
            {
                if (verb.BaseForm == verb.PastPrinciple && verb.PastPrinciple == verb.Preterit)
                {
                    easyVerbs.Add(verb);
                }
                else if (verb.BaseForm == verb.PastPrinciple || verb.PastPrinciple == verb.Preterit)
                {
                    mediumVerbs.Add(verb);
                }
                else
                {
                    hardVerbs.Add(verb);
                }
            }

            Verbs.Add(easyVerbs);
            Verbs.Add(mediumVerbs);
            Verbs.Add(hardVerbs);
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