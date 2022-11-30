using EnglishBattle.BLL.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EnglishBattle.Web.Pages
{
    public class GameDetailsModel : PageModel
    {
        private readonly GameService _gameService;
        public GameDetailsModel(GameService gameService)
        {
            _gameService = gameService;
        }
        public void OnGet(int id)
        {
            
        }
    }
}
