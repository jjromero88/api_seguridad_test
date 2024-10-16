using API.SEG.Aplicacion.Dto;
using API.SEG.Transversal.Common.Generics;

namespace API.SEG.Aplicacion.Interface.Features
{
    public interface IAuthenticateApplication
    {
        Task<SegResponse> Login(AuthenticateDto request);
        Task<SegResponse> AuthorizeProfile(AuthenticateDto request);
    }
}
