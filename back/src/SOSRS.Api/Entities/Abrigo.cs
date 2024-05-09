using SOSRS.Api.Enums;
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
        string telefone,
        string? observacao,
        Guid usuarioId,
        EnderecoVO endereco,
        List<Alimento> alimentos,
        List<PessoaDesaparecida> pessoasDesaparecidas,
        TipoAbrigoEnum tipoAbrigo)
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
        Telefone = telefone;
        UsuarioId = usuarioId;
        Endereco = endereco;
        Alimentos = alimentos;
        PessoasDesaparecidas = pessoasDesaparecidas;
        Lotado = quantidadeVagasDisponiveis == 0;
        UltimaAtualizacao = DateTime.Now;
        TipoAbrigo = tipoAbrigo;
    }

    //Ef
    private Abrigo() { }

    public SearchableStringVO Nome { get; private set; } = default!;
    public Guid GuidId { get; private set; } = Guid.NewGuid();
    public int? QuantidadeNecessariaVoluntarios { get; private set; } = default!;
    public int? QuantidadeVagasDisponiveis { get; private set; } = default!;
    public int? CapacidadeTotalPessoas { get; private set; } = default!;
    public string? TipoChavePix { get; private set; } = default!;
    public string? ChavePix { get; private set; } = default!;
    public string Telefone { get; private set; } = default!;
    public bool PermiteAnimais { get; private set; } = false!;
    public TipoAbrigoEnum TipoAbrigo { get; private set; } = TipoAbrigoEnum.Geral;
    public EnderecoVO Endereco { get; private set; } = default!;
    public string? Observacao { get; private set; } = default!;
    public List<Alimento> Alimentos { get; private set; } = default!;
    public List<Animal> Animais { get; private set; } = default!;
    public List<PessoaDesaparecida> PessoasDesaparecidas { get; private set; } = default!;
    public bool Lotado { get; private set; } = default!;
    public Guid UsuarioId { get; private set; } = default!;
    public DateTime? UltimaAtualizacao { get; private set; }

    public Usuario? Usuario { get; private set; } = default!;

    public void AddAlimento(Alimento alimento)
    {
        Alimentos.Add(alimento);
    }

    public void RemoverAlimento(int alimentoId)
    {
        Alimentos.Remove(Alimentos.FirstOrDefault(x => x.Id == alimentoId)!);
    }

    public void AddPessoaDesaparecida(PessoaDesaparecida pessoaDesaparecida)
    {
        PessoasDesaparecidas.Add(pessoaDesaparecida);
    }

    public void RemoverPessoaDesaparecida(int pessoaDesaparecidaId)
    {
        PessoasDesaparecidas.Remove(PessoasDesaparecidas.FirstOrDefault(x => x.Id == pessoaDesaparecidaId)!);
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
