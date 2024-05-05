using FluentValidation;
using SOSRS.Api.ValueObjects;

namespace SOSRS.Api.Validations;

public class SearchableStringValidador : AbstractValidator<SearchableStringVO>
{
    public SearchableStringValidador(string campo, int min = 3, int max = 150)
    {
        RuleFor(x => x.Value)
           .Cascade(CascadeMode.Stop)
           .NotEmpty().WithMessage($"O campo {campo} é obrigatório.")
           .NotNull().WithMessage($"O campo {campo} é obrigatório.")
           .Length(min, max).WithMessage($"O campo {campo} deve ter entre {min} a {max} caracteres.");
    }
}
