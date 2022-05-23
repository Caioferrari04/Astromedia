namespace Astromedia.Models;

public class Postagem
{
    public int Id { get; set; }

    public string Texto { get; set; }

    public DateTime DataPostagem { get; set; }

    public Usuario Usuario { get; set; }

    public int UsuarioId { get; set; }

    public Postagem()
    {
        Texto = "";
        DataPostagem = DateTime.Now;
    }

    public Postagem(string texto, DateTime dataPostagem)
    {
        Texto = texto;
        DataPostagem = dataPostagem;
    }
}