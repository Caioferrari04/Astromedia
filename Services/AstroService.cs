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

    private Astro ToAstro(AstroDTO astroDTO)
    {
        Astro astro = new Astro();
        astro.Nome = astroDTO.Nome;
        astro.Curiosidades = astroDTO.Curiosidades;

        return astro;
    }
}