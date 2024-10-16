using API.SEG.Aplicacion.Interface.Infraestructure;
using API.SEG.Infraestructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace API.SEG.Infraestructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfraestructureServices(this IServiceCollection services)
        {
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IRedisCacheService, RedisCacheService>();
            services.AddScoped<IEncryptionService, EncryptionService>();

            return services;
        }
    }
}
