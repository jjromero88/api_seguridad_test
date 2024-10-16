using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.SEG.Domain.Entities
{
    public class UsuarioCache
    {
        public string? authkey { get; set; }
        public string? usuariokey { get; set; }
        public string? perfilkey { get; set; }
        public string? perfil_codigo { get; set; }
        public string? perfil_descripcion { get; set; }
        public string? username { get; set; }
        public string? numdocumento { get; set; }
        public string? usuario_mail { get; set; }
        public string? nombre_completo { get; set; }
        public List<Accesos>? usuario_accesos { get; set; }
    }
}
