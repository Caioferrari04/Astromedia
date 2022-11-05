using Astromedia.DTO;
using Astromedia.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Astromedia.Services;

public class AstroService
{
    private AstroContext _astroContext;

    public AstroService(AstroContext astroContext)
    {
        _astroContext = astroContext;
    }

    public async Task Create(AstroDTO astroDTO)
    {
        await _astroContext.Astros.AddAsync(ToAstro(astroDTO));
        await _astroContext.SaveChangesAsync();
    }

    public async Task<Astro> GetById(int id) => await _astroContext.Astros.Include(el => el.Usuarios).FirstOrDefaultAsync(astro => astro.Id == id);

    public async Task<List<Astro>> GetAll() => await _astroContext.Astros.ToListAsync();

    public async Task<IEnumerable<Astro>> GetAllRecommended()
    {
        Random rnd = new Random();
        List<Astro> astros = await _astroContext.Astros.ToListAsync();
        List<Astro> retorno = new List<Astro>();

        for (var i = 0; i < 3; i++)
        {
            retorno.Add(astros.ElementAt(rnd.Next(astros.Count())));
        }


        return retorno.DistinctBy(el => el.Id);
    }

    public async Task Delete(int id)
    {
        Astro astro = await GetById(id);

        _astroContext.Astros.Remove(astro);
        _astroContext.SaveChanges();
    }

    public async Task<Astro> Update(int id, AstroDTO astroDTO)
    {
        Astro astro = await GetById(id);

        astro.Nome = astroDTO.Nome;
        astro.Curiosidades = astroDTO.Curiosidades;
        astro.Foto = astroDTO.Foto;

        _astroContext.Astros.Update(astro);
        _astroContext.SaveChanges();

        return astro;
    }

    private Astro ToAstro(AstroDTO astroDTO)
    {
        Astro astro = new Astro();
        astro.Nome = astroDTO.Nome;
        astro.Curiosidades = astroDTO.Curiosidades;
        astro.Foto = astroDTO.Foto;

        return astro;
    }

    public async Task JoinForum(int id, Usuario usuario)
    {
        var astro = await GetById(id);

        astro.Usuarios = astro.Usuarios is null ? new List<Usuario>() :  astro.Usuarios; 
        astro.Usuarios.Add(usuario);

        _astroContext.Update(astro);
        await _astroContext.SaveChangesAsync();
    }

    public async Task<List<Astro>> GetAllByUser(string id) 
    {
        var usuario = await _astroContext.Users.Include(u => u.Astros).FirstAsync(el => el.Id == id);
        return usuario.Astros;
    }
}