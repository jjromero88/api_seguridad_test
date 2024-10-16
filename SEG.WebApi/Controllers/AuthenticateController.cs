using API.SEG.Aplicacion.Dto;
using API.SEG.Aplicacion.Interface.Features;
using API.SEG.Transversal.Common.Generics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using System.Net;

namespace SEG.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : Controller
    {
        private readonly IAuthenticateApplication _authenticateApplication;
        private readonly IMapper _mapper;

        public AuthenticateController(IAuthenticateApplication authenticateApplication, IMapper mapper)
        {
            _authenticateApplication = authenticateApplication;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(SegResponse<LoginResponse>))]
        [ProducesResponseType((int)HttpStatusCode.UnprocessableEntity, Type = typeof(SegResponse<List<ErrorDetail>>))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(SegResponse))]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized, Type = typeof(SegResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(SegResponse))]
        [ProducesResponseType((int)HttpStatusCode.Forbidden, Type = typeof(SegResponse))]
        [ProducesResponseType((int)HttpStatusCode.Conflict, Type = typeof(SegResponse))]
        public async Task<ActionResult<SegResponse>> Login([FromBody] LoginRequest request)
        {
            return await _authenticateApplication.Login(_mapper.Map<AuthenticateDto>(request));
        }

        [AllowAnonymous]
        [HttpPost("AuthorizeProfile")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(SegResponse<AuthorizeProfileResponse>))]
        [ProducesResponseType((int)HttpStatusCode.UnprocessableEntity, Type = typeof(SegResponse<List<ErrorDetail>>))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(SegResponse))]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized, Type = typeof(SegResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(SegResponse))]
        [ProducesResponseType((int)HttpStatusCode.Forbidden, Type = typeof(SegResponse))]
        [ProducesResponseType((int)HttpStatusCode.Conflict, Type = typeof(SegResponse))]
        public async Task<ActionResult<SegResponse>> AuthorizeProfile([FromBody] AuthorizeProfileRequest request)
        {
            return await _authenticateApplication.AuthorizeProfile(_mapper.Map<AuthenticateDto>(request));
        }

    }
}
