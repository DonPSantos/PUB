using FluentValidation;
using PUB.Domain.Entities.Validations.Docs;

namespace PUB.Domain.Entities.Validations
{
    public class OneDrinkPromoValidate : AbstractValidator<OneDrinkPromo>
    {
        public OneDrinkPromoValidate()
        {
            RuleFor(f => f.Cpf.Length).Equal(CpfValidate.TamanhoCpf)
                .WithMessage("O campo Documento precisa ter {ComparisonValue} caracteres e foi fornecido {PropertyValue}.");

            RuleFor(f => CpfValidate.Validar(f.Cpf)).Equal(true)
                .WithMessage("O documento fornecido é inválido.");

            RuleFor(c => c.Cpf)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(2, 200).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(2, 200).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(c => c.Birth)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .LessThan(DateTime.Now.AddYears(-15)).WithMessage("A idade minima é 15 anos.");
        }
    }
}