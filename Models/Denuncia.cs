namespace Astromedia.Models;

public class Denuncia 
{
    public int Id { get; set; }
    public Usuario Usuario { get; set; }
    public Postagem Postagem { get; set; } = null!;
    public Comentario Comentario { get; set; } = null!;
    public string Conteudo { get; set; }
    public bool Respondida { get; set; }
}
