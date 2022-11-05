namespace Astromedia.DTO;

public class PostagemDTO 
{
    public string Texto { get; set; }
    public DateTime DataPostagem { get; set; }
    public string Imagem { get; set; }
    public string UsuarioId { get; set; }
    public int AstroId { get; set; }
}