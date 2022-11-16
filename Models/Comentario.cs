namespace Astromedia.Models;

public class Comentario
{
    public int Id { get; set; }
    public string Texto { get; set; }
    public DateTime DataComentario { get; set; }
    public Usuario Usuario { get; set; }
    public Postagem Postagem { get; set; }

    public Comentario() { }
    public Comentario(string texto, DateTime dataComentario, Usuario usuario, Postagem postagem)
    {
        Texto = texto;
        DataComentario = dataComentario;
        Usuario = usuario;
        Postagem = postagem;
    }
}