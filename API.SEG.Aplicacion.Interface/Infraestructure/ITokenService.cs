using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.SEG.Aplicacion.Interface.Infraestructure
{
    public interface ITokenService
    {
        Task<string> BuildToken(string userName, string idSession, int expireMinutes);
        Task<string> GetIdSessionFromToken(string tokenString);
    }
}
