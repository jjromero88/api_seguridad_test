using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.SEG.Aplicacion.Dto
{
    public class AuthenticateDto
    {
        public string? username { get; set; }
        public string? password { get; set; }
        public string? idsession { get; set; }
        public string? perfil_codigo { get; set; }
    }

    public class AuthorizeProfileRequest
    {
        public string? idsession { get; set; }
        public string? perfil_codigo { get; set; }
    }

    public class AuthorizeProfileResponse
    {
        public string? token { get; set; }
        public string? nombre_completo { get; set; }
        public string? username { get; set; }
        public string? perfil_descripcion { get; set; }
        public string? perfil_codigo { get; set; }
        public string? numdocumento { get; set; }
    }

    public class LoginRequest
    {
        public string? username { get; set; }
        public string? password { get; set; }
    }
    public class LoginCache
    {
        public int usuario_id { get; set; }
        public string? username { get; set; }
        public string? nombrecompleto { get; set; }
        public string? numdocumento { get; set; }
        public string? email { get; set; }
        public List<LoginPerfilesCache>? lista_perfiles { get; set; }
    }
    public class LoginPerfilesCache
    {
        public int perfil_id { get; set; }
        public string? codigo { get; set; }
        public string? descripcion { get; set; }
    }

    public class LoginResponse
    {
        public string? id { get; set; }
        public string? username { get; set; }
        public string? nombrecompleto { get; set; }
        public List<LoginPerfilesResponse>? lista_perfiles { get; set; }
    }

    public class LoginPerfilesResponse
    {
        public string? codigo { get; set; }
        public string? descripcion { get; set; }
    }

}
