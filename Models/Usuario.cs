using Microsoft.AspNetCore.Identity;

namespace Astromedia.Models;

public class Usuario : IdentityUser
{
    public Usuario()
    {
        FotoPerfil = "";
        Postagens = new List<Postagem>();
    }

    public Usuario(string fotoPerfil, List<Postagem> postagens)
    {
        FotoPerfil = fotoPerfil;
        Postagens = postagens;
    }

    public string FotoPerfil { get; set; }
    public string FotoBackground { get; set; }

    public DateTime DataNascimento { get; set; }

    public List<Postagem> Postagens { get; set; }

    public List<Astro> Astros { get; set; }

    public bool isAdmin { get; set; }

    public string Bio { get; set; }

    public List<Like> Likes { get; set; }
    public List<Denuncia> Denuncias { get; set; }
}