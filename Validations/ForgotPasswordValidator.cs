using Astromedia.DTO;
using FluentValidation;
using FluentValidation.Validators;

namespace Astromedia.Validations;

public class ForgotPasswordValidator : AbstractValidator<ForgotPassword>
{
    public ForgotPasswordValidator()
    {
        RuleFor(m => m.Email)
            .NotEmpty()
            .WithMessage("Email não pode ser vazio.");
            
        RuleFor(m => m.Email)
            .EmailAddress(EmailValidationMode.AspNetCoreCompatible)
            .WithMessage("Email inválido");
    }
}