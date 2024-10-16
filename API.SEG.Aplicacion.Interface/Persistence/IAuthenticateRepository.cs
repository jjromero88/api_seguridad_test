using API.SEG.Domain.Entities;
using API.SEG.Transversal.Common;

namespace API.SEG.Aplicacion.Interface.Persistence
{
    public interface IAuthenticateRepository
    {
        Task<Response<dynamic>> Login(Usuario entidad);
        Task<Response<List<dynamic>>> GetListPerfiles(Usuario entidad);
        Task<Response<string>> GetListAccesos(int usuario_id, int perfil_id);
    }
}
