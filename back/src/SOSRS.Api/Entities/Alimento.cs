using SOSRS.Api.ValueObjects;

namespace SOSRS.Api.Entities;

public class Alimento : Entity
{
    public Alimento(int id, int abrigoId, string nome, int? quantidadeNecessaria)
        : base(id)
    {
        AbrigoId = abrigoId;
        Nome = new SearchableStringVO(nome);
        QuantidadeNecessaria = quantidadeNecessaria;
    }

    //Ef
    private Alimento() { }

    public int AbrigoId { get; private set; } = default!;
    public SearchableStringVO Nome { get; private set; } = default!;
    public int? QuantidadeNecessaria { get; private set; } = default!;

    public Abrigo Abrigo { get; private set; } = default!;
}
