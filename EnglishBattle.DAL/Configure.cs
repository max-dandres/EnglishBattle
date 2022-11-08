using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EnglishBattle.DAL
{
    public static class Configure
    {
        public static IServiceCollection AddDataAccessLayer(this IServiceCollection services)
        {
            services.AddDbContext<EnglishBattleContext>(options =>
            {
                options.UseInMemoryDatabase("EnglishBattle");
            });

            return services;
        }
    }
}
