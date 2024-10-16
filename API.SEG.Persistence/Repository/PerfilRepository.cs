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
            throw new NotImplementedException();
        }

    }
}
