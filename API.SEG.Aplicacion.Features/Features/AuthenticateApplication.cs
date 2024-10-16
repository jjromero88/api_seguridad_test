using AutoMapper;
using System.Text.Json;
using API.SEG.Aplicacion.Dto;
using API.SEG.Aplicacion.Interface;
using API.SEG.Aplicacion.Interface.Features;
using API.SEG.Aplicacion.Interface.Infraestructure;
using API.SEG.Aplicacion.Validator;
using API.SEG.Domain.Entities;
using API.SEG.Transversal.Common;
using API.SEG.Transversal.Common.Constants;
using API.SEG.Transversal.Common.Generics;
using API.SEG.Transversal.Util.Encryptions;

namespace API.SEG.Aplicacion.Features
{
    public class AuthenticateApplication: IAuthenticateApplication
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IRedisCacheService _redisCacheService;
        private readonly IEncryptionService _encryptionService;
        private readonly ITokenService _tokenService;
        private readonly IAppLogger<AuthenticateApplication> _logger;
        private readonly AuthenticateValidationManager _authenticateValidationManager;

        public AuthenticateApplication(
            IUnitOfWork unitOfWork, 
            IMapper mapper,
            IRedisCacheService redisCacheService,
            IEncryptionService encryptionService,
            ITokenService tokenService,
            IAppLogger<AuthenticateApplication> logger, 
            AuthenticateValidationManager authenticateValidationManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _redisCacheService = redisCacheService;
            _encryptionService = encryptionService;
            _tokenService = tokenService;
            _logger = logger;
            _authenticateValidationManager = authenticateValidationManager;
        }

        public async Task<SegResponse> AuthorizeProfile(AuthenticateDto request)
        {
            try
            {
                // mapeamos la clase al authorize request
                var authorizeRequest = _mapper.Map<AuthorizeProfileRequest>(request);

                //ejecutamos las validaciones
                var validation = _authenticateValidationManager.Validate(authorizeRequest);

                //verificamos si ocurrio un error de validacion
                if (!validation.IsValid)
                    return ResponseUtil.UnprocessableEntity(validation.Errors, Validation.InvalidMessage);

                //obtenemos la informacion del login almacenado en cache
                var loginCache = await _redisCacheService.GetAsync<LoginCache>(authorizeRequest.idsession);

                //verificamos si retorna informacion de la caché
                if (loginCache == null || loginCache.lista_perfiles == null)
                    return ResponseUtil.BadRequest("Su sesión ha expirado, vuelva a intentarlo nuevamente");

                //obtenemos el perfil seleccionado mediante el codigo de perfil del request
                var perfil = loginCache.lista_perfiles.FirstOrDefault(p => p.codigo == authorizeRequest.perfil_codigo);

                //verificamos que exista el perfil solicitado
                if (perfil == null)
                    return ResponseUtil.BadRequest("El perfil seleccionado no ha sido encontrado, o probablemente su sesión ha expirado, por favor vuelva a intentarlo nuevamente");

                //obtenemos los accesos que tiene el usuario para el perfil seleccionado
                var accesosResult = await _unitOfWork.Authenticate.GetListAccesos(loginCache.usuario_id, perfil.perfil_id);

                //evaluamos si ocurrio un error de validacion desde base de datos
                if (accesosResult.Error)
                    return ResponseUtil.Conflict(accesosResult.Message);

                //verificamos que los accesos del usuario existan
                if (string.IsNullOrEmpty(accesosResult.Data))
                    return ResponseUtil.Forbidden("No se encontraron accesos asociados al usuario");

                //deserializamos la lista de opciones y permisos
                var accesos = JsonSerializer.Deserialize<List<Accesos>>(accesosResult.Data);

                //generamos la llave de encriptacion para la sesion
                var authkey = CShrapEncryption.GenerateKey();

                //generamos la id para la sesion de usuario
                var idsession = Guid.NewGuid().ToString();

                //formamos la clase con las propiedades de usuario que se guardaran en cache
                var usuarioCache = new UsuarioCache
                {
                    authkey = authkey,
                    usuariokey = _encryptionService.EncryptString(loginCache.usuario_id.ToString(), authkey),
                    perfilkey = _encryptionService.EncryptString(perfil.perfil_id.ToString(), authkey),
                    perfil_codigo = perfil.codigo,
                    perfil_descripcion = perfil.descripcion,
                    username = loginCache.username,
                    numdocumento = loginCache.numdocumento,
                    usuario_mail = loginCache.email,
                    nombre_completo = loginCache.nombrecompleto,
                    usuario_accesos = accesos
                };

                //definimos los tiempos de expiracion de la sesion en minutos
                int totalExpireCache = ApplicationKeys.authorizeAbsoluteExpiration;
                int slidingExpireCache = ApplicationKeys.authorizeSlidingExpiration;

                //guardamos en redis cache la informacion del usuario logeado
                await _redisCacheService.SetAsync(idsession, usuarioCache, totalExpireCache, slidingExpireCache);

                //generamos el Token para la sesion de usuario
                string token = await _tokenService.BuildToken(usuarioCache.username, idsession, totalExpireCache);

                //mapeamos los datos del usuario cache para el response
                var authorizeResponse = _mapper.Map<AuthorizeProfileResponse>(usuarioCache);

                // seteamos el token al response
                authorizeResponse.token = token;

                //retornamos la informacion
                return authorizeResponse != null ? 
                        ResponseUtil.Ok(authorizeResponse, AuthenticateMessage.AuthenticateSuccess
                    ) : ResponseUtil.Unauthorized();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return ResponseUtil.InternalError(ex.Message);
            }
        }

