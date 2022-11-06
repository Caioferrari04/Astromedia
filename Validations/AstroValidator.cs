using Astromedia.DTO;
using FluentValidation;

namespace Astromedia.Validations;

public class AstroValidator : AbstractValidator<AstroDTO>
{
    public AstroValidator()
    {
        RuleFor(m => m.Nome)
            .NotEmpty()
            .WithMessage("Nome não pode ser vazio.")
            .Length(1, 20);
            

        RuleFor(m => m.Curiosidades)
            .NotEmpty()
            .WithMessage("Curiosidades não pode ser vazio.");

        RuleFor(m => m.Foto)
            .NotEmpty()
            .WithMessage("Insira uma foto");
    }
}