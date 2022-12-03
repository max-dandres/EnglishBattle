using AutoMapper;
using AutoMapper.QueryableExtensions;
using EnglishBattle.BLL.Common;
using EnglishBattle.BLL.DTOs;
using EnglishBattle.DAL;
using EnglishBattle.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishBattle.BLL.Services
{
    public class VerbService
    {
        private readonly EnglishBattleContext _context;
        private readonly IConfigurationProvider _mappingConfig;

        public VerbService(EnglishBattleContext context, IMapper mapper)
        {
            _context = context;
            _mappingConfig = mapper.ConfigurationProvider;
        }

        public async Task<PaginatedList<IrregularVerbDto>> GetVerbsAsync(int page, int count, string? search = null, bool orderByDesc = false)
        {
            IQueryable<IrregularVerb> query = _context.IrregularVerbs.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(x => x.PastParticiple.Contains(search) || x.PastSimple.Contains(search) || x.BaseForm.Contains(search));
            }

            if (orderByDesc)
            {
                query = query.OrderByDescending(x => x.BaseForm);
            }
            else
            {
                query = query.OrderBy(x => x.BaseForm);
            }

            var dtos = await query
                .ProjectTo<IrregularVerbDto>(_mappingConfig)
                .ToPaginatedListAsync(page, count);

            return dtos;
        }

        public async Task<List<IrregularVerbDto>> GetAllVerbsAsync(bool shuffle = false)
        {
            var random = new Random();

            var dtos = await _context.IrregularVerbs
                .ProjectTo<IrregularVerbDto>(_mappingConfig)
                .ToListAsync();

            if (shuffle)
            {
                dtos = dtos.OrderBy(x => random.Next()).ToList();
            }

            return dtos;
        }
    }
}
