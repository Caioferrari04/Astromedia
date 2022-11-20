using Astromedia.DTO;
using Astromedia.Models;
using Astromedia.Services;
using Astromedia.Validations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RestSharp;

namespace Astromedia.Controllers;

[Authorize]
public class AdministrativoController : Controller
{
    private readonly UserManager<Usuario> _userManager;
    private readonly AstroContext _context;
    private readonly AstroService _astroService;

    public AdministrativoController(UserManager<Usuario> userManager, AstroContext context, AstroService astroService)
    {
        _userManager = userManager;
        _context = context;
        _astroService = astroService;
    }

    public async Task<IActionResult> Index()
    {
        var usuario = await _userManager.GetUserAsync(User);
        if (!usuario.isAdmin) return RedirectToAction("Postagens", "Feed");
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var usuario = await _userManager.GetUserAsync(User);
        if (!usuario.isAdmin) return RedirectToAction("Postagens", "Feed");
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(AstroDTO astroDTO)
    {
        var validator = new AstroValidator();

        var validationResult = await validator.ValidateAsync(astroDTO);

        if (validationResult.IsValid)
        {
            var respostaImgur = await new ImgurService().UploadImagem(astroDTO.Foto);
            var respostaImgur2 = await new ImgurService().UploadImagem(astroDTO.FotoBackground);
            var astro = new Astro(astroDTO.Nome, astroDTO.Curiosidades, respostaImgur.Data.data.link, respostaImgur2.Data.data.link);
            foreach(var foto in astroDTO.Fotos) {
                var resposta = await new ImgurService().UploadImagem(foto);
                astro.Fotos.Add(resposta.Data.data.link);
            }
            foreach(var marco in astroDTO.MarcosHistoricos) {
                astro.MarcosHistoricos.Add(marco);
            }
            await _context.Astros.AddAsync(astro);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        foreach (var error in validationResult.Errors)
            ModelState.AddModelError(string.Empty, error.ErrorMessage);

        return View(astroDTO);
    }

    public async Task<IActionResult> List() => View(await _astroService.GetAll());

    public async Task<IActionResult> Delete(int id)
    {
        await _astroService.Delete(id);
        return RedirectToAction(nameof(List));
    }

    public async Task<IActionResult> Update(int id) 
    {
        var astro = AstroService.ToDTO(await _astroService.GetById(id));
        return View(astro);
    } 

    [HttpPost]
    public  async Task<IActionResult> Update(AstroDTO astro)
    {
        await _astroService.Update(astro);
        return RedirectToAction(nameof(List));
    }
}
