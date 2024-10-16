using API.SEG.Aplicacion.Features.Services;
using API.SEG.Aplicacion.Interface.Features;
using Microsoft.Extensions.DependencyInjection;

namespace API.SEG.Aplicacion.Features
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticateApplication, AuthenticateApplication>();
            services.AddScoped<IUsuarioApplication, UsuarioApplication>();
            services.AddScoped<ISecurityApplication, SecurityApplication>();
            services.AddScoped<ISessionService, SessionService>();
            services.AddScoped<IDataEncryptionService, DataEncryptionService>();

            return services;
        }
    }
}
