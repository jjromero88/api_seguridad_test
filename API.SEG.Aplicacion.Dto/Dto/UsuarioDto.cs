using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.SEG.Aplicacion.Dto
{
    public class UsuarioDto: EntidadBase
    {
        public string? username { get; set; }
        public string? password { get; set; }
        public string? numdocumento { get; set; }
        public string? nombrecompleto { get; set; }
        public string? email { get; set; }
        public bool? habilitado { get; set; }
    }
    public class UsuarioIdRequest
    {
        public string? serialkey { get; set; }
    }
    public class UsuarioInsertRequest
    { 
        public string? username { get; set; }
        public string? password { get; set; }
        public string? numdocumento { get; set; }
        public string? nombrecompleto { get; set; }
        public string? email { get; set; }
    }
    public class UsuarioUpdateRequest
    {
        public string? serialkey { get; set; }
        public string? username { get; set; }
        public string? password { get; set; }
        public string? numdocumento { get; set; }
        public string? nombrecompleto { get; set; }
        public string? email { get; set; }
        public bool? habilitado { get; set; }
    }
    public class UsuarioFilterRequest
    {
        public string? serialkey { get; set; }
        public string? numdocumento { get; set; }
        public bool? habilitado { get; set; }
        public string? filtro { get; set; }
    }
    public class UsuarioResponse
    {
        public string? serialkey { get; set; }
        public string? username { get; set; }
        public string? numdocumento { get; set; }
        public string? nombrecompleto { get; set; }
        public string? email { get; set; }
        public bool? habilitado { get; set; }
    }
}
