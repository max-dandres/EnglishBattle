using AutoMapper;
using AutoMapper.QueryableExtensions;
using EnglishBattle.BLL.Common;
using EnglishBattle.BLL.DTOs;
using EnglishBattle.DAL;
using EnglishBattle.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace EnglishBattle.BLL.Services
{
    public class GameService
    {
        private readonly EnglishBattleContext _context;
        private readonly IConfigurationProvider _mappingConfig;

        public GameService(EnglishBattleContext context, IMapper mapper)
        {
            _context = context;
            _mappingConfig = mapper.ConfigurationProvider;
        }

        public async Task<PaginatedList<GameDto>> GetAllGamesAsync(int pageIndex, int pageSize, int? playerId, string? search, DateTime? from, DateTime? to)
        {
            var dtos = new List<GameDto>();

            var query = _context.Games.AsQueryable();

            if (playerId is not null)
            {
                query = query.Where(x => x.PlayerId == playerId);
            }
            else if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(x => x.Player.UserName.ToLower().Contains(search.ToLower()));
            }

            if (from is not null)
            {
                query = query.Where(x => x.CreatedAt >= from);
            }
            if (to is not null)
            {
                query = query.Where(x => x.CreatedAt <= to);
            }

            var games = await query
                .Include(x => x.Player)
                .ProjectTo<GameDto>(_mappingConfig)
                .OrderByDescending(x => x.Score)
                .ToPaginatedListAsync(pageIndex, pageSize);

            return games;
        }

        public async Task<bool> AddAnswerAsync(AnswerDto answerDto)
        {
            bool isCorrect = await CheckAnswerAsync(answerDto.VerbId, answerDto.PastParticiple, answerDto.PastSimple);

            var answer = new GameAnswer(
                answerDto.GameId,
                answerDto.VerbId,
                answerDto.PastParticiple,
                answerDto.PastSimple,
                isCorrect,
                answerDto.AnsweredAt
            );

            _context.GameAnswers.Add(answer);
            await _context.SaveChangesAsync();

            return isCorrect;
        }

        public async Task<bool> CheckAnswerAsync(int verbId, string pastParticiple, string pastSimple)
        {
            var verb = await _context.IrregularVerbs.FirstOrDefaultAsync(x => x.Id == verbId);

            if (verb == null)
            {
                throw new Exception($"Verb with id {verbId} not found");
            }

            
            if (string.Compare(verb.PastParticiple, pastParticiple, true) != 0)
            {
                return false;
            }
            else if (string.Compare(verb.PastSimple, pastSimple, true) != 0)
            {
                return false;
            }

            return true;
        }

        public async Task<int> NewGameAsync(int userId)
        {
            var newGame = new Game(userId);

            _context.Games.Add(newGame);
            await _context.SaveChangesAsync();

            return newGame.Id;
        }

        public async Task GameOverAsync(int gameId, DateTime overAt)
        {
            var game = _context.Games.Include(x => x.Answers).FirstOrDefault(x => x.Id == gameId);

            if (game == null)
            {
                throw new Exception($"Game with id {gameId} not found");
            }

            game.OverAt = overAt;
            game.Duration = 60 + game.Answers.Sum(x => x.TimePenalty);
            game.Score = game.Answers.Count(x => x.IsCorrect);

            await _context.SaveChangesAsync();
        }
    }
}
