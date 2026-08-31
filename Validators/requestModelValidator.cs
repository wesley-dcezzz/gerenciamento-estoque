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
                .NotEmpty().WithMessage("O preço é obrigatório.")
                .Must(precoStr =>
                {
                    if (string.IsNullOrEmpty(precoStr)) return false;

                    //Lógica de limpeza da string do preço
                    string precoLimpo = precoStr.Replace("R$", "").Replace(" ", "").Replace(".", "").Replace(",", ".").Trim();

                    //Tenta converter para decimal
                    return decimal.TryParse(precoLimpo, out decimal precoDecimal) && precoDecimal > 0;
                })
                .WithMessage("o preço informado é inválido ou menor que zero");
        }
    }
}
