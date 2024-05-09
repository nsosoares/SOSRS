using SOSRS.Api.ValueObjects;

namespace SOSRS.Api.Entities
{
    public class PessoaDesaparecida : Entity
    {
        public PessoaDesaparecida(int id, int abrigoId, string nome, int? idade, string informacaoAdicional, IFormFile foto) : base(id)
        {
            AbrigoId = abrigoId;
            Nome = new SearchableStringVO(nome);
            Idade = idade;
            InformacaoAdicional = informacaoAdicional;
            //Ideia que eu tive, porém nenhum martelo batido
            if (foto is not null)
            {
                using var memoryStream = new MemoryStream();
                foto.CopyToAsync(memoryStream).RunSynchronously();
                Foto = memoryStream.ToArray();
            }
        }

        //Ef
        private PessoaDesaparecida() { }

        public int AbrigoId { get; private set; } = default!;
        public SearchableStringVO Nome { get; private set; } = default!;
        public Abrigo Abrigo { get; private set; } = default!;
        public int? Idade { get; set; } = default!;
        public string InformacaoAdicional { get; set; } = default!;
        public byte[]? Foto { get; set; } = default!;

        public void MoverParaAbrigo(int abrigoDestino)
        {
            AbrigoId = abrigoDestino;
        }
    }
}
