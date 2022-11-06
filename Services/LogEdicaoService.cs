using Astromedia.DTO;
using Astromedia.Models;
using Microsoft.EntityFrameworkCore;

namespace Astromedia.Services;

public class LogEdicaoService
{
    private AstroContext _context;
    public LogEdicaoService(AstroContext context)
    {
        _context = context;
    }

    public IEnumerable<LogEdicao> ObterTodosDePostagem(int postagemId) =>
        _context.LogsEdicoes.Include(el => el.Usuario).Include(el => el.Postagem).Where(l => l.PostagemId == postagemId);

    public async Task Inserir(LogEdicaoDTO log)
    {
        LogEdicao novoLog = new(
            log.Usuario,
            log.Astro,
            log.Postagem,
            log.TextoAntigo,
            log.DataEdicao,
            log.ImagemAntiga
        );

        await _context.LogsEdicoes.AddAsync(novoLog);
        await _context.SaveChangesAsync();
    }
}
