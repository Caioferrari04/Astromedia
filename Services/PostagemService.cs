using Astromedia.DTO;
using Astromedia.Models;

namespace Astromedia.Services;

public class PostagemService
{
    private AstroContext _astroContext;

    public PostagemService(AstroContext astroContext)
    {
        _astroContext = astroContext;
    }

    public void Create(PostagemDTO postagemDTO)
    {
        _astroContext.Postagens.Add(ToPostagem(postagemDTO));
        _astroContext.SaveChanges();
    }

    private Postagem ToPostagem(PostagemDTO postagemDTO) => new Postagem(postagemDTO.Texto, postagemDTO.DataPostagem, ToUsuario(postagemDTO.UsuarioDTO));

    private List<Postagem> ToPostagens(List<PostagemDTO> postagensDTO) 
    {
        List<Postagem> postagens = new List<Postagem>();
        foreach (var postagemDTO in postagensDTO)
        {
            postagens.Add(ToPostagem(postagemDTO));
        }
        return postagens;
    } 

    
    private Usuario ToUsuario(UsuarioDTO usuarioDTO)
    {
        Usuario usuario = new Usuario();
        usuario.UserName = usuarioDTO.Nome;
        usuario.Postagens = ToPostagens(usuarioDTO.Postagens);

        return usuario;
    } 
}