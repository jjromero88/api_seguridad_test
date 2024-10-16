using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.SEG.Aplicacion.Interface.Features
{
    public interface IDataEncryptionService
    {
        string? Encrypt(string? value);
        string? Decrypt(string? value);
    }
}
