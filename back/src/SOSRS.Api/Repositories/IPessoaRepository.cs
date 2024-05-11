using SOSRS.Api.Entities;
using SOSRS.Api.ViewModels;

namespace SOSRS.Api.Repositories
{
    public interface IPessoaRepository
    {
        Task<List<PessoaDesaparecida>> Buscar(string parametroDeBusca);
        Task<bool> Criar(PessoaDesaparecida pessoa);
        Task<bool> RealocarPessoasDeAbrigo(int abrigoOrigem, int abrigoDestino);
    }
}
