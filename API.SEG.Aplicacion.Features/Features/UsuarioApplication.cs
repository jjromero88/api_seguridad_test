using API.SEG.Aplicacion.Dto;
using API.SEG.Aplicacion.Interface;
using API.SEG.Aplicacion.Interface.Features;
using API.SEG.Aplicacion.Validator;
using API.SEG.Domain.Entities;
using API.SEG.Transversal.Common;
using API.SEG.Transversal.Common.Constants;
using API.SEG.Transversal.Common.Generics;
using API.SEG.Transversal.Util.Encryptions;
using AutoMapper;

namespace API.SEG.Aplicacion.Features
{
    public class UsuarioApplication : IUsuarioApplication
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly IDataEncryptionService _dataEncryptionService;
        private readonly IAppLogger<UsuarioApplication> _logger;
        private readonly UsuarioValidationManager _usuarioValidationManager;

        public UsuarioApplication(
            IUnitOfWork unitOfWork, 
            IMapper mapper, 
            ISessionService sessionService, 
            IDataEncryptionService dataEncryptionService,
            IAppLogger<UsuarioApplication> logger, 
            UsuarioValidationManager usuarioValidationManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _sessionService = sessionService;
            _dataEncryptionService = dataEncryptionService;
            _logger = logger;
            _usuarioValidationManager = usuarioValidationManager;
        }

        public async Task<SegResponse> Insert(Request<UsuarioDto> request)
        {
            try
            {
                // consumimos el servicio de validacion
                var validation = _usuarioValidationManager.Validate(_mapper.Map<UsuarioInsertRequest>(request.entidad));

                // si es invalido retornamos los mensajes de validacion
                if (!validation.IsValid)
                    return ResponseUtil.UnprocessableEntity(validation.Errors, Validation.InvalidMessage);

                // mapeamos la entidad de request
                var entidad = _mapper.Map<Usuario>(request.entidad);

                // convertimos en cadena separada por ',' al array de perfiles
                string perfileskey = request.entidad?.perfileskey?.Length > 0 ? string.Join(",", request.entidad.perfileskey) : string.Empty;

                // desencriptamos los id de perfiles
                entidad.perfiles_id = _dataEncryptionService.DecryptArray(perfileskey);

                // encriptamos la contraseña
                entidad.password = string.IsNullOrEmpty(request.entidad.password) ? null : EncriptacionHelper.Encrypt(request.entidad.password?.Trim());
                
                // obtenemos de la data de sesion el nombre de usuario actual
                entidad.usuario_reg = _sessionService.UsuarioCache.username;

                // consumimos el servicio repository
                var result = await _unitOfWork.Usuario.Insert(entidad);

                // verificamos si desde la base de datos retorna un error
                if (result.Error)
                    return ResponseUtil.Conflict(result.Message);

                // retornamos la respuesta success
                return ResponseUtil.Created(message: TransactionMessage.SaveSuccess);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return ResponseUtil.InternalError(message: ex.Message);
            }
        }

        public async Task<SegResponse> Update(Request<UsuarioDto> request)
        {
            try
            {
                // consumimos el servicio de validacion
                var validation = _usuarioValidationManager.Validate(_mapper.Map<UsuarioUpdateRequest>(request.entidad));

                // si hay errores de validacion retornamos los mensajes
                if (!validation.IsValid)
                    return ResponseUtil.UnprocessableEntity(validation.Errors, Validation.InvalidMessage);

                // mapeamos la entidad de request
                var entidad = _mapper.Map<Usuario>(request.entidad);

                // convertimos en cadena separada por ',' al array de perfiles
                string perfileskey = request.entidad?.perfileskey?.Length > 0 ? string.Join(",", request.entidad.perfileskey) : string.Empty;

                // desencriptamos los id de perfiles
                entidad.perfiles_id = _dataEncryptionService.DecryptArray(perfileskey);

                // desencriptamos el id del usuario
                entidad.usuario_id = Convert.ToInt32(_dataEncryptionService.Decrypt(request.entidad.serialkey));

                // encriptamos la contraseña
                entidad.password = string.IsNullOrEmpty(request.entidad.password) ? null : EncriptacionHelper.Encrypt(request.entidad.password?.Trim());
                
                // consumimos el servicio que obtiene la data del usuario de sesion
                entidad.usuario_act = _sessionService.UsuarioCache.username;

                // consumimos el servicio update de repository
                var result = await _unitOfWork.Usuario.Update(entidad);

                // verificamos si ocurrio un error de base de datos
                if (result.Error)
                    return ResponseUtil.Conflict(result.Message);

                // retornamos la informacion
                return ResponseUtil.Created(message: TransactionMessage.UpdateSuccess);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return ResponseUtil.InternalError(message: ex.Message);
            }
        }

