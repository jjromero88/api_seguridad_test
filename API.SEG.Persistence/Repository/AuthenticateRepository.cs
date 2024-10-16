using API.SEG.Aplicacion.Interface.Persistence;
using API.SEG.Domain.Entities;
using API.SEG.Persistence.Context;
using API.SEG.Transversal.Common;
using Dapper;
using System.Data;

namespace API.SEG.Persistence.Repository
{
    public class AuthenticateRepository : IAuthenticateRepository
    {
        private readonly DapperContext _context;

        public AuthenticateRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<Response<dynamic>> Login(Usuario entidad)
        {
            Response<dynamic> retorno = new Response<dynamic>();

            try
            {
                using (var connection = _context.CreateConnection())
                {
                    var query = "dbo.USP_GET_USUARIO_LOGIN";

                    var parameters = new DynamicParameters();

                    parameters.Add("username", entidad.username);
                    parameters.Add("password", entidad.password);
                    parameters.Add("error", dbType: DbType.Boolean, direction: ParameterDirection.Output);
                    parameters.Add("message", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                    var result = await connection.QuerySingleOrDefaultAsync<dynamic>(query, param: parameters, commandType: CommandType.StoredProcedure);

                    retorno.Data = result ?? new Usuario();
                    retorno.Error = parameters.Get<bool?>("error") ?? false;
                    retorno.Message = parameters.Get<string>("message") ?? string.Empty;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retorno;
        }


        public async Task<Response<List<dynamic>>> GetListPerfiles(Usuario entidad)
        {
            Response<List<dynamic>> retorno = new Response<List<dynamic>>();

            try
            {
                using (var connection = _context.CreateConnection())
                {
                    var query = "dbo.USP_GET_USUARIO_PERFILES";

                    var parameters = new DynamicParameters();

                    parameters.Add("usuario_id", entidad.usuario_id.Equals(0) ? (int?)null : entidad.usuario_id);
                    parameters.Add("error", dbType: DbType.Boolean, direction: ParameterDirection.Output);
                    parameters.Add("message", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                    IEnumerable<dynamic> result = await connection.QueryAsync<dynamic>(query, param: parameters, commandType: CommandType.StoredProcedure);
                    List<dynamic> lista = result.ToList();

                    retorno.Data = lista;
                    retorno.Error = parameters.Get<bool?>("error") ?? false;
                    retorno.Message = parameters.Get<string>("message") ?? string.Empty;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retorno;
        }

        public async Task<Response<string>> GetListAccesos(int usuario_id, int perfil_id)
        {
            Response<string> retorno = new Response<string>();

            try
            {
                using (var connection = _context.CreateConnection())
                {
                    var query = "dbo.USP_GET_USUARIO_ACCESOS";

                    var parameters = new DynamicParameters();

                    parameters.Add("usuario_id", usuario_id.Equals(0) ? (int?)null : usuario_id);
                    parameters.Add("perfil_id", perfil_id.Equals(0) ? (int?)null : perfil_id);
                    parameters.Add("error", dbType: DbType.Boolean, direction: ParameterDirection.Output);
                    parameters.Add("message", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);
                    parameters.Add("usuario_accesos", dbType: DbType.String, direction: ParameterDirection.Output, size: int.MaxValue);

                    IEnumerable<dynamic> result = await connection.QueryAsync<dynamic>(query, param: parameters, commandType: CommandType.StoredProcedure);
                    List<dynamic> lista = result.ToList();

                    retorno.Data = parameters.Get<string>("usuario_accesos") ?? string.Empty; ;
                    retorno.Error = parameters.Get<bool?>("error") ?? false;
                    retorno.Message = parameters.Get<string>("message") ?? string.Empty;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retorno;
        }
    }
}
