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
    private readonly CommentService _commentService;
    private readonly UserManager<Usuario> _userManager;
    private readonly LogEdicaoService _logEdicaoService;
    private readonly UsuarioService _usuarioService;
    public FeedController(AstroService astroService, UserManager<Usuario> userManager, PostagemService postagemService, LogEdicaoService logEdicaoService, CommentService commentService, UsuarioService usuarioService)
    {
        _astroService = astroService;
        _userManager = userManager;
        _postagemService = postagemService;
        _logEdicaoService = logEdicaoService;
        _commentService = commentService;
        _usuarioService = usuarioService;
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

    public async Task<IActionResult> Comentarios(int id) 
    {
        Postagem postagem = await _postagemService.GetById(id);
        return View(postagem);
    }

    [HttpPost]
    public async Task<JsonResult> SaveComment([FromForm] CommentDTO comment)
    {
        var usuario = await _userManager.GetUserAsync(User);
        var validator = new CommentValidator();
        var validationResult = validator.Validate(comment);
        var errorMessages = new List<string>();

        if (validationResult.IsValid)
        {
            comment.UsuarioId = usuario.Id;
            comment.DataComentario = DateTime.UtcNow;
            try
            {
                await _commentService.Create(comment);
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

    public async Task<IActionResult> UpdatePostagem([FromForm] PostagemDTO postagem)
    {
        var validator = new PostagemValidator();
        var validationResult = validator.Validate(postagem);
        var errorMessages = new List<string>();

        if (validationResult.IsValid)
        {
            try
            {
                postagem.DataPostagem = DateTime.UtcNow;
                var postagemOriginal = await _postagemService.GetById(postagem.Id);
                await _logEdicaoService.Inserir(new()
                {
                    Astro = await _astroService.GetById(postagem.AstroId),
                    DataEdicao = DateTime.UtcNow,
                    Postagem = postagemOriginal,
                    TextoAntigo = postagemOriginal.Texto,
                    Usuario = await _userManager.GetUserAsync(User),
                    ImagemAntiga = postagemOriginal.Imagem
                });

                // postagemOriginal.DataPostagem = postagem.DataPostagem;
                postagemOriginal.Texto = postagem.Texto;
                postagemOriginal.Imagem = postagem.LinkImagem;
                await _postagemService.Update(postagemOriginal);
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

    [HttpPost]
    public async Task<IActionResult> DeletePostagem(int id)
    {
        try
        {
            var postagem = await _postagemService.GetById(id);
            var usuario = await _userManager.GetUserAsync(User);

            if (!usuario.Postagens.Contains(postagem) && !usuario.isAdmin) throw new Exception("N�o pode excluir a postagem dos outros! V� embora!");

            await _postagemService.Delete(postagem);
            return Json(new { success = true });
        }
        catch (Exception err)
        {
            return Json(new { success = false, errors = new[] { err.Message } });
        }
    }

    public IActionResult LogsEdicao(int Id) => View(_logEdicaoService.ObterTodosDePostagem(Id));

    public async Task<IActionResult> PerfilUsuario(string id) => View(await _usuarioService.GetById(id));

    public async Task<IActionResult> MeusAstros() => View(await _usuarioService.GetById(_userManager.GetUserId(User)));
}
