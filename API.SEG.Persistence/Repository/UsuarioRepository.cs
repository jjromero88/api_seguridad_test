using API.SEG.Aplicacion.Interface.Persistence;
using API.SEG.Domain.Entities;
using API.SEG.Persistence.Context;
using API.SEG.Transversal.Common;
using System.Data;
using Dapper;

namespace API.SEG.Persistence.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly DapperContext _context;

        public UsuarioRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<Response> Insert(Usuario entidad)
        {
            Response retorno = new Response();

            try
            {
                using (var connection = _context.CreateConnection())
                {
                    var query = "dbo.USP_INS_USUARIO";

                    var parameters = new DynamicParameters();

                    parameters.Add("username", entidad.username);
                    parameters.Add("password", entidad.password);
                    parameters.Add("numdocumento", entidad.numdocumento);
                    parameters.Add("nombrecompleto", entidad.nombrecompleto);
                    parameters.Add("email", entidad.email);
                    parameters.Add("perfiles", entidad.perfiles_id);
                    parameters.Add("usuario_reg", entidad.usuario_reg);
                    parameters.Add("error", dbType: DbType.Boolean, direction: ParameterDirection.Output);
                    parameters.Add("message", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                    var result = await connection.ExecuteAsync(query, param: parameters, commandType: CommandType.StoredProcedure);

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

        public async Task<Response> Update(Usuario entidad)
        {
            Response retorno = new Response();

            try
            {
                using (var connection = _context.CreateConnection())
                {
                    var query = "dbo.USP_UPD_USUARIO";

                    var parameters = new DynamicParameters();

                    parameters.Add("usuario_id", entidad.usuario_id.Equals(0) ? (int?)null : entidad.usuario_id);
                    parameters.Add("username", entidad.username);
                    parameters.Add("password", entidad.password);
                    parameters.Add("numdocumento", entidad.numdocumento);
                    parameters.Add("nombrecompleto", entidad.nombrecompleto);
                    parameters.Add("email", entidad.email);
                    parameters.Add("habilitado", entidad.habilitado);
                    parameters.Add("perfiles", entidad.perfiles_id);
                    parameters.Add("usuario_act", entidad.usuario_act);
                    parameters.Add("error", dbType: DbType.Boolean, direction: ParameterDirection.Output);
                    parameters.Add("message", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                    var result = await connection.ExecuteAsync(query, param: parameters, commandType: CommandType.StoredProcedure);

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

        public async Task<Response> Delete(Usuario entidad)
        {
            Response retorno = new Response();

            try
            {
                using (var connection = _context.CreateConnection())
                {
                    var query = "dbo.USP_DEL_USUARIO";

                    var parameters = new DynamicParameters();

                    parameters.Add("usuario_id", entidad.usuario_id);
                    parameters.Add("usuario_act", entidad.usuario_act);
                    parameters.Add("error", dbType: DbType.Boolean, direction: ParameterDirection.Output);
                    parameters.Add("message", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                    var result = await connection.ExecuteAsync(query, param: parameters, commandType: CommandType.StoredProcedure);

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

        public async Task<Response<dynamic>> GetById(Usuario entidad)
        {
            Response<dynamic> retorno = new Response<dynamic>();

            try
            {
                using (var connection = _context.CreateConnection())
                {
                    var query = "dbo.USP_GET_USUARIO";

                    var parameters = new DynamicParameters();

                    parameters.Add("usuario_id", entidad.usuario_id.Equals(0) ? (int?)null : entidad.usuario_id);
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

        public async Task<Response<List<dynamic>>> GetList(Usuario entidad)
        {
            Response<List<dynamic>> retorno = new Response<List<dynamic>>();

            try
            {
                using (var connection = _context.CreateConnection())
                {
                    var query = "dbo.USP_SEL_USUARIO";

                    var parameters = new DynamicParameters();

                    parameters.Add("usuario_id", entidad.usuario_id.Equals(0) ? (int?)null : entidad.usuario_id);
                    parameters.Add("numdocumento", entidad.numdocumento);
                    parameters.Add("habilitado", entidad.habilitado);
                    parameters.Add("filtro", entidad.filtro);
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
    }
}
