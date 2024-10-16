using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.SEG.Aplicacion.Dto
{
    public class AccesosDto
    {
        public string? codigo { get; set; }
        public string? descripcion { get; set; }
        public string? abreviatura { get; set; }
        public string? url_opcion { get; set; }
        public string? icono_opcion { get; set; }
        public int? num_orden { get; set; }
        public List<AccesosResponse>? lista_accesos { get; set; }
    }
    public class AccesosResponse
    {
        public string? codigo { get; set; }
        public string? descripcion { get; set; }
        public string? abreviatura { get; set; }
        public string? url_opcion { get; set; }
        public string? icono_opcion { get; set; }
        public int? num_orden { get; set; }
        public List<AccesosResponse>? lista_accesos { get; set; }
    }

    public class AccesoPermisosRequest
    {
        public string? url_opcion { get; set; }
    }
    public class AccesoPermisosResponse
    {
        public string[]? permisos { get; set; }
    }
}
