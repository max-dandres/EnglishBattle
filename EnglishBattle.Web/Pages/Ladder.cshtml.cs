using EnglishBattle.BLL.DTOs;
using EnglishBattle.BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EnglishBattle.Web.Pages
{
    [Authorize(Roles = "User")]
    public class LadderModel : PageModel
    {
        private readonly GameService _gameService;
        public List<GameDto> Games { get; private set; } = null!;

        public LadderModel(GameService gameService)
        {
            _gameService = gameService;
        }

        public async Task OnGet(int? page, string? search)
        {
            int count = 25;

            Games = await _gameService.GetAllGamesAsync(page ?? 0, count);
        }
    }
}
