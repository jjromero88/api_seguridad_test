using API.SEG.Transversal.Common.Constants;
using FluentValidation.Results;
using System.Net;

namespace API.SEG.Transversal.Common.Generics
{
    public class ResponseUtil
    {
        public static SegResponse Created(string? message = null)
        {
            return new SegResponse { code = (int)HttpStatusCode.Created, message = message };
        }

        public static SegResponse InternalError(string message)
        {
            return new SegResponse
            {
                code = (int)HttpStatusCode.InternalServerError,
                message = "Ocurrió un error mientras se procesaba la operación, inténtelo nuevamente. Si el problema persiste, contáctese con soporte.",
                errordetails = message,
                error = true
            };
        }

        public static SegResponse NoContent()
        {
            return new SegResponse { code = (int)HttpStatusCode.NoContent, message = MessageConstant.NoContentForRequest };
        }

        public static SegResponse Unauthorized()
        {
            return new SegResponse { code = (int)HttpStatusCode.Unauthorized, message = MessageConstant.Unauthorized, error = true };
        }

        public static SegResponse<T> Ok<T>(T payload, string? message = null)
        {
            return new SegResponse<T> { code = (int)HttpStatusCode.OK, message = message, payload = payload };
        }

        public static SegResponse<List<ErrorDetail>> UnprocessableEntity(IEnumerable<ValidationFailure> validationErrors, string? message = null)
        {
            var errorList = validationErrors?.Select(e => new ErrorDetail
            {
                field = e.PropertyName,
                message = e.ErrorMessage,
                code = e.ErrorCode
            }).ToList();

            return new SegResponse<List<ErrorDetail>>
            {
                code = (int)HttpStatusCode.UnprocessableEntity,
                message = message ?? MessageConstant.BadRequest,
                payload = errorList,
                error = true
            };
        }

        public static SegResponse BadRequest(string? message)
        {
            return new SegResponse
            {
                code = (int)HttpStatusCode.BadRequest,
                message = message ?? MessageConstant.BadRequest,
                error = true
            };
        }

        public static SegResponse Forbidden(string? message)
        {
            return new SegResponse
            {
                code = (int)HttpStatusCode.Forbidden,
                message = message ?? "No pudo completarse la operación.",
                error = true
            };
        }

            // para errores controlados de base de datos
            public static SegResponse Conflict(string? message)
        {
            return new SegResponse
            {
                code = (int)HttpStatusCode.Conflict,
                message = message ?? MessageConstant.BadRequest,
                error = true
            };
        }

        //errores de servicio externo
        public static SegResponse<T> ServiceUnavailable<T>(T payload, string? message = null)
        {
            return new SegResponse<T>
            {
                code = (int)HttpStatusCode.ServiceUnavailable,
                message = message ?? MessageConstant.BadRequest,
                payload = payload,
                error = true
            };
        }

        public static SegResponse<T> GatewayTimeout<T>(T payload, string? message = null)
        {
            return new SegResponse<T>
            {
                code = (int)HttpStatusCode.GatewayTimeout,
                message = message ?? MessageConstant.BadRequest,
                payload = payload,
                error = true
            };
        }
    }
}
