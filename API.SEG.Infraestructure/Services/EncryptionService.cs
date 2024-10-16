using API.SEG.Aplicacion.Interface.Infraestructure;
using API.SEG.Transversal.Util.Encryptions;

namespace API.SEG.Infraestructure.Services
{
    public class EncryptionService : IEncryptionService
    {
        public string? EncryptString(string? value, string passPhrase)
        {
            return string.IsNullOrEmpty(value) ? null : CShrapEncryption.EncryptString(value, passPhrase);
        }
        public string? DecryptString(string? value, string passPhrase)
        {
            return string.IsNullOrEmpty(value) ? null : CShrapEncryption.DecryptString(value, passPhrase);
        }
        public string? DecryptArray(string? values, string passPhrase)
        {
            return string.IsNullOrEmpty(values) ? null : CShrapEncryption.DecryptArray(values, passPhrase);
        }
    }
}
