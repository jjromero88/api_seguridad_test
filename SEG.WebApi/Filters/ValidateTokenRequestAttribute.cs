using Microsoft.AspNetCore.Mvc.Filters;

namespace SEG.WebApi.Filters
{
    public class ValidateTokenRequestAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            try
            {
                string authorization = context.HttpContext.Request.Headers["Authorization"];

                // If no authorization header found, nothing to process further
                if (string.IsNullOrEmpty(authorization))
                {
                    throw new ArgumentException("El token no puede estar vacío");
                }

                string token;
                // If no content Bearer in Authorization Header
                if (!authorization.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                {
                    throw new Exception("El token no tiene el formato correcto");
                }
                else
                    token = authorization.Substring("Bearer ".Length).Trim();

                // If no token found, no further work possible
                if (string.IsNullOrEmpty(token))
                {
                    throw new Exception("El token no puede estar vacío");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Ocurrio un error al intentar leer el token: {ex.Message}", ex);
            }
        }

    }

}
