using API.SEG.Aplicacion.Dto;
using FluentValidation;

namespace API.SEG.Aplicacion.Validator
{
    public class AuthorizeProfileRequestValidator : AbstractValidator<AuthorizeProfileRequest>
    {
        public AuthorizeProfileRequestValidator()
        {
            RuleFor(u => u.idsession)
            .IsNullOrWhiteSpace()
            .WithMessage("Debe ingresar el id de la session");

            RuleFor(u => u.perfil_codigo)
            .IsNullOrWhiteSpace()
            .WithMessage("Debe seleccionar un perfil");

            RuleFor(x => x.idsession)
           .MaximumLength(50)
           .WithMessage("El id de la session no debe superar los 50 caracteres");

            RuleFor(x => x.perfil_codigo)
           .MaximumLength(10)
           .WithMessage("El código del perfil debe superar los 10 caracteres");
        }
    }

    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(u => u.username)
            .IsNullOrWhiteSpace()
            .WithMessage("Debe ingresar el nombre de usuario");

            RuleFor(u => u.password)
            .IsNullOrWhiteSpace()
            .WithMessage("Debe ingresar una contraseña");

            RuleFor(x => x.username)
            .MaximumLength(20)
            .WithMessage("El nombre de usuario debe tener máximo 10 caracteres");

            RuleFor(x => x.password)
            .MaximumLength(15)
            .WithMessage("La contraseña debe tener máximo 10 caracteres");

            RuleFor(x => x.username)
            .MinimumLength(5)
            .WithMessage("El nombre de usuario debe tener mínimo 5 caracteres");

            RuleFor(x => x.password)
            .MinimumLength(5)
            .WithMessage("La contraseña debe tener mínimo 5 caracteres");
        }
    }

}
