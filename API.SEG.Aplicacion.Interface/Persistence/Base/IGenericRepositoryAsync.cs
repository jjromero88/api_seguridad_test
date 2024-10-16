using API.SEG.Transversal.Common;

namespace API.SEG.Aplicacion.Interface
{
    public interface IGenericRepositoryAsync<T> where T : class
    {
        Task<Response> Insert(T entidad);
        Task<Response> Update(T entidad);
        Task<Response> Delete(T entidad);
        Task<Response<dynamic>> GetById(T entidad);
        Task<Response<List<dynamic>>> GetList(T entidad);
    }
}
