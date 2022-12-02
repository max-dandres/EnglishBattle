using EnglishBattle.BLL.Common;
using EnglishBattle.BLL.DTOs;
using EnglishBattle.BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace EnglishBattle.Web.Pages
{
    [Authorize(Roles = "User")]
    public class LadderModel : PageModel
    {
        [FromQuery]
        public string? Search { get; set; }
        [FromQuery]
        public bool ShowMeOnly { get; set; }
        [FromQuery]
        public DateTime? From { get; set; }
        [FromQuery]
        public DateTime? To { get; set; }
        [FromQuery(Name = "index")]
        public int PageIndex { get; set; }

        public PaginatedList<GameDto> Games { get; private set; } = null!;

        private readonly GameService _gameService;

        public LadderModel(GameService gameService)
        {
            _gameService = gameService;
        }

        public async Task OnGet()
        {
            int count = 5;
            int? playerId = null;

            if (ShowMeOnly)
            {
                if (!int.TryParse(User.FindFirstValue("UserID"), out int userId))
                {
                    throw new Exception("Could not retrieve userID");
                }

                playerId = userId;
            }
            if (From != null && To != null)
            {
                if (From > To)
                {
                    throw new Exception("Invalid date range");
                }
            }

            Games = await _gameService.GetAllGamesAsync(PageIndex, count, playerId, Search, From, To);
        }
    }
}
