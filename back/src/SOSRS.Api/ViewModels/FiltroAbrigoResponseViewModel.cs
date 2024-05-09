
using SOSRS.Api.Enums;
using System.Text;

namespace SOSRS.Api.ViewModels;

public class FiltroAbrigoResponseViewModel
{
    public List<AbrigoResponseViewModel> Abrigos { get; set; } = default!;
    public int QuantidadeTotalRegistros { get; set; }
}

public class AbrigoResponseViewModel
{
    public int Id { get; set; } = default!;
    public string Nome { get; set; } = default!;
    public string Cidade { get; set; } = default!;
    public string Bairro { get; set; } = default!;
    public int? Numero { get; set; } = default!;
    public string Complemento { get; set; } = default!;
    public string Telefone { get; set; } = default!;
    public string TipoChavePix { get; set; } = default!;
    public string ChavePix { get; set; } = default!;
    public EStatusCapacidade Capacidade { get; set; } = default!;
    public TipoAbrigoEnum TipoAbrigo { get; set; }

    public bool PrecisaAjudante { get; set; } = default!;
    public bool PrecisaAlimento { get; set; } = default!;
    public DateTime? UltimaAtualizacao { get; set; } = default!;
    public string UltimaAtualizacaoTxt
    {
        get
        {
            if (!UltimaAtualizacao.HasValue)
                return "não coletada";

            var stringbuilder = new StringBuilder(4);
            var data = UltimaAtualizacao.Value;
            var dataAtual = DateTime.Now;

            var diferenca = dataAtual - data;
            if (diferenca.Minutes < 60)
            {
                stringbuilder.Append(diferenca.Minutes);
                stringbuilder.Append(" minutos");

                return stringbuilder.ToString();
            }

            if (diferenca.Hours < 24)
            {
                stringbuilder.Append(diferenca.Hours);
                stringbuilder.Append(" horas");

                return stringbuilder.ToString();
            }


            stringbuilder.Append(diferenca.Days);
            stringbuilder.Append(" dias e ");
            var horasPorDia = diferenca.Hours - (diferenca.Days * 24);

            stringbuilder.Append(horasPorDia);
            stringbuilder.Append(" horas");

            return stringbuilder.ToString();
        }
    }
}

public enum EStatusCapacidade
{
    Lotado,
    Disponivel
}