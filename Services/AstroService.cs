using Astromedia.DTO;
using Astromedia.Models;
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
        Random rnd = new();
        var astros = await _astroContext.Astros.Include(p => p.Postagens).Include(u => u.Usuarios).ToListAsync();
        List<Astro> retorno = new();

        for (var i = 0; i < 10; i++)
        {
            if (astros.Count == 0) break;
            
            var elemento = astros.ElementAt(rnd.Next(0, astros.Count));
            retorno.Add(elemento);
            astros.Remove(elemento);
        }
        IEnumerable<Astro> retornoVerdadeiro = retorno;

        return retornoVerdadeiro;
    }

    public IEnumerable<Astro> GetTopAstros() => _astroContext.Astros.Include(el => el.Usuarios).OrderByDescending(el => el.Usuarios.Count);

    public async Task Delete(int id)
    {
        var astro = await GetById(id);

        _astroContext.Astros.Remove(astro);
        _astroContext.SaveChanges();
    }

    public async Task Update(AstroDTO astroDTO)
    {
        var astro = await GetById(astroDTO.Id);
        astro.Nome = astroDTO.Nome;
        astro.Curiosidades = astroDTO.Curiosidades;
        astro.MarcosHistoricos = astroDTO.MarcosHistoricos;

        if(astroDTO.Foto is not null) 
        {
            var respostaImgur = await new ImgurService().UploadImagem(astroDTO.Foto);
            astro.Foto = respostaImgur.Data.data.link;
        }

        if(astroDTO.FotoBackground is not null) 
        {
            var respostaImgur = await new ImgurService().UploadImagem(astroDTO.FotoBackground);
            astro.FotoBackground = respostaImgur.Data.data.link;
        }

        if(astroDTO.Fotos is not null) 
        {
            if(astro.Fotos is null)
                astro.Fotos = new();
            foreach(var foto in astroDTO.Fotos) {
                var resposta = await new ImgurService().UploadImagem(foto);
                astro.Fotos.Add(resposta.Data.data.link);
            }
        }

        _astroContext.Astros.Update(astro);
        await _astroContext.SaveChangesAsync();
    }

    public static Astro ToAstro(AstroDTO astroDTO) => new()
    {
        Nome = astroDTO.Nome,
        Curiosidades = astroDTO.Curiosidades
    };

    public static AstroDTO ToDTO(Astro astro) => new()
    {
        Id = astro.Id,
        LinkFoto = astro.Foto,
        LinkFotoBackground = astro.FotoBackground,
        Nome = astro.Nome,
        Curiosidades = astro.Curiosidades,
        MarcosHistoricos = astro.MarcosHistoricos
    };

    public async Task JoinForum(int id, Usuario usuario)
    {
        var astro = await GetById(id);

        astro.Usuarios = astro.Usuarios is null ? new List<Usuario>() : astro.Usuarios;
        astro.Usuarios.Add(usuario);

        _astroContext.Update(astro);
        await _astroContext.SaveChangesAsync();
    }

    public async Task QuitForum(int id, Usuario usuario) 
    {
        var astro = await GetById(id);

        astro.Usuarios = astro.Usuarios is null ? new List<Usuario>() : astro.Usuarios;
        astro.Usuarios.Remove(usuario);

        _astroContext.Astros.Update(astro);
        await _astroContext.SaveChangesAsync();
    }

    public async Task<List<Astro>> GetAllByUser(string id)
    {
        var usuario = await _astroContext.Users.Include(u => u.Astros).FirstAsync(el => el.Id == id);
        return usuario.Astros;
    }
}