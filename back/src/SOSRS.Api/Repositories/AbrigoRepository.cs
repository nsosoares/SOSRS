using Microsoft.EntityFrameworkCore;
using SOSRS.Api.Data;
using SOSRS.Api.Entities;
using SOSRS.Api.Helpers;
using SOSRS.Api.ViewModels;

namespace SOSRS.Api.Repositories
{
    public class AbrigoRepository : IAbrigoRepository
    {
        private readonly AppDbContext _database;

        public AbrigoRepository(AppDbContext database)
        {
            _database =
                database
                ?? throw new ArgumentNullException(nameof(database));
        }

        public async Task<Abrigo?> GetAbrigoPorIdAsync(int id)
        {
            try
            {
                var abrigo = await _database.Abrigos.FirstAsync(s => s.Id == id);

                return abrigo;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<Abrigo>> GetAbrigosPorIdsAsync(List<int> ids)
        {
            var abrigos = await _database.Abrigos.Where(s => ids.Contains(s.Id)).ToListAsync();

            return abrigos;
        }

        public async Task<List<AbrigoResponseViewModel>> GetAbrigos(FiltroAbrigoViewModel filtroAbrigoViewModel)
        {
            var abrigos = await _database.Abrigos.Where(x => x.TipoAbrigo == filtroAbrigoViewModel.TipoAbrigo )
            .When(!string.IsNullOrEmpty(filtroAbrigoViewModel.Nome) && !string.IsNullOrWhiteSpace(filtroAbrigoViewModel.Nome)
                        , x => x.Nome.SearchableValue.Contains(filtroAbrigoViewModel.Nome!.ToSearchable()))
            .When(!string.IsNullOrEmpty(filtroAbrigoViewModel.Cidade) && !string.IsNullOrWhiteSpace(filtroAbrigoViewModel.Cidade)
                        , x => x.Endereco.Cidade.SearchableValue.Contains(filtroAbrigoViewModel.Cidade!.ToSearchable()))
            .When(!string.IsNullOrEmpty(filtroAbrigoViewModel.Bairro) && !string.IsNullOrWhiteSpace(filtroAbrigoViewModel.Bairro)
                        , x => x.Endereco.Bairro.SearchableValue.Contains(filtroAbrigoViewModel.Bairro!.ToSearchable()))
            .When(filtroAbrigoViewModel.Capacidade != EFiltroStatusCapacidade.Todos && filtroAbrigoViewModel.Capacidade.HasValue
                        , x => (filtroAbrigoViewModel.Capacidade == EFiltroStatusCapacidade.Lotado && x.Lotado)
                            || (filtroAbrigoViewModel.Capacidade == EFiltroStatusCapacidade.Disponivel && !x.Lotado))
                    .When(filtroAbrigoViewModel.PrecisaAlimento.HasValue
                        , x => x.Alimentos.Count > 0 == filtroAbrigoViewModel.PrecisaAlimento)
                    .When(filtroAbrigoViewModel.PrecisaAjudante.HasValue
                        , x => (x.QuantidadeNecessariaVoluntarios.HasValue && x.QuantidadeNecessariaVoluntarios > 0) == filtroAbrigoViewModel.PrecisaAjudante)
            .When(!string.IsNullOrEmpty(filtroAbrigoViewModel.Alimento) && !string.IsNullOrWhiteSpace(filtroAbrigoViewModel.Alimento)
                        , x => !x.Alimentos.Any(a => a.Nome.SearchableValue.Contains(filtroAbrigoViewModel.Alimento!.ToSearchable())))
                    .Select(x => new AbrigoResponseViewModel
                    {
                        Id = x.Id,
                        Nome = x.Nome.Value,
                        Cidade = x.Endereco.Cidade.Value,
                        Bairro = x.Endereco.Bairro.Value,
                        Numero = x.Endereco.Numero,
                        Complemento = x.Endereco.Complemento,
                        TipoChavePix = x.TipoChavePix,
                        ChavePix = x.ChavePix,
                        Telefone = x.Telefone,
                        Capacidade = x.Lotado ? EStatusCapacidade.Lotado : EStatusCapacidade.Disponivel,
                        PrecisaAjudante = (x.QuantidadeNecessariaVoluntarios.HasValue && x.QuantidadeNecessariaVoluntarios > 0),
                        PrecisaAlimento = x.Alimentos.Count > 0,
                        UltimaAtualizacao = x.UltimaAtualizacao,
                        TipoAbrigo = x.TipoAbrigo,

                    })
                    .OrderBy(x => x.Cidade)
                    .ThenBy(x => x.Nome)
                    .ToListAsync();

            return abrigos;
        }
    }
}
