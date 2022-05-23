using Microsoft.AspNetCore.Identity;

namespace Astromedia.Models;

public class Usuario : IdentityUser
{
    public Usuario()
    {
        FotoPerfil = "";
    }

    public Usuario(string fotoPerfil)
    {
        FotoPerfil = fotoPerfil;
    }

    public string FotoPerfil { get; set; }

    public List<Postagem> Postagens { get; set; }
}