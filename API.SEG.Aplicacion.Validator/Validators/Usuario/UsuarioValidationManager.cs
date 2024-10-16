using API.SEG.Aplicacion.Dto;
using FluentValidation.Results;

namespace API.SEG.Aplicacion.Validator
{
    public class UsuarioValidationManager
    {
        private readonly UsuarioIdRequestValidator _usuarioIdRequestValidator;
        private readonly UsuarioInsertRequestValidator _usuarioInsertRequestValidator;
        private readonly UsuarioUpdateRequestValidator _usuarioUpdateRequestValidator;

        public UsuarioValidationManager()
        {
            _usuarioIdRequestValidator = new UsuarioIdRequestValidator();
            _usuarioInsertRequestValidator = new UsuarioInsertRequestValidator();
            _usuarioUpdateRequestValidator = new UsuarioUpdateRequestValidator();
        }

        public ValidationResult Validate(UsuarioIdRequest entidad)
        {
            return _usuarioIdRequestValidator.Validate(entidad);
        }
        public ValidationResult Validate(UsuarioInsertRequest entidad)
        {
            return _usuarioInsertRequestValidator.Validate(entidad);
        }
        public ValidationResult Validate(UsuarioUpdateRequest entidad)
        {
            return _usuarioUpdateRequestValidator.Validate(entidad);
        }
    }
}
