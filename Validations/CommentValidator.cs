using Astromedia.DTO;
using FluentValidation;

namespace Astromedia.Validations;

public class CommentValidator : AbstractValidator<CommentDTO>
{
    public CommentValidator()
    {
        RuleFor(p => p.Texto)
            .NotEmpty()
            .WithMessage("Comentário inválido");
        RuleFor(p => p.Texto)
            .Matches(@"^([1-9a-zA-ZáàâãéêíóôõöúçñÀÁÂÃÉÊÍÓÔÕÚÜÇ ?=.*@%'}{<>()[\]\"",.^?;#:~=+_*-.|/\\])\w*")
            .When(p => p.Texto != null);
        RuleFor(p => p.Texto)
            .MaximumLength(280);
    }
}