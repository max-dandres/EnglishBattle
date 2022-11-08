using EnglishBattle.BLL.DTOs;
using EnglishBattle.BLL.Services;
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
    }
}