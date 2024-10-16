using API.SEG.Aplicacion.Dto;
using API.SEG.Transversal.Common.Generics;
using API.SEG.Transversal.Common;

namespace API.SEG.Aplicacion.Interface.Features
{
    public interface IPerfilApplication
    {
        Task<SegResponse> Insert(Request<PerfilDto> request);
        Task<SegResponse> Update(Request<PerfilDto> request);
        Task<SegResponse> Delete(Request<PerfilDto> request);
        Task<SegResponse> GetById(Request<PerfilDto> request);
        Task<SegResponse> GetList(Request<PerfilDto> request);
    }
}
