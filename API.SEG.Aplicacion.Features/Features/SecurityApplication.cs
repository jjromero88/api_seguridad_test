using API.SEG.Aplicacion.Dto;
using API.SEG.Aplicacion.Features.Util;
using API.SEG.Aplicacion.Interface.Features;
using API.SEG.Aplicacion.Interface.Infraestructure;
using API.SEG.Domain.Entities;
using API.SEG.Transversal.Common;
using API.SEG.Transversal.Common.Constants;
using API.SEG.Transversal.Common.Generics;
using AutoMapper;

namespace API.SEG.Aplicacion.Features
{
    public class SecurityApplication : ISecurityApplication
    {
        private readonly IMapper _mapper;
        private readonly IAppLogger<UsuarioApplication> _logger;
        private readonly IRedisCacheService _redisCacheService;
        private readonly ISessionService _sessionService;
        private readonly ITokenService _tokenService;

        public SecurityApplication(IMapper mapper,
                                   IAppLogger<UsuarioApplication> logger, 
                                   IRedisCacheService redisCacheService, 
                                   ISessionService sessionService,
                                   ITokenService tokenService)
        {
            _mapper = mapper;
            _logger = logger;
            _redisCacheService = redisCacheService;
            _sessionService = sessionService;
            _tokenService = tokenService;
        }

        public async Task<SegResponse> GetAccesoPermisos(AccesosDto request)
        {
            try
            {
                string url_path = request.url_opcion ?? string.Empty;

                if (string.IsNullOrEmpty(url_path))
                    return ResponseUtil.BadRequest("Debe ingresar el nombre o descripción del path del url");

                // obtenemos el token
                string token = _sessionService.GetToken();

                //verificamos quew el toekn llegue correctamente
                if (string.IsNullOrEmpty(token))
                    return ResponseUtil.BadRequest("No se encontró un valor en el Token para la petición");

                // obtenemos el idSession del Token
                string idsession = await _tokenService.GetIdSessionFromToken(token);

                //buscamos en redis los datos de la sesion en redis
                var usuarioCache = await _redisCacheService.GetAsync<UsuarioCache>(idsession);

                //verificamos si retorna informacion de la caché
                if (usuarioCache == null)
                    return ResponseUtil.BadRequest("Su sesión ha expirado, vuelva a intentarlo");

                // obtenemos los accesos del usuariocache
                List<Accesos> lista_accesos = usuarioCache.usuario_accesos ?? new List<Accesos>();

                // obtenemos el acceso que coincide con el path del url
                var acceso = UtilSecurity.BuscarAccesoPorUrl(lista_accesos, url_path);

                string[] codigosPermisos = acceso.lista_permisos? 
                                                .Where(p => p.permiso_codigo != null)
                                                .Select(p => p.permiso_codigo)
                                                .ToArray();

                //retornamos la informacion
                return codigosPermisos != null ? ResponseUtil.Ok(
                    codigosPermisos, TransactionMessage.QuerySuccess) : ResponseUtil.NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return ResponseUtil.InternalError(ex.Message);
            }
        }

        public async Task<SegResponse> GetAccesos()
        {
            try
            {
                // obtenemos el token
                string token = _sessionService.GetToken();

                //verificamos quew el toekn llegue correctamente
                if (string.IsNullOrEmpty(token))
                    return ResponseUtil.BadRequest("No se encontró un valor en el Token para la petición");

                // obtenemos el idSession del Token
                string idsession = await _tokenService.GetIdSessionFromToken(token);

                //verificamos que el token 
                if (string.IsNullOrEmpty(idsession))
                    return ResponseUtil.Forbidden("Ocurrió un error inesperado, no se logró identificar la sesión del token solicitado");

                //buscamos en redis los datos de la sesion en redis
                var usuarioCache = await _redisCacheService.GetAsync<UsuarioCache>(idsession);

                //verificamos si retorna informacion de la caché
                if (usuarioCache == null)
                    return ResponseUtil.BadRequest("Su sesión ha expirado, vuelva a intentarlo");

                // obtenemos los accesos del usuariocache
                var accesos = usuarioCache.usuario_accesos;

                // mapeamos el response de accesos
                var accesosResponse = _mapper.Map<List<AccesosResponse>>(accesos);

                //retornamos la informacion
                return accesosResponse != null ? ResponseUtil.Ok(
                    accesosResponse, TransactionMessage.QuerySuccess) : ResponseUtil.Unauthorized();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return ResponseUtil.InternalError(ex.Message);
            }
        }

        public async Task<SegResponse> GetSessionData()
        {
            try
            {
                // obtenemos el token
                string token = _sessionService.GetToken();

                //verificamos quew el toekn llegue correctamente
                if (string.IsNullOrEmpty(token))
                    return ResponseUtil.BadRequest("No se encontró un valor en el Token para la petición");

                // obtenemos el idSession del Token
                string idsession = await _tokenService.GetIdSessionFromToken(token);

                //verificamos que el token 
                if (string.IsNullOrEmpty(idsession))
                    return ResponseUtil.Forbidden("Ocurrió un error inesperado, no se logró identificar la sesión del token solicitado");

                //buscamos en redis los datos de la sesion en redis
                var usuarioCache = await _redisCacheService.GetAsync<UsuarioCache>(idsession);

                //verificamos si retorna informacion de la caché
                if (usuarioCache == null)
                    return ResponseUtil.BadRequest("Su sesión ha expirado, vuelva a intentarlo");

                var cacheResponse = _mapper.Map<UsuarioCacheResponse>(usuarioCache);

                //retornamos la informacion
                return cacheResponse != null ? ResponseUtil.Ok(
                    cacheResponse, TransactionMessage.QuerySuccess) : ResponseUtil.Unauthorized();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return ResponseUtil.InternalError(ex.Message);
            }
        }
    }
}
