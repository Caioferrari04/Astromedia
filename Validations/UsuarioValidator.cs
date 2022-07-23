using Astromedia.Models;
using FluentValidation;
using FluentValidation.Validators;

public class UsuarioValidator : AbstractValidator<Usuario>
{
    public UsuarioValidator()
    {
        RuleFor(m => m.UserName)
            .Length(5, 20)
            .WithMessage("Nome de usuario muito grande");

        RuleFor(m => m.UserName)
            .Matches("^([a-zA-Z0-9_])*$")
            .WithMessage("Nome deve conter apenas letras, números e underline");

        RuleFor(m => m.Email)
            .EmailAddress(EmailValidationMode.AspNetCoreCompatible)
            .WithMessage("Email inválido");
        
        RuleFor(m => m.DataNascimento)
            .LessThan(m => DateTime.UtcNow.AddYears(-18))
            .GreaterThan(m => DateTime.UtcNow.AddYears(-150));
    }
}