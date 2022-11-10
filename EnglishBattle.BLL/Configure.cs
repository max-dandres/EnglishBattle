using EnglishBattle.BLL.Mappers;
using EnglishBattle.BLL.Services;
using Microsoft.Extensions.DependencyInjection;

namespace EnglishBattle.BLL
{
    public static class Configure
    {
        public static IServiceCollection AddBusinessLayer(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile));

            services.AddTransient<GameService>();
            services.AddTransient<PlayerService>();

            return services;
        }
    }
}