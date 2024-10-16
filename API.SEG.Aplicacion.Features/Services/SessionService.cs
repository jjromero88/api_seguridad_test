using API.SEG.Aplicacion.Interface.Features;
using API.SEG.Domain.Entities;

namespace API.SEG.Aplicacion.Features.Services
{
    public class SessionService : ISessionService
    {
        public UsuarioCache UsuarioCache { get; set; } = new UsuarioCache();
        private string _token;

        public void updateUsuarioCache(UsuarioCache entidad)
        {
            UsuarioCache = entidad;
        }

        public void SetToken(string token)
        {
            if (token.StartsWith("Bearer "))
                token = token.Substring("Bearer ".Length).Trim();

            _token = token;
        }

        public string GetToken()
        {
            return _token;
        }
    }
}
