using Astromedia.DTO;
using FluentValidation;

namespace Astromedia.Validations;

public class ResetPasswordValidator : AbstractValidator<ResetPassword>
{
    public ResetPasswordValidator()
    {
        RuleFor(m => m.Senha)
            .Length(6, 20)
            .Matches("^([a-zA-Z0-9_-])*$")
            .WithMessage("Senha deve conter apenas letras, números, underline e traço");

        RuleFor(m => m.ConfirmarSenha)
            .Equal(m => m.Senha)
            .WithMessage("Senha de confirmação não é igual à senha inserida");
    }
}