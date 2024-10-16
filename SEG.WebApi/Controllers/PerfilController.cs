using AutoMapper;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using API.SEG.Aplicacion.Dto;
using API.SEG.Aplicacion.Interface.Features;
using API.SEG.Transversal.Common.Generics;
using API.SEG.Transversal.Common;
using SEG.WebApi.Filters;

namespace SEG.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PerfilController : Controller
    {
        private readonly IPerfilApplication _perfilApplication;
        private readonly IMapper _mapper;

        public PerfilController(IPerfilApplication perfilApplication, IMapper mapper)
        {
            _perfilApplication = perfilApplication;
            _mapper = mapper;
        }

        [HttpGet("GetList")]
        [ServiceFilter(typeof(AuthorizationRequestAttribute))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(SegResponse<List<PerfilResponse>>))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(SegResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(SegResponse))]
        [ProducesResponseType((int)HttpStatusCode.Forbidden, Type = typeof(SegResponse))]
        public async Task<ActionResult<SegResponse>> GetList([FromQuery] PerfilFilterRequest request)
        {
            return await _perfilApplication.GetList(new Request<PerfilDto>() { entidad = _mapper.Map<PerfilDto>(request) });
        }
    }
}
