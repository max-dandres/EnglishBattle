using EnglishBattle.BLL.DTOs;
using EnglishBattle.BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace EnglishBattle.Web.Pages
{
    [Authorize(Roles = "User,Anonymous")]
    public class EnglishBattleModel : PageModel
    {
        public List<IrregularVerbDto> IrregularVerbs { get; set; } = null!;
        public int UserID { get; set; }
        //public int GameID { get; set; }

        private readonly GameService _gameService;

        public EnglishBattleModel(GameService gameService)
        {
            _gameService = gameService;
        }

        public void OnGet()
        {
            if (!int.TryParse(User.FindFirstValue("UserID"), out int userId))
            {
                throw new Exception("Could not retrieve userID");
            }

            UserID = userId;

            return;
        }

        public async Task<IActionResult> OnGetIrregularVerbs()
        {
            var irregularVerbs = await _gameService.GetAllVerbsAsync(shuffle: true);

            return new JsonResult(new { irregularVerbs });
        }

        public async Task<IActionResult> OnPostAnswer(AnswerDto answer)
        {
            if (string.IsNullOrEmpty(answer.PastParticiple) || string.IsNullOrEmpty(answer.PastSimple))
            {
                return BadRequest("Answer cannot be empty");
            }

            bool isCorrect = await _gameService.AddAnswerAsync(answer);

            return new JsonResult(new { isCorrect });
        }

        public async Task<IActionResult> OnPostNewGame(int playerId)
        {
            int gameId = await _gameService.NewGameAsync(playerId);

            return new JsonResult(new { gameId });
        }

        public async Task<IActionResult> OnPostGameOver(int gameId, DateTime overAt)
        {
            await _gameService.GameOverAsync(gameId, overAt);

            return StatusCode(StatusCodes.Status202Accepted, new { statusCode = 202 });
        }
    }
}