        public async Task<SegResponse> Delete(Request<UsuarioDto> request)
        {
            try
            {
                // consumimos el servicio de validacion
                var validation = _usuarioValidationManager.Validate(_mapper.Map<UsuarioIdRequest>(request.entidad));

                // si hay errores de validacion retornamos los mensajes
                if (!validation.IsValid)
                    return ResponseUtil.UnprocessableEntity(validation.Errors, Validation.InvalidMessage);

                // mapeamos la entidad de request
                var entidad = _mapper.Map<Usuario>(request.entidad);

                // desencriptamos el id
                entidad.usuario_id = Convert.ToInt32(_dataEncryptionService.Decrypt(request.entidad.serialkey));

                // obtenemos el username del usuario de sesion
                entidad.usuario_act = _sessionService.UsuarioCache.username;

                // consumimos el servicio de delete desde repository
                var result = await _unitOfWork.Usuario.Delete(entidad);

                // verificamos si ocurrió un error desde base de datos
                if (result.Error)
                    return ResponseUtil.Conflict(result.Message);

                // retornamos el resulado
                return ResponseUtil.Created(message: result.Message ?? TransactionMessage.DeleteSuccess);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return ResponseUtil.InternalError(message: ex.Message);
            }
        }

        public async Task<SegResponse> GetById(Request<UsuarioDto> request)
        {
            try
            {
                // consumimos el servicio de validacion
                var validation = _usuarioValidationManager.Validate(_mapper.Map<UsuarioIdRequest>(request.entidad));

                // verificamos si ocurrio un error de validacion
                if (!validation.IsValid)
                    return ResponseUtil.UnprocessableEntity(validation.Errors, Validation.InvalidMessage);

                // mapeamos el request
                var entidad = _mapper.Map<Usuario>(request.entidad);

                // desencriptamos el id
                entidad.usuario_id = Convert.ToInt32(_dataEncryptionService.Decrypt(request.entidad.serialkey));

                // consumimos el servicio de getbyid
                var result = await _unitOfWork.Usuario.GetById(entidad);

                // verificamos si ocurrió un error desde base de datos
                if (result.Error)
                    return ResponseUtil.Conflict(result.Message);

                // si hay data retornamos la informacion
                if (result.Data != null)
                {
                    // mapeamos el resultado
                    entidad = new Usuario
                    {
                        serialkey = _dataEncryptionService.Encrypt(result.Data.usuario_id.ToString()),
                        username = result.Data.username,
                        numdocumento = result.Data.numdocumento,
                        nombrecompleto = result.Data.nombrecompleto,
                        email = result.Data.email,
                        habilitado = result.Data.habilitado,
                        password = EncriptacionHelper.Decrypt(result.Data.password),
                        estado = result.Data.estado,
                        usuario_reg = result.Data.usuario_reg,
                        fecha_reg = result.Data.fecha_reg,
                        usuario_act = result.Data.usuario_act,
                        fecha_act = result.Data.fecha_act
                    };
                }

                // retornamos la informacion
                return result.Data != null ? ResponseUtil.Ok(
                    _mapper.Map<UsuarioResponse>(_mapper.Map<UsuarioDto>(entidad)), result.Message ?? TransactionMessage.QuerySuccess
                    ) : ResponseUtil.NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return ResponseUtil.InternalError(message: ex.Message);
            }
        }

        public async Task<SegResponse> GetList(Request<UsuarioDto> request)
        {
            try
            {
                // mapeamos la entidad de usuario
                var entidad = _mapper.Map<Usuario>(request.entidad);

                // desencriptamos el id de usuario
                entidad.usuario_id = Convert.ToInt32(_dataEncryptionService.Decrypt(request.entidad.serialkey));

                // consumimos el servicio de la lista
                var result = await _unitOfWork.Usuario.GetList(entidad);

                // verificamos si ocurrió un error desde base de datos
                if (result.Error)
                    return ResponseUtil.Conflict(result.Message);

                // instanciamos la lista de usuario
                List<Usuario> Lista = new List<Usuario>();

                // verificamos si hay data de respuesta
                if (result.Data != null)
                {
                    foreach (var item in result.Data)
                    {
                        // mapeamos el resutlado de la data
                        Lista.Add(new Usuario()
                        {
                            serialkey = _dataEncryptionService.Encrypt(item.usuario_id.ToString()),
                            username = item.username,
                            numdocumento = item.numdocumento,
                            nombrecompleto = item.nombrecompleto,
                            email = item.email,
                            habilitado = item.habilitado,
                            password = EncriptacionHelper.Decrypt(item.password),
                            usuario_reg = item.usuario_reg,
                            fecha_reg = item.fecha_reg
                        });
                    }
                }

                // retornamos la informacion
                return result != null ? ResponseUtil.Ok(
                    _mapper.Map<List<UsuarioResponse>>(_mapper.Map<List<UsuarioDto>>(Lista)),
                    result.Message ?? TransactionMessage.QuerySuccess
                    ) : ResponseUtil.NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return ResponseUtil.InternalError(message: ex.Message);
            }
        }
    }
}
