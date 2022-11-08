using EnglishBattle.BLL.DTOs;
using EnglishBattle.DAL;
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

        public async Task<List<IrregularVerbDto>> GetAllVerbs()
        {
            var dtos = new List<IrregularVerbDto>();
            var verbs = await _context.IrregularVerbs.ToListAsync();

            foreach (var verb in verbs)
            {
                dtos.Add(new IrregularVerbDto(verb.BaseForm, verb.PastPrinciple, verb.Preterit));
            }

            return dtos;
        }
    }
}
