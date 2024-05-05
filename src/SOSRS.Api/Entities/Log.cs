namespace SOSRS.Api.Entities;

public class Log : Entity
{
    public Log(int id, int codAcesso, ETipoOperacao tipoOperacao, string json)
        : base(id)
    {
        CodAcesso = codAcesso;
        TipoOperacao = tipoOperacao;
        Json = json;
    }

    //Ef
    private Log()
    {
        
    }

    public int CodAcesso { get; private set; } = default!;
    public ETipoOperacao TipoOperacao { get; private set; } = default!;
    public string Json { get; private set; } = default!;
}

public enum ETipoOperacao
{
    Registrar,
    Atualizar,
    Deletar
}
