using EnglishBattle.BLL.Common;
using EnglishBattle.BLL.DTOs;
using EnglishBattle.BLL.Services;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EnglishBattle.Web.Pages
{
    public class IrregularVerbsModel : PageModel
    {
        [FromQuery]
        public string? Search { get; set; }
        [FromQuery]
        public bool OrderByDesc { get; set; }
        [FromQuery(Name = "index")]
        public int PageIndex { get; set; }
        public int Count { get; set; } = 25;
        public PaginatedList<IrregularVerbDto> Verbs { get; set; } = null!;

        private readonly VerbService _verbService;

        public string Culture { get; set; } = "en";

        public IrregularVerbsModel(VerbService verbService)
        {
            _verbService = verbService;
        }

        public async Task OnGet()
        {
            var rqf = Request.HttpContext.Features.Get<IRequestCultureFeature>();
            Culture = rqf?.RequestCulture.Culture.Name ?? "en";

            Verbs = await _verbService.GetVerbsAsync(PageIndex, Count, Search, OrderByDesc);
        }
    }
}
