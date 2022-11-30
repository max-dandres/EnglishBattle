using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace EnglishBattle.DAL
{
    internal class EnglishBattleContextDesignTimeFactory : IDesignTimeDbContextFactory<EnglishBattleContext>
    {
        public EnglishBattleContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<EnglishBattleContext>();

            optionsBuilder.UseSqlite($"Data Source={EnglishBattleContext.GetLocalDbPath()}");

            return new EnglishBattleContext(optionsBuilder.Options);
        }
    }
}
