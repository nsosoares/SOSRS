using FluentValidation;
using SOSRS.Api.Entities;

namespace SOSRS.Api.Validations;

public class AbrigoValidador : AbstractValidator<Abrigo>
{
    public AbrigoValidador()
    {
        RuleFor(x => x.Endereco).SetValidator(new EnderecoValidador());
        RuleFor(x => x.Nome).SetValidator(new SearchableStringValidador("Nome do abrigo"));
        RuleForEach(x => x.Alimentos).SetValidator(new AlimentoValidador());
        RuleFor(x => x.Telefone)
             .NotEmpty().WithMessage("O campo Telefone é obrigatório.")
             .NotNull().WithMessage("O campo Telefone é obrigatório.")
             .Length(3, 50).WithMessage("O campo Telefone deve ter entre 3 a 150 caracteres.");

        //RuleFor(x => x.TipoChavePix)
        //    .NotEmpty().WithMessage("O campo Tipo Chave Pix é obrigatório.")
        //    .NotNull().WithMessage("O campo Tipo Chave Pix é obrigatório.")
        //    .Length(3, 150).WithMessage("O campo Tipo Chave Pix deve ter entre 3 a 150 caracteres.");

        //RuleFor(x => x.QuantidadeNecessariaVoluntarios)
        //    .NotNull().WithMessage("O campo Quantidade Necessária Voluntários é obrigatório.");

        //RuleFor(x => x.QuantidadeVagasDisponiveis)
        //  .NotNull().WithMessage("O campo Quandidade de vagas disponíveis é obrigatório.");
    }
}
