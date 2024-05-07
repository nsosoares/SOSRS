using Microsoft.EntityFrameworkCore;
using SOSRS.Api.Data;
using SOSRS.Api.Entities;
using SOSRS.Api.Helpers;
using SOSRS.Api.ViewModels;

namespace SOSRS.Api.Repositories
{
    public class PessoaRepository : IPessoaRepository
    {
        private readonly AppDbContext _database;

        public PessoaRepository(AppDbContext database)
        {
            _database =
                database
                ?? throw new ArgumentNullException(nameof(database));
        }

        public async Task<List<PessoaDesaparecida>> Buscar(string parametroDeBusca)
        {
            var resultado =
                await _database
                    .PessoasDesaparecidas
                    .AsNoTracking()
                    .Where(s => s.Nome.SearchableValue.Contains(parametroDeBusca.ToSearchable()))
                    .ToListAsync();

            return resultado;
        }

        public async Task<bool> Criar(PessoaDesaparecida pessoa)
        {
            var registradoComSucesso = await _database.PessoasDesaparecidas.AddAsync(pessoa);

            if(registradoComSucesso.State == EntityState.Added)
            {
                await _database.SaveChangesAsync();

                return true;
            }

            return false;
        }
    }
}
