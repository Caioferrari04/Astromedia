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

    public async Task Create(PostagemDTO postagemDTO)
    {
        var astroTask = _astroContext.Astros.FindAsync(postagemDTO.AstroId);
        var usuarioTask = _astroContext.Users.FindAsync(postagemDTO.UsuarioId);

        Postagem postagem = new Postagem(
            postagemDTO.Texto,
            postagemDTO.DataPostagem,
            postagemDTO.LinkImagem,
            await usuarioTask,
            await astroTask
        );

        await _astroContext.Postagens.AddAsync(postagem);
        await _astroContext.SaveChangesAsync();
    }

    public List<Postagem> GetAllByAstroId(int id)
    {
        var postagens = _astroContext.Postagens
            //.Include(a => a.Astro)
            .Include(u => u.Usuario)
            .Where(p => p.Astro.Id == id)
            .OrderByDescending(p => p.DataPostagem)
            .ToList();

        postagens.ForEach(p => p.DataPostagem = p.DataPostagem.ToLocalTime());

        return postagens;
    }

    public List<Postagem> GetAll()
    {
        var postagens = _astroContext.Postagens
            .Include(a => a.Astro)
            .Include(u => u.Usuario)
            .OrderByDescending(p => p.DataPostagem)
            .ToList();

        postagens.ForEach(p => p.DataPostagem = p.DataPostagem.ToLocalTime());

        return postagens;
    }

    public async Task<Postagem> GetById(int id) =>
        await _astroContext.Postagens.Include(el => el.Astro).Include(el => el.Usuario).Include(el => el.Comentarios).FirstAsync(el => el.Id == id);

    public async Task Update(Postagem postagem)
    {
        _astroContext.Postagens.Update(postagem);
        await _astroContext.SaveChangesAsync();
    }

    public async Task Delete(Postagem postagem)
    {
        _astroContext.Postagens.Remove(postagem);
        await _astroContext.SaveChangesAsync();
    }
}