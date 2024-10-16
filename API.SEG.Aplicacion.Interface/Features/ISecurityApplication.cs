using API.SEG.Aplicacion.Dto;
using API.SEG.Transversal.Common.Generics;

namespace API.SEG.Aplicacion.Interface.Features
{
    public interface ISecurityApplication
    {
        Task<SegResponse> GetSessionData();
        Task<SegResponse> GetAccesos();
        Task<SegResponse> GetAccesoPermisos(AccesosDto request);
    }
}
