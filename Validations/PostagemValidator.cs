using Astromedia.DTO;
using FluentValidation;

namespace Astromedia.Validations;

public class PostagemValidator : AbstractValidator<PostagemDTO>
{
    public PostagemValidator()
    {
        RuleFor(p => p.Texto)
            .NotEmpty()
            .When(p => p.LinkImagem == null)
            .WithMessage("Postagem inválida");
        RuleFor(p => p.Texto)
            .Matches(@"^([1-9a-zA-ZáàâãéêíóôõöúçñÀÁÂÃÉÊÍÓÔÕÚÜÇ ?=.*@%'}{<>()[\]\"",.^?;#:~=+_*-.|/\\])\w*")
            .When(p => p.Texto != null);
        RuleFor(p => p.Texto)
            .MaximumLength(280);
    }
}