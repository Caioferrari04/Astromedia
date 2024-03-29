namespace Astromedia.DTO;

public class AstroDTO
{
    public int Id { get; set; }
    public string Curiosidades { get; set; }
    public List<string> MarcosHistoricos { get; set; }
    public string Nome { get; set; }
    public IFormFile Foto { get; set; }
    public IFormFile FotoBackground { get; set; }
    public List<IFormFile> Fotos { get; set; }
    public string LinkFoto { get; set; }
    public string LinkFotoBackground { get; set; }
    // public IFormFile Background { get; set; }
}