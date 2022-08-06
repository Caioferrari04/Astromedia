using System.ComponentModel.DataAnnotations;

namespace Astromedia.DTO;

public class UsuarioDTO
{
    public string Nome { get; set; }
    
    [EmailAddress]
    public string Email { get; set; }

    [DataType(DataType.Password)]
    public string Senha { get; set; }

    [DataType(DataType.Password)]
    public string ConfirmarSenha { get; set; }

    [DataType(DataType.Date)]
    public DateTime DataNascimento { get; set; }

    public bool LembrarMe { get; set; }

    public bool ConditionTerms { get; set; }

    public bool PrivacyTerms { get; set; }

    public List<PostagemDTO> Postagens { get; set; }

}