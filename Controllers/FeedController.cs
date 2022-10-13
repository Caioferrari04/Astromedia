using Microsoft.AspNetCore.Mvc;
using Astromedia.Services;
using Astromedia.Models;
using Microsoft.AspNetCore.Authorization;
using Astromedia.DTO;
using Microsoft.AspNetCore.Identity;

[AllowAnonymous]
public class FeedController : Controller {
    private readonly AstroService _astroService;
    private readonly PostagemService _postagemService;
    private readonly UserManager<Usuario> _userManager;
    public FeedController(AstroService astroService, UserManager<Usuario> userManager, PostagemService postagemService)
    {
       _astroService = astroService;
       _postagemService = postagemService;
       _userManager = userManager;
    }

    public IActionResult Index() 
    {
        var postagens = _postagemService.GetAll();
        return View("Postagens", postagens);
    }

    public IActionResult PerfilAstro(int id)
    {
        Astro astro = _astroService.GetById(id);

        return View(astro);
    }
    
    public IActionResult Postagens(int id) {
        var postagens = _postagemService.GetAllByAstroId(id);
        return View(postagens);
    }

    [HttpPost]
    public async Task<JsonResult> SavePostagem([FromBody]PostagemDTO postagem)
    {
        var usuario = await _userManager.GetUserAsync(User);
        var validator = new PostagemValidator();
        var validationResult = validator.Validate(postagem);

        if(validationResult.IsValid)
        {
            postagem.UsuarioId = usuario.Id;
            postagem.DataPostagem = DateTime.Now;
            //service
            var data = new {
                    dataPostagem = postagem.DataPostagem.ToString("dd/MM/yyyy HH:mm"),
                    imagem = postagem.Imagem,
                    texto = postagem.Texto
                };
            return Json(new {success = true, data = data});
        }
        return Json(new {success = false, errors = validationResult.Errors});
    }

    public IActionResult Foruns() => PartialView("_Foruns");
}
