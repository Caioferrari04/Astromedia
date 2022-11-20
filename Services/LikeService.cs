using Astromedia.Models;

namespace Astromedia.Services;

public class LikeService 
{
    private readonly AstroContext _context;
    private readonly PostagemService _postagemService;
    private readonly CommentService _comentarioService;
    public LikeService(AstroContext context, PostagemService postagemService, CommentService comentarioService)
    {
        _context = context;
        _postagemService = postagemService;
        _comentarioService = comentarioService;
    }

    public async Task AdicionarLikePostagem(Usuario usuario, int postagemId)
    {
        Postagem postagem = await _postagemService.GetById(postagemId);

        await _context.Likes.AddAsync(new Like() {
            Postagem = postagem,
            Usuario = usuario
        });

        await _context.SaveChangesAsync();
    }

    public async Task AdicionarLikeComentario(Usuario usuario, int comentarioId) 
    {
        Comentario comentario = await _comentarioService.GetById(comentarioId);
        // comentario.Id = comentarioId;

        await _context.Likes.AddAsync(new Like() {
            Comentario = comentario,
            Usuario = usuario
        });

        await _context.SaveChangesAsync();
    }

    public async Task RemoverLikePostagem(Usuario usuario, int postagemId)
    {
        Postagem postagem = await _postagemService.GetById(postagemId);

        var like = _context.Likes.FirstOrDefault(el => el.Usuario.Id == usuario.Id && el.Postagem.Id == postagemId);
        _context.Likes.Remove(like);

        await _context.SaveChangesAsync();
    }

    public async Task RemoverLikeComentario(Usuario usuario, int comentarioId)
    {
        Comentario comentario = await _comentarioService.GetById(comentarioId);

        var like = _context.Likes.FirstOrDefault(el => el.Usuario.Id == usuario.Id && el.Comentario.Id == comentarioId);
        _context.Likes.Remove(like);
        
        await _context.SaveChangesAsync();
    }
}
