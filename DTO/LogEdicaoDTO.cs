using Astromedia.Models;

namespace Astromedia.DTO;
public class LogEdicaoDTO
{
    public Usuario Usuario { get; set; }
    public Astro Astro { get; set; }
    public Postagem Postagem { get; set; }
    public string TextoAntigo { get; set; }
    public string ImagemAntiga { get; set; }
    public DateTime DataEdicao { get; set; }
}
