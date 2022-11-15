using EnglishBattle.BLL.DTOs;
using EnglishBattle.DAL;
using EnglishBattle.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishBattle.BLL.Services
{
    public class GameService
    {
        private readonly EnglishBattleContext _context;

        public GameService(EnglishBattleContext context)
        {
            _context = context;
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

        public async Task AddAnswerAsync(AnswerDto answerDto)
        {
            var answer = new GameAnswer(answerDto.GameId, answerDto.VerbId, answerDto.Preterit, answerDto.PastPrinciple, answerDto.AnsweredAt);

            _context.GameAnswers.Add(answer);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> CheckAnswerAsync(int verbId, string preterit, string pastPrinciple)
        {
            var verb = await _context.IrregularVerbs.FirstOrDefaultAsync(x => x.Id == verbId);

            if (verb == null)
            {
                throw new Exception($"Verb with id {verbId} not found");
            }

            if (string.Compare(verb.PastSimple, preterit, true) == 0)
            {
                return false;
            }
            else if (string.Compare(verb.PastParticiple, pastPrinciple, true) == 0)
            {
                return false;
            }

            return true;
        }

        public async Task<int> CreateGameAsync(int userId)
        {
            var newGame = new Game(userId);

            _context.Games.Add(newGame);
            await _context.SaveChangesAsync();

            return newGame.Id;
        }

        public async Task StartGameAsync(int gameId, DateTime timestamp)
        {
            var game = _context.Games.FirstOrDefault(x => x.Id == gameId);

            if (game == null)
            {
                throw new Exception($"Game with id {gameId} not found");
            }

            game.StartedAt = timestamp;

            await _context.SaveChangesAsync();
        }
    }
}
