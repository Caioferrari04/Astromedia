namespace Astromedia.DTO;

public class CommentDTO
{
    public int Id { get; set; }
    public string Texto { get; set; }
    public DateTime DataComentario { get; set; }

    public string UsuarioId { get; set; }
    public int PostId { get; set; }
}