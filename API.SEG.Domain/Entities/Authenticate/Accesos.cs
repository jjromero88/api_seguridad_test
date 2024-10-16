using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.SEG.Domain.Entities
{
    public class Accesos
    {
        public string? codigo {  get; set; }
        public string? descripcion { get; set; }
        public string? abreviatura {  get; set; }
        public string? url_opcion {  get; set; }
        public string? icono_opcion { get; set; }
        public int? num_orden {  get; set; }
        public List<Accesos>? lista_accesos { get; set; }
        public List<AccesoPermiso>? lista_permisos { get; set; }
    }
}
