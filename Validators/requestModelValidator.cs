using controleEstoque.Models;
using FluentValidation;

namespace controleEstoque.Validators
{
    public class requestModelValidator : AbstractValidator<requestModel>
    {
        public requestModelValidator() 
        { 
            RuleFor(x => x.nome)
                .NotEmpty().WithMessage("O nome do produto é obrigatório.")
                .MinimumLength(2).WithMessage("O nome do produto deve ter pelo menos 2 caracteres.");

            RuleFor(x => x.categoria)
                .NotEmpty().WithMessage("A categoria do produto é obrigatória.");

            RuleFor(x => x.preco)
                .GreaterThan(0)
                .WithMessage("O preço do produto deve ser maior que zero.");
        }
    }
}
