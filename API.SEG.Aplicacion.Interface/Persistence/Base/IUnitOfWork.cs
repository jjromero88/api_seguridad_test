using API.SEG.Aplicacion.Interface.Persistence;

namespace API.SEG.Aplicacion.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        IUsuarioRepository Usuario { get; }
        IAuthenticateRepository Authenticate { get; }
        IPerfilRepository Perfil { get; }
    }
}
