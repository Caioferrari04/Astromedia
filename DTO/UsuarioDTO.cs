using System.ComponentModel.DataAnnotations;

namespace Astromedia.DTO;

public class UsuarioDTO
{
    public string Nome { get; set; }
    
    [EmailAddress]
    public string Email { get; set; }
    [Display(Name = "Senha atual")]
    [DataType(DataType.Password)]
    public string SenhaAtual { get; set; }

    [DataType(DataType.Password)]
    public string Senha { get; set; }

    [Display(Name = "Confirmar senha")]
    [DataType(DataType.Password)]
    public string ConfirmarSenha { get; set; }

    [Display(Name = "Data de nascimento")]
    [DataType(DataType.Date)]
    public DateTime DataNascimento { get; set; }

    public bool LembrarMe { get; set; }

    public bool ConditionTerms { get; set; }

    public bool PrivacyTerms { get; set; }

    public string FotoPerfil { get; set; }

    public List<PostagemDTO> Postagens { get; set; }
    public bool Atualizar { get; set; }
}