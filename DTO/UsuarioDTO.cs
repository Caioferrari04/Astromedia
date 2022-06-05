using System.ComponentModel.DataAnnotations;

namespace Astromedia.DTO;

public class UsuarioDTO 
{
    public string Nome { get; set; }

    public string Email { get; set; }

    public string Senha { get; set; }

    public string ConfirmarSenha { get; set; }

    public DateTime DataNascimento { get; set; }

    public bool LembrarMe { get; set; }

    public List<PostagemDTO> Postagens { get; set; }

}