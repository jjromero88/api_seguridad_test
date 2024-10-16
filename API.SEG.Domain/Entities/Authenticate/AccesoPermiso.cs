using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.SEG.Domain.Entities
{
    public class AccesoPermiso
    {
        public int permiso_id {  get; set; }
        public string? permiso_codigo { get; set; }
        public string? permiso_nombre { get; set; }
        public string? permiso_descripcion { get; set; }
    }
}
