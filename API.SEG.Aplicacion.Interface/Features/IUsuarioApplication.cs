using API.SEG.Aplicacion.Dto;
using API.SEG.Transversal.Common;
using API.SEG.Transversal.Common.Generics;

namespace API.SEG.Aplicacion.Interface.Features
{
    public interface IUsuarioApplication
    {
        Task<SegResponse> Insert(Request<UsuarioDto> request);
        Task<SegResponse> Update(Request<UsuarioDto> request);
        Task<SegResponse> Delete(Request<UsuarioDto> request);
        Task<SegResponse> GetById(Request<UsuarioDto> request);
        Task<SegResponse> GetList(Request<UsuarioDto> request);
    }
}
