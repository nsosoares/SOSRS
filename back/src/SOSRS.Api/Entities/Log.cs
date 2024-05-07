namespace SOSRS.Api.Entities;

public class Log : Entity
{
    public Log(int id, Guid usuarioId, ETipoOperacao tipoOperacao, string json)
        : base(id)
    {
        UsuarioId = usuarioId;
        TipoOperacao = tipoOperacao;
        Json = json;
    }

    //Ef
    private Log()
    {
        
    }

    public Guid UsuarioId { get; private set; } = default!;
    public ETipoOperacao TipoOperacao { get; private set; } = default!;
    public string Json { get; private set; } = default!;
}

public enum ETipoOperacao
{
    Registrar,
    Atualizar,
    Deletar
}
