using Astromedia.DTO;
using Astromedia.Models;
using Microsoft.EntityFrameworkCore;

namespace Astromedia.Services;

public class CommentService
{
    private AstroContext _astroContext;

    public CommentService(AstroContext astroContext)
    {
        _astroContext = astroContext;
    }

    public async Task Create(CommentDTO commentDTO)
    {
        var postTask = _astroContext.Postagens.FindAsync(commentDTO.PostId);
        var usuarioTask = _astroContext.Users.FindAsync(commentDTO.UsuarioId);

        Comentario comentario = new Comentario(
            commentDTO.Texto,
            commentDTO.DataComentario,
            await usuarioTask,
            await postTask
        );

        await _astroContext.Comentarios.AddAsync(comentario);
        await _astroContext.SaveChangesAsync();
    }

    public async Task Update(Comentario comentario)
    {
        _astroContext.Comentarios.Update(comentario);
        await _astroContext.SaveChangesAsync();
    }

    public async Task Delete(Comentario comentario)
    {
        _astroContext.Comentarios.Remove(comentario);
        await _astroContext.SaveChangesAsync();
    }
}