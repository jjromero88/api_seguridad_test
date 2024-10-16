using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using API.SEG.Aplicacion.Interface.Features;
using API.SEG.Aplicacion.Dto;
using API.SEG.Transversal.Common;
using SEG.WebApi.Filters;
using API.SEG.Transversal.Common.Generics;

namespace SEG.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioApplication _usuarioApplication;
        private readonly IMapper _mapper;

        public UsuarioController(IUsuarioApplication usuarioApplication, IMapper mapper)
        {
            _usuarioApplication = usuarioApplication;
            _mapper = mapper;
        }

        [HttpGet("GetById")]
        [ServiceFilter(typeof(AuthorizationRequestAttribute))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(SegResponse<UsuarioResponse>))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(SegResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(SegResponse))]
        [ProducesResponseType((int)HttpStatusCode.Forbidden, Type = typeof(SegResponse))]
        public async Task<ActionResult<SegResponse>> GetById([FromQuery] UsuarioIdRequest request)
        {
            return await _usuarioApplication.GetById(new Request<UsuarioDto>() { entidad = _mapper.Map<UsuarioDto>(request) });
        }

        [HttpGet("GetList")]
        [ServiceFilter(typeof(AuthorizationRequestAttribute))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(SegResponse<List<UsuarioResponse>>))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(SegResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(SegResponse))]
        [ProducesResponseType((int)HttpStatusCode.Forbidden, Type = typeof(SegResponse))]
        public async Task<ActionResult<SegResponse>> GetList([FromQuery] UsuarioFilterRequest request)
        {
            return await _usuarioApplication.GetList(new Request<UsuarioDto>() { entidad = _mapper.Map<UsuarioDto>(request) });
        }

        [HttpPost("Insert")]
        [ServiceFilter(typeof(AuthorizationRequestAttribute))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(SegResponse))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(SegResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(SegResponse))]
        [ProducesResponseType((int)HttpStatusCode.Forbidden, Type = typeof(SegResponse))]
        public async Task<ActionResult<SegResponse>> Insert([FromBody] UsuarioInsertRequest request)
        {
            return await _usuarioApplication.Insert(new Request<UsuarioDto>() { entidad = _mapper.Map<UsuarioDto>(request) });
        }

        [HttpPut("Update")]
        [ServiceFilter(typeof(AuthorizationRequestAttribute))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(SegResponse))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(SegResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(SegResponse))]
        [ProducesResponseType((int)HttpStatusCode.Forbidden, Type = typeof(SegResponse))]
        public async Task<ActionResult<SegResponse>> Update([FromBody] UsuarioUpdateRequest request)
        {
            return await _usuarioApplication.Update(new Request<UsuarioDto>() { entidad = _mapper.Map<UsuarioDto>(request) });
        }

        [HttpDelete("Delete")]
        [ServiceFilter(typeof(AuthorizationRequestAttribute))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(SegResponse))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(SegResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(SegResponse))]
        [ProducesResponseType((int)HttpStatusCode.Forbidden, Type = typeof(SegResponse))]
        public async Task<ActionResult<SegResponse>> DeleteObject([FromBody] UsuarioIdRequest request)
        {
            return await _usuarioApplication.Delete(new Request<UsuarioDto>() { entidad = _mapper.Map<UsuarioDto>(request) });
        }
    }
}
