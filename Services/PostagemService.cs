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
        var astro = await _astroContext.Astros.FindAsync(postagemDTO.AstroId);
        var usuario = await _astroContext.Users.FindAsync(postagemDTO.UsuarioId);

        Postagem postagem = new Postagem(
            postagemDTO.Texto,
            postagemDTO.DataPostagem,
            postagemDTO.LinkImagem,
            usuario,
            astro
        );

        await _astroContext.Postagens.AddAsync(postagem);
        await _astroContext.SaveChangesAsync();
    }

    public List<Postagem> GetAllByAstroId(int id)
    {
        var postagens = _astroContext.Postagens
            //.Include(a => a.Astro)
            .Include(u => u.Usuario)
            .Include(u => u.Likes)
            .Include(u => u.Comentarios)
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
            .Include(u => u.Comentarios)
            .Include(l => l.Likes)
            .OrderByDescending(p => p.DataPostagem)
            .ToList();

        postagens.ForEach(p => p.DataPostagem = p.DataPostagem.ToLocalTime());

        return postagens;
    }

    public async Task<Postagem> GetById(int id) =>
        await _astroContext.Postagens
            .Include(el => el.Astro)
            .Include(el => el.Usuario)
            .Include(el => el.Likes)
            .Include(el => el.Comentarios)
            .ThenInclude(el => el.Usuario)
            .ThenInclude(el => el.Likes)
            .FirstAsync(el => el.Id == id);

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