using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.SEG.Domain.Entities
{
    public class Usuario : EntidadBase
    {
        public int usuario_id { get; set; }
        public string? username { get; set; }
        public string? password { get; set; }
        public string? numdocumento { get; set; }
        public string? nombrecompleto { get; set; }
        public string? email { get; set; }
        public bool? habilitado { get; set; }
    }
}
