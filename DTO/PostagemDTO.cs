namespace Astromedia.DTO;

public class PostagemDTO 
{
    public string Texto { get; set; }

    public DateTime DataPostagem { get; set; }

    public UsuarioDTO UsuarioDTO { get; set; }
}