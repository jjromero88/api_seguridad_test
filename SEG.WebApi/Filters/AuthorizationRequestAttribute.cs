using API.SEG.Aplicacion.Interface.Features;
using API.SEG.Aplicacion.Interface.Infraestructure;
using API.SEG.Domain.Entities;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SEG.WebApi.Filters
{
    public class AuthorizationRequestAttribute : ActionFilterAttribute
    {
        private readonly ITokenService _tokenService;
        private readonly IRedisCacheService _redisCacheService;

        public AuthorizationRequestAttribute(ITokenService tokenService, IRedisCacheService redisCacheService)
        {
            _tokenService = tokenService;
            _redisCacheService = redisCacheService;
        }

        public override async void OnActionExecuting(ActionExecutingContext context)

        {
            try
            {
                string authorization = context.HttpContext.Request.Headers["Authorization"];

                // If no authorization header found, nothing to process further
                if (string.IsNullOrEmpty(authorization))
                {
                    throw new ArgumentException("El token no puede estar vacío");
                }

                // If no content Bearer in Authorization Header
                if (!authorization.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                {
                    throw new Exception("El token no tiene el formato correcto");
                }

                // obtenemos el idSession del Token
                string idsession = await _tokenService.GetIdSessionFromToken(authorization);

                //buscamos en redis los datos de la sesion en redis
                var usuarioCache = await _redisCacheService.GetAsync<UsuarioCache>(idsession);

                // verificamos que el cast haya sido correcto
                if (usuarioCache == null)
                {
                    throw new ArgumentException("Su sesion ha expirado");
                }

                // Obtener el servicio desde el proveedor de servicios
                var serviceProvider = context.HttpContext.RequestServices;

                // obtenemos el servicio que controla las variables de sesion
                var sessionService = serviceProvider.GetRequiredService<ISessionService>();

                // actualizamos la data del usuario de la sesión
                sessionService.updateUsuarioCache(usuarioCache);

                // actualizamos el token
                sessionService.SetToken(authorization);

            }
            catch (Exception ex)
            {
                throw new Exception($"Error inesperado: {ex.Message}", ex);
            }
        }

    }
}
