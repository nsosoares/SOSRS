using FluentValidation;
using FluentValidation.Results;
using SOSRS.Api.Entities;

namespace SOSRS.Api.Services;

public interface IValidadorService
{
    ValidationResult Validar<TEntity, TValidador>(TEntity entity, TValidador validador)
        where TEntity : Entity
        where TValidador : AbstractValidator<TEntity>;
}
