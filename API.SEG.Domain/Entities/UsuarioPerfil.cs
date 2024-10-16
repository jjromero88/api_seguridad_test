using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.SEG.Domain.Entities
{
    public class UsuarioPerfil : EntidadBase
    {
        public int usuarioperfil_id { get; set; }
        public int? usuario_id { get; set; }
        public int? perfil_id { get; set; }
        public string? usuariokey { get; set; }
        public string? perfilkey { get; set; }
        public Usuario? usuario { get; set; } = new Usuario();
        public Perfil? perfil { get; set; } = new Perfil();
    }
}
