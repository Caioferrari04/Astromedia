namespace Astromedia.Models;

public class Postagem
{
    public int Id { get; set; }

    public string Texto { get; set; }

    public DateTime DataPostagem { get; set; }

    public Usuario Usuario { get; set; }

    public Astro Astro { get; set; }

    public Postagem()
    {
        Texto = "";
        DataPostagem = DateTime.Now;
        Usuario = new Usuario();
    }

    public Postagem(string texto, DateTime dataPostagem, Usuario usuario)
    {
        Texto = texto;
        DataPostagem = dataPostagem;
        Usuario = usuario;
    }
}