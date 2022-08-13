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
            .GreaterThan(m => DateTime.UtcNow.AddYears(-120))
            .LessThan(m => DateTime.UtcNow.AddYears(-18))
            .WithName("Data de Nascimento");
    
        RuleFor(m => m.PasswordHash)
            .Length(6, 20)
            .Matches("^([a-zA-Z0-9_-])*$")
            .WithMessage("Senha deve conter apenas letras, números, underline e traço");
    }
}