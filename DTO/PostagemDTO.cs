namespace Astromedia.DTO;

public class PostagemDTO
{
    public int Id { get; set; }
    public string Texto { get; set; }
    public DateTime DataPostagem { get; set; }
    public IFormFile Imagem { get; set; }
    public string LinkImagem { get; set; }

    public string UsuarioId { get; set; }
    public int AstroId { get; set; }
}