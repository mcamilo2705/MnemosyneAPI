using FluentValidation;
using MnemosyneAPI.Model;

namespace MnemosyneAPI.Validators
{
    public class MemoryValidator : AbstractValidator<Memory>
    {
        public MemoryValidator() 
        {
            // Defino as regras de validação
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Informe uma Categoria")
                .Length(3, 150).WithMessage("O titulo precisa ter entre 3 e 100 caracteres");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Informe o Nome do Produto")
                .Length(3, 254).WithMessage("A descricao precisa ter entre 3 e 254 caracteres");

        }
    }
}