        public async Task<SegResponse> Login(AuthenticateDto request)
        {
            try
            {                
                //ejecutamos las validaciones
                var validation = _authenticateValidationManager.Validate(_mapper.Map<LoginRequest>(request));

                //verificamos si ocurrio un error de validacion
                if (!validation.IsValid)
                    return ResponseUtil.UnprocessableEntity(validation.Errors, Validation.InvalidMessage);

                // mapeamos la entidad request
                var entidad = _mapper.Map<Usuario>(request);

                // consumimos el metodo Authenticate
                var result = await _unitOfWork.Authenticate.Login(new Usuario { username = entidad.username?.Trim(), password = EncriptacionHelper.Encrypt(entidad.password?.Trim()) });

                //evaluamos si ocurrio un error de validacion desde base de datos
                if (result.Error)
                    return ResponseUtil.Conflict(result.Message);

                //verificamos que exista el usuario
                if (result.Data == null)
                    return ResponseUtil.Forbidden("No se encontró el usuario solicitado");

                //mapeamos el resultado dynamic a usuario
                Usuario usuario = _mapper.Map<Usuario>(result.Data);

                //obtenemos la lista de perfiles del usuario logueado
                var result_perfiles = await _unitOfWork.Authenticate.GetListPerfiles(new Usuario { usuario_id = usuario.usuario_id });

                //evaluamos si ocurrio un error de validacion desde base de datos
                if (result_perfiles.Error)
                    return ResponseUtil.Conflict(result.Message);

                //verificamos que exista el usuario
                if (result_perfiles.Data == null)
                    return ResponseUtil.Forbidden("No se encontraron perfiles asociados al usuario");

                //generamos un id para la sesion 
                var idsession = Guid.NewGuid().ToString();

                //mapeamos la lista de perfiles
                List<Perfil> perfiles = _mapper.Map<List<Perfil>>(result_perfiles.Data);

                //mapeamos al usuario cache
                LoginCache loginCache = _mapper.Map<LoginCache>(usuario);

                //mapeamos la lista de perfiles para el cache
                loginCache.lista_perfiles = _mapper.Map<List<LoginPerfilesCache>>(perfiles);

                //guardamos los datos del login en cache de redis
                await _redisCacheService.SetAsync(idsession, loginCache, ApplicationKeys.authenticateAbsoluteExpiration, ApplicationKeys.authenticateSlidingExpiration);

                //mapeamos la data del usuario para response
                LoginResponse response = _mapper.Map<LoginResponse>(loginCache);

                //seteamos el id de la session
                response.id = idsession;

                //registramos el log de la transaccion
                _logger.LogInformation(AuthenticateMessage.AuthenticateSuccess);

                //retornamos la informacion
                return response != null ? ResponseUtil.Ok(
                    response, AuthenticateMessage.AuthenticateSuccess
                    ) : ResponseUtil.Unauthorized();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return ResponseUtil.InternalError(ex.Message);
            }
        }
    }
}
