using Microsoft.AspNetCore.Mvc;
using Astromedia.Services;
using Astromedia.Models;
using Microsoft.AspNetCore.Authorization;
using Astromedia.DTO;
using Microsoft.AspNetCore.Identity;

[AllowAnonymous]
public class FeedController : Controller {
    private readonly AstroService _astroService;
    private readonly UserManager<Usuario> _userManager;
    public FeedController(AstroService astroService, UserManager<Usuario> userManager)
    {
       _astroService = astroService;
       _userManager = userManager;
    }

    public IActionResult Index() => View();

    public IActionResult PerfilAstro(int id)
    {
        Astro astro = _astroService.GetById(id);

        return View(astro);
    }
    
    public IActionResult Postagens() => View();

    [HttpPost]
    public IActionResult SavePostagem([FromBody]PostagemDTO postagem)
    {
        if(postagem == null) {
        Console.WriteLine("Nula");

        }

        else {
            Console.WriteLine("Não nula");
            Console.WriteLine(postagem.Texto);
            Console.WriteLine(postagem.DataPostagem);
            Console.WriteLine(postagem.Imagem);
        }
        // var postagemService = new PostagemService();
        // postagemService.Create(postagem);
        return RedirectToAction("Postagens");
    }

    public IActionResult Foruns() => PartialView("_Foruns");
}
