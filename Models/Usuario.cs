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

    public List<Postagem> Postagens { get; set; }
}