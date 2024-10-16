using API.SEG.Aplicacion.Dto;
using API.SEG.Aplicacion.Interface.Features;
using API.SEG.Domain.Entities;
using API.SEG.Transversal.Common.Generics;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SEG.WebApi.Filters;
using System.Net;

namespace SEG.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityController : Controller
    {
        private readonly ISecurityApplication _securityApplication;
        private readonly IMapper _mapper;

        public SecurityController(ISecurityApplication securityApplication, IMapper mapper)
        {
            _securityApplication = securityApplication;
            _mapper = mapper;
        }

        [HttpGet("GetSessionData")]
        [ServiceFilter(typeof(ValidateTokenRequestAttribute))]
        [ServiceFilter(typeof(UpdateTokenRequestAttribute))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(SegResponse<UsuarioCacheResponse>))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(SegResponse))]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized, Type = typeof(SegResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(SegResponse))]
        [ProducesResponseType((int)HttpStatusCode.Forbidden, Type = typeof(SegResponse))]
        public async Task<ActionResult<SegResponse>> GetSessionData()
        {
            return await _securityApplication.GetSessionData();
        }

        [HttpGet("GetAccesos")]
        [ServiceFilter(typeof(ValidateTokenRequestAttribute))]
        [ServiceFilter(typeof(UpdateTokenRequestAttribute))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(SegResponse<AccesosResponse>))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(SegResponse))]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized, Type = typeof(SegResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(SegResponse))]
        [ProducesResponseType((int)HttpStatusCode.Forbidden, Type = typeof(SegResponse))]
        public async Task<ActionResult<SegResponse>> GetAccesos()
        {
            return await _securityApplication.GetAccesos();
        }

        [HttpGet("GetAccesoPermisos")]
        [ServiceFilter(typeof(ValidateTokenRequestAttribute))]
        [ServiceFilter(typeof(UpdateTokenRequestAttribute))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(SegResponse<string[]>))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(SegResponse))]
        [ProducesResponseType((int)HttpStatusCode.NoContent, Type = typeof(SegResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(SegResponse))]
        public async Task<ActionResult<SegResponse>> GetPermisos([FromQuery] AccesoPermisosRequest request)
        {
            return await _securityApplication.GetAccesoPermisos(_mapper.Map<AccesosDto>(request));
        }

    }
}
