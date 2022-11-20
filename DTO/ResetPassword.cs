using System.ComponentModel.DataAnnotations;

namespace Astromedia.DTO;

public class ResetPassword
{
    public string Email { get; set; }
    public string Senha { get; set; }
    [Display(Name = "Confirmar senha")]
    public string ConfirmarSenha { get; set; }
    public string Token { get; set; }
}
