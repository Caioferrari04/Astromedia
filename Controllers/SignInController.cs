using Astromedia.DTO;
using Astromedia.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Astromedia.Controllers;

[AllowAnonymous]
public class SignInController : Controller
{
    private readonly SignInManager<Usuario> _signInManager;
    private readonly UserManager<Usuario> _userManager;

    public SignInController(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    /*Cadastro e log in*/
    [HttpGet]
    public IActionResult Index() => View();

    [HttpPost]
    public async Task<IActionResult> Index(UsuarioDTO usuario)
    {
        var validator = new UsuarioValidator();
        var novoUsuario = new Usuario { UserName = usuario.Nome, FotoPerfil = "~/img/default-img.jpg", Email = usuario.Email };

        var validationResult = await validator.ValidateAsync(novoUsuario);

        if (validationResult.IsValid)
        {
            var resultado = await _userManager.CreateAsync(novoUsuario, usuario.Senha);

            if (resultado.Succeeded)
            {
                await _signInManager.SignInAsync(novoUsuario, isPersistent: false);

                return RedirectToAction("Index", "Home");
            }

            return Json(new { tipo = "database", erros = resultado.Errors });
        }

        return Json(new { tipo = "model", erros = validationResult.Errors });
    }

    public async Task<IActionResult> LogIn()
    {
        await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme); /*Desloga o usuário caso esteja logado
                                                                            Em teoria, isso deve ocorrer apenas 
                                                                            quando o tempo da sessão acaba e é
                                                                            forçado a acessar a tela de login
                                                                            novamente*/

        return PartialView("LogIn");
    }

    [HttpPost]
    public async Task<IActionResult> LogIn(UsuarioDTO usuario)
    {
        if (ModelState.IsValid)
        {
            var resultado = await _signInManager.PasswordSignInAsync(
                userName: usuario.Nome,
                password: usuario.Senha,
                isPersistent: usuario.LembrarMe,
                lockoutOnFailure: false
            );

            if (!resultado.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Tentativa de login inválida");
                return View();
            }

            return RedirectToAction("Index", "Home"); /*Redirecionar para home*/
        }

        return View(); /*oopsie, alguma coisa tava errado :)*/
    }

    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
}