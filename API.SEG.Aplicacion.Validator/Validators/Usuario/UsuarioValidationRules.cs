using API.SEG.Aplicacion.Dto;
using FluentValidation;

namespace API.SEG.Aplicacion.Validator
{
    public class UsuarioIdRequestValidator : AbstractValidator<UsuarioIdRequest>
    {
        public UsuarioIdRequestValidator()
        {
            RuleFor(u => u.serialkey)
                .IsNullOrWhiteSpace()
                  .WithMessage("Debe ingresar el Id del Usuario");
        }
    }
    public class UsuarioInsertRequestValidator : AbstractValidator<UsuarioInsertRequest>
    {
        public UsuarioInsertRequestValidator()
        {
            RuleFor(u => u.perfileskey)
            .NotEmpty()
            .WithMessage("Debe ingresar por lo menos un perfil");

            RuleFor(u => u.username)
            .IsNullOrWhiteSpace()
            .WithMessage("Debe ingresar el nombre de usuario");

            RuleFor(u => u.password)
            .IsNullOrWhiteSpace()
            .WithMessage("Debe ingresar una contraseña");

            RuleFor(u => u.numdocumento)
            .IsNullOrWhiteSpace()
            .WithMessage("Debe ingresar el numero de documento");

            RuleFor(u => u.nombrecompleto)
            .IsNullOrWhiteSpace()
            .WithMessage("Debe ingresar el nombre completo");

            RuleFor(u => u.email)
            .IsNullOrWhiteSpace()
            .WithMessage("Debe ingresar un correo electrónico");

            RuleFor(x => x.username)
            .MaximumLength(20)
            .WithMessage("El nombre de usuario debe tener máximo 20 caracteres");

            RuleFor(x => x.password)
            .MaximumLength(15)
            .WithMessage("La contraseña debe tener máximo 15 caracteres");

            RuleFor(x => x.username)
            .MinimumLength(6)
            .WithMessage("El nombre de usuario debe tener mínimo 6 caracteres");

            RuleFor(x => x.password)
            .MinimumLength(6)
            .WithMessage("La contraseña debe tener mínimo 6 caracteres");

            RuleFor(x => x.numdocumento)
            .MaximumLength(20)
            .WithMessage("El número de documento debe tener máximo 20 caracteres");
        }
    }
    public class UsuarioUpdateRequestValidator : AbstractValidator<UsuarioUpdateRequest>
    {
        public UsuarioUpdateRequestValidator()
        {
            RuleFor(u => u.serialkey)
            .IsNullOrWhiteSpace()
            .WithMessage("Debe ingresar el Id del Usuario");

            RuleFor(u => u.perfileskey)
            .NotEmpty()
            .WithMessage("Debe ingresar por lo menos un perfil");

            RuleFor(u => u.username)
            .IsNullOrWhiteSpace()
            .WithMessage("Debe ingresar el nombre de usuario");

            RuleFor(u => u.password)
            .IsNullOrWhiteSpace()
            .WithMessage("Debe ingresar una contraseña");

            RuleFor(u => u.numdocumento)
            .IsNullOrWhiteSpace()
            .WithMessage("Debe ingresar el numero de documento");

            RuleFor(u => u.nombrecompleto)
            .IsNullOrWhiteSpace()
            .WithMessage("Debe ingresar el nombre completo");

            RuleFor(u => u.email)
            .IsNullOrWhiteSpace()
            .WithMessage("Debe ingresar un correo electrónico");

            RuleFor(x => x.username)
            .MaximumLength(20)
            .WithMessage("El nombre de usuario debe tener máximo 20 caracteres");

            RuleFor(x => x.password)
            .MaximumLength(15)
            .WithMessage("La contraseña debe tener máximo 15 caracteres");

            RuleFor(x => x.username)
            .MinimumLength(6)
            .WithMessage("El nombre de usuario debe tener mínimo 6 caracteres");

            RuleFor(x => x.password)
            .MinimumLength(6)
            .WithMessage("La contraseña debe tener mínimo 6 caracteres");

            RuleFor(x => x.numdocumento)
            .MaximumLength(20)
            .WithMessage("El número de documento debe tener máximo 20 caracteres");
        }
    }
}
