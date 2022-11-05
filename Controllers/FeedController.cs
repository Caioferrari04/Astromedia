using Astromedia.DTO;
using Astromedia.Models;
using Astromedia.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

[Authorize]
public class FeedController : Controller
{
    private readonly AstroService _astroService;
    private readonly PostagemService _postagemService;
    private readonly UserManager<Usuario> _userManager;
    public FeedController(AstroService astroService, UserManager<Usuario> userManager, PostagemService postagemService)
    {
        _astroService = astroService;
        _postagemService = postagemService;
        _userManager = userManager;
    }
    
    public IActionResult PerfilAstro(int id)
    {
        Astro astro = _astroService.GetById(id);

        return View(astro);
    }

    public IActionResult Postagens(int id)
    {
        var postagens = id == 0 ? _postagemService.GetAll() : _postagemService.GetAllByAstroId(id);
        return View(postagens);
    }

    [HttpPost]
    public async Task<JsonResult> SavePostagem([FromBody] PostagemDTO postagem)
    {
        var usuario = await _userManager.GetUserAsync(User);
        var validator = new PostagemValidator();
        var validationResult = validator.Validate(postagem);
        var errorMessages = new List<string>();

        if (validationResult.IsValid)
        {
            postagem.UsuarioId = usuario.Id;
            postagem.DataPostagem = DateTime.UtcNow;
            try
            {
                _postagemService.Create(postagem);
                var data = new
                {
                    dataPostagem = postagem.DataPostagem.ToLocalTime().ToString("dd/MM/yyyy HH:mm"),
                    imagem = postagem.Imagem,
                    texto = postagem.Texto
                };
                return Json(new { success = true, data = data });
            }
            catch (Exception ex)
            {
                errorMessages.Add(ex.Message);
                return Json(new { success = false, errors = errorMessages });
            }
        }
        validationResult.Errors.ForEach(error => errorMessages.Add(error.ErrorMessage));
        return Json(new { success = false, errors = errorMessages });
    }

    public IActionResult Foruns() => PartialView("_Foruns");

    public IActionResult Comentarios() => PartialView("Comentarios");
}
