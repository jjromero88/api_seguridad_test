using API.SEG.Aplicacion.Dto;
using FluentValidation.Results;

namespace API.SEG.Aplicacion.Validator
{
    public class AuthenticateValidationManager
    {
        private readonly LoginRequestValidator _loginRequestValidator;
        private readonly AuthorizeProfileRequestValidator _authorizeProfileRequestValidator;

        public AuthenticateValidationManager()
        {
            _loginRequestValidator = new LoginRequestValidator();
            _authorizeProfileRequestValidator = new AuthorizeProfileRequestValidator();
        }

        public ValidationResult Validate(LoginRequest entidad)
        {
            return _loginRequestValidator.Validate(entidad);
        }
        public ValidationResult Validate(AuthorizeProfileRequest entidad)
        {
            return _authorizeProfileRequestValidator.Validate(entidad);
        }
    }
}
