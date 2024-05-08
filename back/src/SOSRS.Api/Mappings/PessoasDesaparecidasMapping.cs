using SOSRS.Api.Entities;
using SOSRS.Api.ViewModels;

namespace SOSRS.Api.Mappings
{
    public static class PessoasDesaparecidasMapping
    {
        public static PessoaDesaparecida MapToPessoaDesaparecidaModel(this PessoaDesaparecidaViewModel pessoa)
        {
            var newPessoa = 
                new PessoaDesaparecida(
                    pessoa.Id, 
                    pessoa.AbrigoId,
                    pessoa.Nome, 
                    pessoa.Idade, 
                    pessoa.InformacaoAdicional, 
                    (pessoa.Foto ?? null!));

            return newPessoa;
        }
    }
}
