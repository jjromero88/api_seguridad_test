using API.SEG.Aplicacion.Dto;
using API.SEG.Aplicacion.Validator;
using FluentValidation;

namespace SEG.WebApi.Modules.Validator
{
    public static class ValidatorExtensions
    {
        public static IServiceCollection AddValidator(this IServiceCollection services)
        {
            services.AddTransient<IValidator<LoginRequest>, LoginRequestValidator>();
            services.AddTransient<IValidator<AuthorizeProfileRequest>, AuthorizeProfileRequestValidator>();
            services.AddTransient<AuthenticateValidationManager>();

            services.AddTransient<IValidator<UsuarioIdRequest>, UsuarioIdRequestValidator>();
            services.AddTransient<IValidator<UsuarioInsertRequest>, UsuarioInsertRequestValidator>();
            services.AddTransient<IValidator<UsuarioUpdateRequest>, UsuarioUpdateRequestValidator>();
            services.AddTransient<UsuarioValidationManager>();

            return services;
        }
    }
}
