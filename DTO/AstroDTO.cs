namespace Astromedia.DTO;

public class AstroDTO
{
    public int Id { get; set; }
    public string Curiosidades { get; set; }
    public string Nome { get; set; }
    public IFormFile Foto { get; set; }
    public string LinkFoto { get; set; }
    // public IFormFile Background { get; set; }
}