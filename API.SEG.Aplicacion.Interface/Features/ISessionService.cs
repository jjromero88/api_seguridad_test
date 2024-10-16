using API.SEG.Domain.Entities;

namespace API.SEG.Aplicacion.Interface.Features
{
    public interface ISessionService
    {
        public UsuarioCache UsuarioCache { get; set; }
        public void updateUsuarioCache(UsuarioCache entidad);
        void SetToken(string token);
        string GetToken();
    }
}
