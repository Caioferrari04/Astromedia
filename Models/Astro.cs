namespace Astromedia.Models;

public class Astro
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Curiosidades { get; set; }
    public List<Usuario> usuarios { get; set; }
    public List<Postagem> postagens { get; set; }
}