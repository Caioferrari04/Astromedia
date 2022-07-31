using Astromedia.DTO;
using Astromedia.Models;

namespace Astromedia.Services;

public class AstroService
{
    private AstroContext _astroContext;

    public AstroService(AstroContext astroContext)
    {
        _astroContext = astroContext;
    }

    public void Create(AstroDTO astroDTO)
    {
        _astroContext.Astros.Add(ToAstro(astroDTO));
        _astroContext.SaveChanges();
    }

    public Astro GetById(int id) => _astroContext.Astros.Find(id);

    public List<Astro> GetAll() => _astroContext.Astros.ToList();

    // public List<Astro> GetAllByUser(Usuario usuario)
    // {
    //     return _astroContext.Astros.Where(astro => astro.Usuarios.Contains(usuario)).ToList();
    // } 

    public void Delete(int id)
    {
        Astro astro = GetById(id);

        _astroContext.Astros.Remove(astro);
        _astroContext.SaveChanges();
    }

    public Astro Update(int id, AstroDTO astroDTO)
    {
        Astro astro = GetById(id);

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
}