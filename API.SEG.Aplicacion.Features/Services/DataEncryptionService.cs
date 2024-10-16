using API.SEG.Aplicacion.Interface.Features;
using API.SEG.Aplicacion.Interface.Infraestructure;

namespace API.SEG.Aplicacion.Features.Services
{
    public class DataEncryptionService : IDataEncryptionService
    {
        private readonly ISessionService _sessionService;
        private readonly IEncryptionService _encryptionService;

        public DataEncryptionService(ISessionService sessionService, IEncryptionService encryptionService)
        {
            _sessionService = sessionService;
            _encryptionService = encryptionService;
        }

        public string? Decrypt(string? value)
        {
            return  string.IsNullOrEmpty(value) ? null :
                    string.IsNullOrEmpty(_sessionService.UsuarioCache.authkey) ? null : 
                    _encryptionService.DecryptString(value, _sessionService.UsuarioCache.authkey);
        }

        public string? Encrypt(string? value)
        {
            return  string.IsNullOrEmpty(value) ? null :
                    string.IsNullOrEmpty(_sessionService.UsuarioCache.authkey) ? null : 
                    _encryptionService.EncryptString(value, _sessionService.UsuarioCache.authkey);
        }
    }
}
