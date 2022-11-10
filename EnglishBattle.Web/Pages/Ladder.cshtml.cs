using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EnglishBattle.Web.Pages
{
    [Authorize(Roles = "User")]
    public class LadderModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
