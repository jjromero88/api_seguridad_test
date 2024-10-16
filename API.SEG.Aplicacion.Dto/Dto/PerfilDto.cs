using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.SEG.Aplicacion.Dto
{
    public class PerfilDto : EntidadBase
    {
        public string? codigo { get; set; }
        public string? abreviatura { get; set; }
        public string? descripcion { get; set; }
    }
    public class PerfilFilterRequest
    {
        public string? serialkey { get; set; }
        public string? filtro { get; set; }
    }
    public class PerfilResponse
    {
        public string? serialkey { get; set; }
        public string? codigo { get; set; }
        public string? abreviatura { get; set; }
        public string? descripcion { get; set; }
    }
}
