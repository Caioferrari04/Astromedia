using Astromedia.DTO;
using Astromedia.Models;
using Microsoft.EntityFrameworkCore;

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
        var astro = _astroContext.Astros.Find(postagemDTO.AstroId);
        var usuario = _astroContext.Users.Find(postagemDTO.UsuarioId);

        Postagem postagem = new Postagem(
            postagemDTO.Texto,
            postagemDTO.DataPostagem,
            postagemDTO.Imagem,
            usuario,
            astro
        );
        
        _astroContext.Postagens.Add(postagem);
        _astroContext.SaveChanges();
    }

    public List<Postagem> GetAllByAstroId(int id) {
        
        var postagens = _astroContext.Postagens
            .Include(a => a.Astro)
            .Include(u => u.Usuario)
            .Where(p => p.Astro.Id == id)
            .OrderBy(p => p.DataPostagem)
            .ToList();

        postagens.ForEach(p => p.DataPostagem = p.DataPostagem.ToLocalTime());

        return postagens;
    }

    public List<Postagem> GetAll() {
        var postagens = _astroContext.Postagens
            .Include(a => a.Astro)
            .Include(u => u.Usuario)
            .ToList();

        postagens.ForEach(p => p.DataPostagem = p.DataPostagem.ToLocalTime());

        return postagens;
    }
    
}