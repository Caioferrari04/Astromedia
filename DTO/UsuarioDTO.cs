using System.ComponentModel.DataAnnotations;

namespace Astromedia.DTO;

public class UsuarioDTO
{
    [Required(ErrorMessage = "O {0} é necessário.")]
    [DataType(DataType.Text)]
    [StringLength(20, ErrorMessage = "O {0} deve ter pelo menos {2} e no máximo {1} caracteres.", MinimumLength = 5)]
    public string Nome { get; set; }
    
    [EmailAddress]
    public string Email { get; set; }

    [Required(ErrorMessage = "A {0} é necessária.")]
    [StringLength(20, ErrorMessage = "A {0} deve ter pelo menos {2} e no máximo {1} caracteres.", MinimumLength = 5)]
    [DataType(DataType.Password)]
    public string Senha { get; set; }

    [DataType(DataType.Password)]
    [StringLength(20, ErrorMessage = "A {0} deve ter pelo menos {2} e no máximo {1} caracteres.", MinimumLength = 5)]
    [Display(Name = "Confirmar senha")]
    public string ConfirmarSenha { get; set; }

    [DataType(DataType.Date)]
    public DateTime DataNascimento { get; set; }

    public bool LembrarMe { get; set; }

    public List<PostagemDTO> Postagens { get; set; }

}