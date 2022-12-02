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

        public async Task<PaginatedList<GameDto>> GetAllGamesAsync(int pageIndex, int pageSize)
        {
            var dtos = new List<GameDto>();

            var query = _context.Games
                    .Include(x => x.Player);

            var games = await query
                .ProjectTo<GameDto>(_mappingConfig)
                .OrderByDescending(x => x.Score)
                .ToPaginatedListAsync(pageIndex, pageSize);

            return games;
        }

        public async Task<List<IrregularVerbDto>> GetAllVerbsAsync(bool shuffle = false)
        {
            var dtos = new List<IrregularVerbDto>();
            var verbs = await _context.IrregularVerbs.ToListAsync();

            foreach (var verb in verbs)
            {
                dtos.Add(new IrregularVerbDto(verb.Id, verb.BaseForm, verb.PastParticiple, verb.PastSimple));
            }

            if (shuffle)
            {
                var random = new Random();

                return dtos.OrderBy(x => random.Next()).ToList();
            }

            return dtos;
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
