using System.ComponentModel.DataAnnotations;

namespace Astromedia.DTO;

public class UsuarioDTO
{
    [Display(Name = "Nome de usuário")]
    public string Nome { get; set; }
    
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

    [Display(Name = "Foto de perfil")]
    public IFormFile FotoPerfil { get; set; }
    
    [Display(Name = "Capa de perfil")]
    public IFormFile FotoBackground { get; set; }

    public List<PostagemDTO> Postagens { get; set; }
    public bool Atualizar { get; set; }
    public string Bio { get; set; }
    [Display(Name = "Novo Email")]
    public string NovoEmail { get; set; }
}