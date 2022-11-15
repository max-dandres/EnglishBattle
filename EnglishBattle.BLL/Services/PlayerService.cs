using AutoMapper;
using EnglishBattle.BLL.DTOs;
using EnglishBattle.DAL;
using EnglishBattle.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace EnglishBattle.BLL.Services
{
    public class PlayerService
    {
        private readonly EnglishBattleContext _context;
        private readonly IMapper _mapper;

        public PlayerService(EnglishBattleContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task CreateAsync(PlayerDto playerDto)
        {
            Player player = _mapper.Map<Player>(playerDto);

            _context.Add(player);
            await _context.SaveChangesAsync();
        }

        public async Task<PlayerDto?> GetPlayerAsync(string userName, string password)
        {
            Player? player = await _context.Players.Where(x => x.UserName == userName).FirstOrDefaultAsync();

            if (player is null)
            {
                return null;
            }

            if (player.Password != password)
            {
                return null;
            }

            return new PlayerDto(player.Id, player.UserName, string.Empty);
        }

        public async Task<bool> ExistAsync(string username)
        {
            return await _context.Players.AnyAsync(x => x.UserName == username);
        }
    }
}
