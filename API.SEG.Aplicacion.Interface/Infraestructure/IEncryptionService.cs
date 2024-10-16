using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.SEG.Aplicacion.Interface.Infraestructure
{
    public interface IEncryptionService
    {
        string? EncryptString(string? value, string passPhrase);
        string? DecryptString(string? value, string passPhrase);
    }
}
