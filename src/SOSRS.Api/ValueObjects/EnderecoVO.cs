namespace SOSRS.Api.ValueObjects;

public class EnderecoVO
{
    public EnderecoVO()
    {
        
    }

    public EnderecoVO(
        string rua, 
        int numero, 
        string bairro, 
        string cidade, 
        string estado, 
        string complemento,
        string cep)
    {
        Rua = new SearchableStringVO(rua);
        Numero = numero;
        Bairro = new SearchableStringVO(bairro);
        Cidade = new SearchableStringVO(cidade);
        Estado = new SearchableStringVO(estado);
        Complemento = complemento;
        Cep = cep;
    }

    public SearchableStringVO Rua { get; private set; } = default!;
    public int Numero { get; private set; } = default!;
    public SearchableStringVO Bairro { get; private set; } = default!;
    public SearchableStringVO Cidade { get; private set; } = default!;
    public SearchableStringVO Estado { get; private set; } = default!;
    public string Complemento { get; private set; } = default!;
    public string Cep { get; private set; } = default!;
}