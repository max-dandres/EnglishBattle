using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EnglishBattle.Web.Pages
{
    [Authorize(Roles = "User,Anonymous")]
    public class EnglishBattleModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
