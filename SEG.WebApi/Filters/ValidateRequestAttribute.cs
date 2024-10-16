using API.SEG.Transversal.Common.Generics;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace SEG.WebApi.Filters
{
    public class ValidateRequestAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // Validar si la entrada es null
            foreach (var argument in context.ActionArguments)
            {
                if (argument.Value == null)
                {
                    // Retornar SegResponse con BadRequest si se encuentra un valor nulo
                    context.Result = new BadRequestObjectResult(
                        ResponseUtil.BadRequest(
                            "El cuerpo de la solicitud no puede ser null."
                        )
                    );
                    return;
                }
            }
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            // No se requiere lógica para después de la acción
        }
    }
}
