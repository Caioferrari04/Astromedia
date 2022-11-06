using Astromedia.DTO;
using FluentValidation;
using FluentValidation.Validators;

public class UsuarioValidator : AbstractValidator<UsuarioDTO>
{
    public UsuarioValidator()
    {
        RuleFor(m => m.Nome)
            .Length(5, 20)
            .WithMessage("Nome de usuario muito grande")
            .Unless(m => m.Atualizar == true && m.Senha != null);

        RuleFor(m => m.Nome)
            .Matches("^([a-zA-Z0-9_])*$")
            .WithMessage("Nome deve conter apenas letras, números e underline")
            .Unless(m => m.Atualizar == true && m.Senha != null);

        RuleFor(m => m.Email)
            .EmailAddress(EmailValidationMode.AspNetCoreCompatible)
            .WithMessage("Email inválido")
            .Unless(m => m.Atualizar == true && m.Senha != null);
        
        RuleFor(m => m.DataNascimento)
            .LessThan(m => DateTime.UtcNow.AddYears(-13))
            .WithMessage("Você deve ser possuir mais que 13 anos para se cadastrar.")
            .Unless(m => m.Atualizar == true && m.Senha != null);

        RuleFor(m => m.DataNascimento)
            .GreaterThan(m => DateTime.UtcNow.AddYears(-120))
            .WithMessage("Data de nascimento inválida.")
            .Unless(m => m.Atualizar == true && m.Senha != null);
    
        RuleFor(m => m.Senha)
            .Length(6, 20)
            .Matches("^([a-zA-Z0-9_-])*$")
            .WithMessage("Senha deve conter apenas letras, números, underline e traço")
            .Unless(m => m.Atualizar == true && m.Nome != null);

        RuleFor(m => m.ConfirmarSenha)
            .Equal(m => m.Senha)
            .WithMessage("Senha de confirmação não é igual à senha inserida")
            .Unless(m => m.Atualizar == true && m.Nome != null);
         RuleFor(m => m.SenhaAtual)
            .Length(6, 20)
            .Matches("^([a-zA-Z0-9_-])*$")
            .WithMessage("Senha atual deve conter apenas letras, números, underline e traço")
            .Unless(m => m.Atualizar == false || m.Nome != null);

        RuleFor(v => v.PrivacyTerms)
            .Equal(true).WithMessage("Aceite os termos de privacidade para continuar")
            .Unless(m => m.Atualizar == true);
        RuleFor(v => v.ConditionTerms)
            .Equal(true).WithMessage("Aceite os termos de condição para continuar")
            .Unless(m => m.Atualizar == true);
    }
}