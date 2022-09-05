using Astromedia.DTO;
using FluentValidation;
using FluentValidation.Validators;

public class UsuarioValidator : AbstractValidator<UsuarioDTO>
{
    public UsuarioValidator()
    {
        RuleFor(m => m.Nome)
            .Length(5, 20)
            .WithMessage("Nome de usuario muito grande");

        RuleFor(m => m.Nome)
            .Matches("^([a-zA-Z0-9_])*$")
            .WithMessage("Nome deve conter apenas letras, números e underline");

        RuleFor(m => m.Email)
            .EmailAddress(EmailValidationMode.AspNetCoreCompatible)
            .WithMessage("Email inválido");
        
        RuleFor(m => m.DataNascimento)
            .LessThan(m => DateTime.UtcNow.AddYears(-13))
            .WithMessage("Você deve ser possuir mais que 13 anos para se cadastrar.");

        RuleFor(m => m.DataNascimento)
            .GreaterThan(m => DateTime.UtcNow.AddYears(-120))
            .WithMessage("Data de nascimento inválida.");
    
        RuleFor(m => m.Senha)
            .Length(6, 20)
            .Matches("^([a-zA-Z0-9_-])*$")
            .WithMessage("Senha deve conter apenas letras, números, underline e traço");

        RuleFor(m => m.ConfirmarSenha)
            .Equal(m => m.Senha)
            .WithMessage("Senha de confirmação não é igual à senha inserida");

        RuleFor(v => v.PrivacyTerms).Equal(true).WithMessage("Aceite os termos de privacidade para continuar");
        RuleFor(v => v.ConditionTerms).Equal(true).WithMessage("Aceite os termos de condição para continuar");
    }
}