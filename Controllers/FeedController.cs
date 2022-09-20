using Microsoft.AspNetCore.Mvc;
using Astromedia.Services;
using Astromedia.Models;
using Microsoft.AspNetCore.Authorization;
using Astromedia.DTO;

[AllowAnonymous]
public class FeedController : Controller {
    private readonly AstroService _astroService;

    public FeedController(AstroService astroService)
    {
       _astroService = astroService;
    }

    public IActionResult Index() => View();

    public IActionResult PerfilAstro(int id)
    {
        Astro astro = _astroService.GetById(id);

        return View(astro);
    }
    
    public IActionResult Postagens() => View();

    [HttpPost]
    public void SavePostagem(PostagemDTO postagem)
    {
        // var postagemService = new PostagemService();
        // postagemService.Create(postagem);
    }
}

