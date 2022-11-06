using Astromedia.DTO;
using Astromedia.Models;
using Astromedia.Services;
using Astromedia.Validations;
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

    public async Task<IActionResult> PerfilAstro(int id)
    {
        Astro astro = await _astroService.GetById(id);

        return View(astro);
    }

    public async Task<IActionResult> Postagens(int id)
    {
        List<Postagem> postagens;
        if (id is not 0)
        {
            postagens = _postagemService.GetAllByAstroId(id);
            ViewBag.astro = await _astroService.GetById(id);
        }
        else
        {
            postagens = _postagemService.GetAll();
        }

        return View(postagens);
    }

    [HttpPost]
    public async Task<JsonResult> SavePostagem([FromForm] PostagemDTO postagem)
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
                await _postagemService.Create(postagem);
                return Json(new { success = true });
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

    [HttpGet]
    public async Task<IActionResult> EntrarForum(int id)
    {
        var usuario = await _userManager.GetUserAsync(User);

        await _astroService.JoinForum(id, usuario);

        return RedirectToAction(nameof(PerfilAstro), new { id = id });
    }

    public async Task<IActionResult> SairForum(int id)
    {
        var usuario = await _userManager.GetUserAsync(User);

        await _astroService.QuitForum(id, usuario);

        return RedirectToAction(nameof(PerfilAstro), new { id = id });
    }

    [HttpPost]
    public async Task<IActionResult> UploadImagem([FromForm] IFormFile Imagem)
    {
        try
        {
            var respostaImgur = await new ImgurService().UploadImagem(Imagem);
            return Json(new { sucesso = true, linkImagem = respostaImgur.Data.data.link });
        }
        catch (Exception ex)
        {
            return Json(new { sucesso = false, erro = ex.Message });
        }
    }
}
