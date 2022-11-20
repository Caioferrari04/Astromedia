using Astromedia.DTO;
using Astromedia.Models;
using Astromedia.Services;
using Astromedia.Validations;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;

namespace Astromedia.Controllers;

[AllowAnonymous]
public class SignInController : Controller
{
    private readonly SignInManager<Usuario> _signInManager;
    private readonly UserManager<Usuario> _userManager;
    private readonly EmailService _emailService;

    public SignInController(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager, EmailService emailService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _emailService = emailService;
    }

    /*Cadastro e log in*/
    [HttpGet]
    public IActionResult Index() => View();

    [HttpPost]
    public async Task<IActionResult> Index(UsuarioDTO usuario)
    {
        var validator = new UsuarioValidator();
        var validationResult = await validator.ValidateAsync(usuario, options => options.IncludeAllRuleSets());

        if (validationResult.IsValid)
        {
            var novoUsuario = new Usuario
            {
                UserName = usuario.Nome,
                FotoPerfil = "/img/default-img.jpg",
                FotoBackground = "/img/capa-padrao.jpeg",
                Email = usuario.Email,
                DataNascimento = usuario.DataNascimento.ToUniversalTime()
            };
            try
            {
                var resultado = await _userManager.CreateAsync(novoUsuario, usuario.Senha);

                if (resultado.Succeeded)
                {
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(novoUsuario);
                    var confirmationLink = Url.Action(
                        nameof(ConfirmEmail),
                        "SignIn", 
                        new { email = novoUsuario.Email, token = token },
                        Request.Scheme
                        );
                    
                    bool emailResponse = _emailService.SendConfirmationEmail(novoUsuario.Email, novoUsuario.UserName, confirmationLink);
                    
                    if (!emailResponse)
                    {
                        ModelState.AddModelError(string.Empty, "Não foi possível enviar o e-mail, verifique se ele está correto ou tente novamente mais tarde.");
                        await _userManager.DeleteAsync(novoUsuario);
                        return View(usuario);
                    }

                    return RedirectToAction(nameof(SucessRegistration));
                }

                foreach (var error in resultado.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
            }
        }

        foreach (var error in validationResult.Errors)
            ModelState.AddModelError(string.Empty, error.ErrorMessage);

        return View(); /*Atualizar pagina*/
    }

    public IActionResult SucessRegistration() => View();

    public async Task<IActionResult> ConfirmEmail(string email, string token)
    {
        
        var errors = new List<string>();
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null || token == null)
        {
            errors.Add("Token de confirmação de e-mail inválido.");
            return View(errors);
        }

        if (await _userManager.IsEmailConfirmedAsync(user))
        {
            if(_signInManager.IsSignedIn(User)) return RedirectToAction("MeusAstros", "Feed");
            return RedirectToAction("LogInView");
        } 

        var result = await _userManager.ConfirmEmailAsync(user, token);

        if(result.Succeeded)
            return View();

        foreach (var error in result.Errors)
            errors.Add(error.Description);

        return View(errors);
    } 

    public async Task<IActionResult> LogInView() 
    {
        await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
        return View();
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
        try
        {
            var resultado = await _signInManager.PasswordSignInAsync(
                userName: usuario.Nome,
                password: usuario.Senha,
                isPersistent: usuario.LembrarMe,
                lockoutOnFailure: false
            );

            if (!resultado.Succeeded)
            {
                ModelState.AddModelError(string.Empty, @"Tentativa de login inválida, 
                verifique se digitou seus dados corretamente");
                return RedirectToAction(nameof(LogInView));
            }

            return RedirectToAction("MeusAstros", "Feed"); /*Redirecionar para o feed :)*/
        }
        catch
        {
            Console.WriteLine("Passou aqui 2");
            ModelState.AddModelError(string.Empty, "Algo deu errado! Verifique sua conexão de internet");
            return RedirectToAction(nameof(LogInView));
        }
    }

    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }

    public IActionResult EmailRecPassword() => View();

    public IActionResult RecPassword() => View();

}