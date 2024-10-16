using API.SEG.Aplicacion.Interface.Features;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SEG.WebApi.Filters
{
    public class UpdateTokenRequestAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            try
            {
                string authorization = context.HttpContext.Request.Headers["Authorization"];

                if (string.IsNullOrEmpty(authorization))
                    throw new ArgumentException("El token no puede estar vacío");

                var token = authorization.Substring("Bearer ".Length).Trim();

                // Obtener el servicio desde el proveedor de servicios
                var serviceProvider = context.HttpContext.RequestServices;

                // obtenemos el servicio que controla las variables de sesion
                var sessionService = serviceProvider.GetRequiredService<ISessionService>();

                // actualizamos el token
                sessionService.SetToken(authorization);
            }
            catch (Exception ex)
            {
                throw new Exception($"Ocurrio un error al intentar leer el token: {ex.Message}", ex);
            }
        }
    }
}
