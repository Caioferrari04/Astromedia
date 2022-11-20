using Astromedia.DTO;
using FluentValidation;
using FluentValidation.Validators;

namespace Astromedia.Validations;

public class UsuarioValidator : AbstractValidator<UsuarioDTO>
{
    public UsuarioValidator()
    {
        RuleSet("ValidacaoUpdateProfile", () => {
            RuleFor(m => m.Nome)
                .MinimumLength(4)
                .WithMessage("Nome de usuario muito pequeno.");

            RuleFor(m => m.Nome)
                .MaximumLength(15)
                .WithMessage("Nome de usuario muito grande.");

            RuleFor(m => m.Nome)
                .Matches("^([a-zA-Z0-9_])*$")
                .WithMessage("Nome deve conter apenas letras, números e underline.");

            RuleFor(m => m.Bio)
                .Matches(@"^([1-9a-zA-ZáàâãéêíóôõöúçñÀÁÂÃÉÊÍÓÔÕÚÜÇ ?=.*@%'}{<>()[\]\"",.^?;#:~=+_*-.|/\\])\w*")
                .WithMessage("Bio inválida.");

            RuleFor(m => m.Email)
                .EmailAddress(EmailValidationMode.AspNetCoreCompatible)
                .WithMessage("Email inválido.");
            
            RuleFor(m => m.DataNascimento)
                .LessThan(m => DateTime.UtcNow.AddYears(-13))
                .WithMessage("Você deve ser possuir mais que 13 anos para se cadastrar.");

            RuleFor(m => m.DataNascimento)
                .GreaterThan(m => DateTime.UtcNow.AddYears(-120))
                .WithMessage("Data de nascimento inválida.");
        });

        RuleSet("ValidacaoSenha", () => {
            RuleFor(m => m.Senha)
                .MinimumLength(6)
                .WithMessage("Senha pequena demais.");
            
            RuleFor(m => m.Senha)
                .MaximumLength(20)
                .WithMessage("Senha grande demais!");

            RuleFor(m => m.Senha)
                .Matches("^([a-zA-Z0-9_-])*$")
                .WithMessage("Senha deve conter apenas letras, números, underline e traço.");

            RuleFor(m => m.ConfirmarSenha)
                .Equal(m => m.Senha)
                .WithMessage("Senha de confirmação não é igual à senha inserida.");
                
            RuleFor(m => m.SenhaAtual)
                .Length(6, 20)
                .Matches("^([a-zA-Z0-9_-])*$")
                .WithMessage("Senha atual deve conter apenas letras, números, underline e traço.");
        });

        RuleSet("ValidacaoTermos", () => {
            RuleFor(v => v.PrivacyTerms)
                .Equal(true).WithMessage("Aceite os termos de privacidade para continuar.");

            RuleFor(v => v.ConditionTerms)
                .Equal(true).WithMessage("Aceite os termos de condição para continuar.");
        });

        RuleSet("ValidacaoEmail", () => {
            RuleFor(m => m.NovoEmail)
            .EmailAddress(EmailValidationMode.AspNetCoreCompatible)
            .WithMessage("Email inválido.");
            
            RuleFor(m => m.SenhaAtual)
                .Length(6, 20)
                .Matches("^([a-zA-Z0-9_-])*$")
                .WithMessage("Senha atual deve conter apenas letras, números, underline e traço.");
        });
    

    }
}