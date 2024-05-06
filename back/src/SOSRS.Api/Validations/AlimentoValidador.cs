using FluentValidation;
using SOSRS.Api.Entities;

namespace SOSRS.Api.Validations;

public class AlimentoValidador : AbstractValidator<Alimento>
{
    public AlimentoValidador()
    {
        RuleFor(x => x.Nome).SetValidator(new SearchableStringValidador("Nome do alimento"));

        //RuleFor(x => x.QuantidadeNecessaria)
        //    .NotNull().WithMessage("O campo Quantidade Necessária é obrigatório.")
        //    .GreaterThan(0).WithMessage("O campo Quantidade Necessária precisa ser maior que zero.");
    }
}
