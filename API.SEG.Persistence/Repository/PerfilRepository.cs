using API.SEG.Aplicacion.Interface.Persistence;
using API.SEG.Transversal.Common;
using API.SEG.Domain.Entities;
using API.SEG.Persistence.Context;
using System.Data;
using Dapper;

namespace API.SEG.Persistence.Repository
{
    public class PerfilRepository : IPerfilRepository
    {
        private readonly DapperContext _context;

        public PerfilRepository(DapperContext context)
        {
            _context = context;
        }

        public Task<Response> Insert(Perfil entidad)
        {
            throw new NotImplementedException();
        }

        public Task<Response> Update(Perfil entidad)
        {
            throw new NotImplementedException();
        }

        public Task<Response> Delete(Perfil entidad)
        {
            throw new NotImplementedException();
        }

        public Task<Response<dynamic>> GetById(Perfil entidad)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<List<dynamic>>> GetList(Perfil entidad)
        {
            Response<List<dynamic>> retorno = new Response<List<dynamic>>();

            try
            {
                using (var connection = _context.CreateConnection())
                {
                    var query = "dbo.USP_SEL_PERFIL";

                    var parameters = new DynamicParameters();

                    parameters.Add("perfil_id", entidad.perfil_id.Equals(0) ? (int?)null : entidad.perfil_id);
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
                retorno.Error = true;
                retorno.Message = ex.Message;
            }

            return retorno;
        }

    }
}
