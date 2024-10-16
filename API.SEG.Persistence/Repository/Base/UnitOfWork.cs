using API.SEG.Aplicacion.Interface;
using API.SEG.Aplicacion.Interface.Persistence;

namespace API.SEG.Persistence.Repository.Base
{
    public class UnitOfWork : IUnitOfWork
    {
        public IUsuarioRepository Usuario { get; }
        public IAuthenticateRepository Authenticate { get; }
        public IPerfilRepository Perfil {  get; }

        public UnitOfWork(
            IUsuarioRepository usuario, 
            IAuthenticateRepository authenticate,
            IPerfilRepository perfil)
        {
            Usuario = usuario;
            Authenticate = authenticate;
            Perfil = perfil;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
