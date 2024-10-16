using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.SEG.Transversal.Common.Constants
{
    public enum HttpStatusCodeEnum
    {
        OK = 200,
        Created = 201,
        NoContent = 204,
        BadRequest = 400,
        Unauthorized = 401,
        NotFound = 404,
        Conflict = 409,
        ValidationError = 422,
        InternalServerError = 500,
    }
}
