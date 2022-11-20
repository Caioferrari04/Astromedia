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
        RuleFor(m => m.FotoBackground)
            .NotEmpty()
            .WithMessage("Insira uma foto de background");
        RuleForEach(m => m.MarcosHistoricos)
            .NotEmpty();
        RuleForEach(m => m.MarcosHistoricos)
            .Length(5, 200)
            .WithMessage("Marcos históricos deve ter entre 200 e 5 caracteres");
    }
}