using EnglishBattle.BLL.Services;
using Microsoft.Extensions.DependencyInjection;

namespace EnglishBattle.BLL
{
    public static class Configure
    {
        public static IServiceCollection AddBusinessLayer(this IServiceCollection services)
        {
            services.AddTransient<GameService>();

            return services;
        }
    }
}