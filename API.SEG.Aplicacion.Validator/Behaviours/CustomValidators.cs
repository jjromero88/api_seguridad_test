using FluentValidation;
using System.Text.RegularExpressions;

namespace API.SEG.Aplicacion.Validator
{
    public static class CustomValidators
    {
        public static IRuleBuilderOptions<T, string?> IsNullOrWhiteSpace<T>(this IRuleBuilder<T, string?> ruleBuilder)
        {
            return ruleBuilder.Must(m => !String.IsNullOrWhiteSpace(m));
        }

        public static IRuleBuilderOptions<T, string?> BeNumeric<T>(this IRuleBuilder<T, string?> ruleBuilder)
        {
            return ruleBuilder.Must((rootObject, value, context) =>
            {
                if (string.IsNullOrEmpty(value))
                    return true;

                return long.TryParse(value, out _);
            });
        }

        public static IRuleBuilderOptions<T, string?> SecretKeyValidate<T>(this IRuleBuilder<T, string?> ruleBuilder)
        {
            return ruleBuilder
            .NotNull()
            .NotEmpty()
            .MinimumLength(15)
            .MaximumLength(20)
            .Must(secretkey =>
                secretkey.Any(char.IsLower) &&
                secretkey.Any(char.IsUpper) &&
                secretkey.Any(c => !char.IsLetterOrDigit(c)));
            //.WithMessage("La secretkey debe tener al menos una letra minúscula, una letra mayúscula, un caracter especial y una longitud entre 15 y 20 caracteres.");
        }

        public static IRuleBuilderOptions<T, string?> NotContainSqlInjection<T>(this IRuleBuilder<T, string?> ruleBuilder)
        {
            return ruleBuilder.Must(value =>
            {
                // Utiliza una expresión regular para verificar que no haya caracteres no permitidos.
                // Puedes personalizar esta expresión regular según tus necesidades específicas.
                // Esta expresión regular verifica la ausencia de caracteres como ' y ; que son comunes en inyecciones SQL.
                string sqlInjectionPattern = @"[';]";

                if (string.IsNullOrEmpty(value))
                    return true; // No se puede inyectar SQL en una cadena vacía.

                return !Regex.IsMatch(value, sqlInjectionPattern);
            }).WithMessage("Contiene caracteres no permitidos (posible inyección SQL).");
        }

        public static bool BeValidInteger(int? value)
        {
            return value.HasValue;
        }
        public static bool BeValidDecimal(decimal? value)
        {
            return value.HasValue;
        }
        public static bool BeValidDateTime(DateTime? value)
        {
            return value.HasValue;
        }
        public static bool BeValidDouble(double? value)
        {
            return value.HasValue;
        }

    }

}
