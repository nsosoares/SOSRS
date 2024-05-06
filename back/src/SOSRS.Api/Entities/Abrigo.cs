using SOSRS.Api.ValueObjects;

namespace SOSRS.Api.Entities;

public class Abrigo : Entity
{
    public Abrigo(
        int id, 
        string nome, 
        int? quantidadeNecessariaVoluntarios, 
        int? quantidadeVagasDisponiveis,
        int? capacidadeTotalPessoas,
        string tipoChavePix, 
        string chavePix, 
        string observacao, 
        EnderecoVO endereco, 
        List<Alimento> alimentos)
        : base(id)
    {
        Id = id;
        Nome = new SearchableStringVO(nome);
        QuantidadeNecessariaVoluntarios = quantidadeNecessariaVoluntarios;
        QuantidadeVagasDisponiveis = quantidadeVagasDisponiveis;
        CapacidadeTotalPessoas = capacidadeTotalPessoas;
        TipoChavePix = tipoChavePix;
        Observacao = observacao;
        ChavePix = chavePix;
        Endereco = endereco;
        Alimentos = alimentos;
        Lotado = quantidadeVagasDisponiveis == 0;
    }

    //Ef
    private Abrigo() { }

    public SearchableStringVO Nome { get; private set; } = default!;
    public int? QuantidadeNecessariaVoluntarios { get; private set; } = default!;
    public int? QuantidadeVagasDisponiveis { get; private set; } = default!;
    public int? CapacidadeTotalPessoas { get; private set; } = default!;
    public string TipoChavePix { get; private set; } = default!;
    public string ChavePix { get; private set; } = default!;
    public EnderecoVO Endereco { get; private set; } = default!;
    public string Observacao { get; private set; } = default!;
    public List<Alimento> Alimentos { get; private set; } = default!;
    public bool Lotado { get; private set; } = default!;

    public void AddAlimento(Alimento alimento)
    {
        Alimentos.Add(alimento);
    }

    public void RemoverAlimento(int alimentoId)
    {
        Alimentos.Remove(Alimentos.FirstOrDefault(x => x.Id == alimentoId)!);
    }

    public void AumentarQuantidadeVagasDisponiveis(int quantidadeVagasDisponiveis)
    {
        QuantidadeVagasDisponiveis += quantidadeVagasDisponiveis;
        Lotado = QuantidadeVagasDisponiveis == 0; 
    }

    public void DiminiurQuantidadeVagasDisponiveis(int quantidadeVagasDisponiveis)
    {
        if (QuantidadeVagasDisponiveis == 0) return;
        QuantidadeVagasDisponiveis -= quantidadeVagasDisponiveis;
        Lotado = QuantidadeVagasDisponiveis == 0;
    }
}
