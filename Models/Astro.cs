namespace Astromedia.Models;

public class Astro
{
    public Astro() { }
    public Astro(string nome, string curiosidades, string foto, string fotoBackground)
    {
        Id = 0;
        Nome = nome;
        Curiosidades = curiosidades;
        Foto = foto;
        Usuarios = new();
        Postagens = new();
        FotoBackground = fotoBackground;
    }
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Curiosidades { get; set; }
    public string Foto { get; set; }
    public string FotoBackground { get; set; }
    public List<Usuario> Usuarios { get; set; }
    public List<Postagem> Postagens { get; set; }
}