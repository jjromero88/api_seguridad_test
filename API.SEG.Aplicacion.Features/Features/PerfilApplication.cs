using API.SEG.Aplicacion.Dto;
using API.SEG.Aplicacion.Interface;
using API.SEG.Aplicacion.Interface.Features;
using API.SEG.Domain.Entities;
using API.SEG.Transversal.Common;
using API.SEG.Transversal.Common.Constants;
using API.SEG.Transversal.Common.Generics;
using AutoMapper;

namespace API.SEG.Aplicacion.Features
{
    public class PerfilApplication : IPerfilApplication
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly IDataEncryptionService _dataEncryptionService;
        private readonly IAppLogger<PerfilApplication> _logger;

        public PerfilApplication(
            IUnitOfWork unitOfWork, 
            IMapper mapper, 
            ISessionService sessionService, 
            IDataEncryptionService dataEncryptionService, 
            IAppLogger<PerfilApplication> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _sessionService = sessionService;
            _dataEncryptionService = dataEncryptionService;
            _logger = logger;
        }

        public Task<SegResponse> Insert(Request<PerfilDto> request)
        {
            throw new NotImplementedException();
        }

        public Task<SegResponse> Update(Request<PerfilDto> request)
        {
            throw new NotImplementedException();
        }

        public Task<SegResponse> Delete(Request<PerfilDto> request)
        {
            throw new NotImplementedException();
        }

        public Task<SegResponse> GetById(Request<PerfilDto> request)
        {
            throw new NotImplementedException();
        }

        public async Task<SegResponse> GetList(Request<PerfilDto> request)
        {
            try
            {
                // mapeamos la entidad de perfil
                var entidad = _mapper.Map<Perfil>(request.entidad);

                // desencriptamos el id de perfil
                entidad.perfil_id = Convert.ToInt32(_dataEncryptionService.Decrypt(request.entidad.serialkey));

                // consumimos el servicio de la lista
                var result = await _unitOfWork.Perfil.GetList(entidad);

                // verificamos si ocurrió un error desde base de datos
                if (result.Error)
                    return ResponseUtil.Conflict(result.Message);

                // instanciamos la lista de perfiles
                List<Perfil> Lista = new List<Perfil>();

                // verificamos si hay data de respuesta
                if (result.Data != null)
                {
                    foreach (var item in result.Data)
                    {
                        // mapeamos el resutlado de la data
                        Lista.Add(new Perfil()
                        {
                            serialkey = _dataEncryptionService.Encrypt(item.perfil_id.ToString()),
                            codigo = item.codigo,
                            abreviatura = item.abreviatura,
                            descripcion = item.descripcion,
                            usuario_reg = item.usuario_reg,
                            fecha_reg = item.fecha_reg
                        });
                    }
                }

                // retornamos la informacion
                return result != null ? ResponseUtil.Ok(
                    _mapper.Map<List<PerfilResponse>>(_mapper.Map<List<PerfilDto>>(Lista)),
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
