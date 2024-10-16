using API.SEG.Aplicacion.Interface.Persistence;
using API.SEG.Aplicacion.Interface;
using API.SEG.Persistence.Context;
using API.SEG.Persistence.Repository.Base;
using API.SEG.Persistence.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace API.SEG.Persistence
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services)
        {
            services.AddSingleton<DapperContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAuthenticateRepository, AuthenticateRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IPerfilRepository, PerfilRepository>();

            return services;
        }
    }
}
