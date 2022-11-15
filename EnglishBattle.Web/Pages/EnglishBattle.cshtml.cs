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
        public int GameID { get; set; }

        private readonly GameService _gameService;

        public EnglishBattleModel(GameService gameService)
        {
            _gameService = gameService;
        }

        public async Task OnGet()
        {
            if (!int.TryParse(User.FindFirstValue("UserID"), out int userId))
            {
                throw new Exception("Could not retrieve userID");
            }

            GameID = await _gameService.CreateGameAsync(userId);
            UserID = userId;

            IrregularVerbs = await _gameService.GetAllVerbsAsync(shuffle: true);

            return;
        }

        public async Task<IActionResult> OnPostAnswer(AnswerDto answer)
        {
            if (string.IsNullOrEmpty(answer.PastPrinciple) || string.IsNullOrEmpty(answer.Preterit))
            {
                return BadRequest("Answer cannot be empty");
            }

            bool isCorrect = await _gameService.CheckAnswerAsync(answer.VerbId, answer.Preterit, answer.PastPrinciple);

            await _gameService.AddAnswerAsync(answer);

            return new JsonResult(new { isCorrect });
        }

        public async Task<IActionResult> OnPutStart(int gameId, DateTime startedAt)
        {
            await _gameService.StartGameAsync(gameId, startedAt);

            return StatusCode(StatusCodes.Status202Accepted, new { statusCode = 202 });
        }
    }
}
