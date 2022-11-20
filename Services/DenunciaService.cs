using Astromedia.Models;
using Microsoft.EntityFrameworkCore;

namespace Astromedia.Services;

public class DenunciaService
{
    private readonly AstroContext _context;
    public DenunciaService(AstroContext context)
    {
        _context = context;
    }

    public async Task Create(Denuncia denuncia)
    {
        await _context.Denuncias.AddAsync(denuncia);
        await _context.SaveChangesAsync();
    }

    public IEnumerable<Denuncia> GetAll() =>
         _context.Denuncias
            .Include(el => el.Comentario)
            .ThenInclude(el => el.Usuario)
            .Include(el => el.Postagem)
            .ThenInclude(el => el.Usuario);

    public async Task MarkAsResolved(int id) 
    {
        var denuncia = await _context.Denuncias.FindAsync(id);
        denuncia.Respondida = true;
        _context.Denuncias.Update(denuncia);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var denuncia = await _context.Denuncias.FindAsync(id);
        _context.Denuncias.Remove(denuncia);
        await _context.SaveChangesAsync();
    }
}
