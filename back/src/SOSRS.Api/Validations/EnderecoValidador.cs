using FluentValidation;
using SOSRS.Api.ValueObjects;

namespace SOSRS.Api.Validations;

public class EnderecoValidador : AbstractValidator<EnderecoVO>
{
    public EnderecoValidador()
    {
        //RuleFor(x => x.Rua).SetValidator(new SearchableStringValidador("Rua"));
        RuleFor(x => x.Cidade).SetValidator(new SearchableStringValidador("Cidade"));
        //RuleFor(x => x.Estado).SetValidator(new SearchableStringValidador("Estado"));

        RuleFor(x => x.Bairro).SetValidator(new SearchableStringValidador("Bairro"));

        //RuleFor(x => x.Numero)
        //    .NotNull().WithMessage("O campo número é obrigatório")
        //    .GreaterThan(0).WithMessage("O campo número deve ser maior que zero");
    }
}
