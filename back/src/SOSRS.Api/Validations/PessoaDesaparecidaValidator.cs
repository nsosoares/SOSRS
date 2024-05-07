using FluentValidation;
using SOSRS.Api.Entities;

namespace SOSRS.Api.Validations;

public class PessoaDesaparecidaValidator : AbstractValidator<PessoaDesaparecida>
{
    public PessoaDesaparecidaValidator()
    {
        RuleFor(x => x.Nome).SetValidator(new SearchableStringValidador("Nome da pessoa"));
    }
}
