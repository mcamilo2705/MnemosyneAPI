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
                .NotNull().WithMessage("Titulo e obrigatorio")
                .Length(3, 100).WithMessage("O titulo precisa ter entre 3 e 100 caracteres");

            RuleFor(x => x.Description)
                .NotNull().WithMessage("Descrição é obrigatória")
                .NotEmpty().WithMessage("Descrição não pode ser vazio");

            RuleFor(x => x.Images)
               .NotNull().WithMessage("Imagem é obrigatória")
               .NotEmpty().WithMessage("Imagem não pode ser vazio");
        }
    }
}
