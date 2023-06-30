using FluentValidation;
using FluentValidation.Results;
using PUB.Domain.Entities;
using PUB.Domain.Interfaces;

namespace PUB.Domain.Services
{
    public abstract class BaseService
    {
        private readonly INotificator _notificador;

        protected BaseService(INotificator notificador)
        {
            _notificador = notificador;
        }

        protected void Notify(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                Notify(error.ErrorMessage);
            }
        }

        protected void Notify(string mensagem)
        {
            _notificador.Handle(mensagem);
        }

        protected bool ExecValidate<TV, TE>(TV validacao, TE entidade) where TV : AbstractValidator<TE> where TE : EntityBase
        {
            var validator = validacao.Validate(entidade);

            if (validator.IsValid) return true;

            Notify(validator);

            return false;
        }
    }
}