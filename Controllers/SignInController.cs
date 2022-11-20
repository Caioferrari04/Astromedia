using Astromedia.DTO;
using Astromedia.Models;
using Astromedia.Validations;
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
        var validationResult = await validator.ValidateAsync(usuario);

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
                    await _signInManager.SignInAsync(novoUsuario, isPersistent: false);

                    return RedirectToAction("Index", "Home");
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

            return RedirectToAction("Postagens", "Feed"); /*Redirecionar para o feed :)*/
        }
        catch
        {
            ModelState.AddModelError(string.Empty, "Algo deu errado! Verifique sua conexão de internet");
            return RedirectToAction(nameof(LogInView));
        }
    }

    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }

    public IActionResult ForgotPassword() => View();

    [HttpPost]
    public async Task<IActionResult> ForgotPassword(ForgotPassword forgotPassword)
    {
        var validator = new ForgotPasswordValidator();
        var validationResult = await validator.ValidateAsync(forgotPassword);

        if (validationResult.IsValid)
        {                                              
            var user = await _userManager.FindByEmailAsync(forgotPassword.Email);
            if(user != null)
            {   
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                var passwordResetLink = Url.Action(
                    "ResetPassword",
                    "SignIn",
                    new { email = forgotPassword.Email, token =  token }, 
                    Request.Scheme
                );

                EmailHelper emailHelper = new EmailHelper();
                bool emailResponse = emailHelper.SendEmailPasswordReset(user.Email, passwordResetLink);

                if (emailResponse)
                {
                    return RedirectToAction("ForgotPasswordConfirmation");
                }

                else {
                    return View(forgotPassword);
                }
               
                // astromedia123_
                // testealex962

                //email alexsandro.astromedia@outlook.com
                //senha astromedia123_
                //nome alexsandro astromedia
                
            }

            ModelState.AddModelError(string.Empty, "Nenhuma conta foi encontrada com o email informado");
            return View(forgotPassword);
        }

        foreach (var error in validationResult.Errors)
            ModelState.AddModelError(string.Empty, error.ErrorMessage);
        return View(forgotPassword);
    } 

    public IActionResult ForgotPasswordConfirmation() => View();

    public IActionResult ResetPassword(string token, string email)
    {
        if (_signInManager.IsSignedIn(User)) return RedirectToAction("MeusAstros", "Feed");
        if (token == null || email == null)
        {
            ModelState.AddModelError("", "Token de redefinição de senha inválido");
        }
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> ResetPassword(ResetPassword resetPassword)
    {
        var validator = new ResetPasswordValidator();
        var validationResult = await validator.ValidateAsync(resetPassword);

        if (validationResult.IsValid)
        {
            var user = await _userManager.FindByEmailAsync(resetPassword.Email);

            if (user != null)
            {
                var result = await _userManager.ResetPasswordAsync(user, resetPassword.Token, resetPassword.Senha);
                if (result.Succeeded)
                {
                    return View("ResetPasswordConfirmation");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(resetPassword);
            }

            return View("ResetPasswordConfirmation");
        }

        foreach (var error in validationResult.Errors)
            ModelState.AddModelError(string.Empty, error.ErrorMessage);
        return View(resetPassword);
    }

     public IActionResult ResetPasswordConfirmation() => View();

}