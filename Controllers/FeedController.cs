using Microsoft.AspNetCore.Mvc;
using Astromedia.Services;
using Astromedia.Models;

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
}