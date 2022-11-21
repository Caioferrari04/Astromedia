namespace Astromedia.Models;

public class Postagem
{
    public Postagem() { }
    public Postagem(string texto, DateTime dataPostagem, string imagem, Usuario usuario, Astro astro)
    {
        Texto = texto;
        DataPostagem = dataPostagem;
        Imagem = imagem;
        Usuario = usuario;
        Astro = astro;
    }
    
    public int Id { get; set; }
    public string Texto { get; set; }
    public string Imagem { get; set; }
    public DateTime DataPostagem { get; set; }
    public Usuario Usuario { get; set; }
    public Astro Astro { get; set; }
    public List<Comentario> Comentarios { get; set; }
    public List<Like> Likes { get; set; }
}