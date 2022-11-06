namespace Astromedia.Models;
public class LogEdicao
{
    public LogEdicao() { }

    public LogEdicao(Usuario usuario, Astro astro, Postagem postagem, string textoAntigo, DateTime dataEdicao, string imagemAntiga)
    {
        Usuario = usuario;
        Astro = astro;
        Postagem = postagem;
        TextoAntigo = textoAntigo;
        DataEdicao = dataEdicao;
        ImagemAntiga = imagemAntiga;
    }

    public int Id { get; set; }
    public string UsuarioId { get; set; }
    public Usuario Usuario { get; set; }
    public int AstroId { get; set; }
    public Astro Astro { get; set; }
    public int PostagemId { get; set; }
    public Postagem Postagem { get; set; }
    public string TextoAntigo { get; set; }
    public string ImagemAntiga { get; set; }
    public DateTime DataEdicao { get; set; }
}
