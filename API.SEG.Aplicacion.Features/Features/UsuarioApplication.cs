using API.SEG.Aplicacion.Dto;
using API.SEG.Aplicacion.Interface;
using API.SEG.Aplicacion.Interface.Features;
using API.SEG.Aplicacion.Validator;
using API.SEG.Transversal.Common;
using API.SEG.Transversal.Common.Generics;
using AutoMapper;

namespace API.SEG.Aplicacion.Features
{
    public class UsuarioApplication : IUsuarioApplication
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAppLogger<UsuarioApplication> _logger;
        private readonly UsuarioValidationManager _usuarioValidationManager;

        public UsuarioApplication(
            IUnitOfWork unitOfWork, 
            IMapper mapper, 
            IAppLogger<UsuarioApplication> logger, 
            UsuarioValidationManager usuarioValidationManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _usuarioValidationManager = usuarioValidationManager;
        }

        public Task<SegResponse> Insert(Request<UsuarioDto> request)
        {
            throw new NotImplementedException();
        }

        public Task<SegResponse> Update(Request<UsuarioDto> request)
        {
            throw new NotImplementedException();
        }

        public Task<SegResponse> Delete(Request<UsuarioDto> request)
        {
            throw new NotImplementedException();
        }

        public Task<SegResponse> GetById(Request<UsuarioDto> request)
        {
            throw new NotImplementedException();
        }

        public Task<SegResponse> GetList(Request<UsuarioDto> request)
        {
            throw new NotImplementedException();
        }
    }
}
