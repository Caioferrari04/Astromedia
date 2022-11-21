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
                DataNascimento = usuario.DataNascimento.ToUniversalTime(),
                Bio = "Olá mundo!"
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
                    return View("SucessRegistration", usuario.Email);
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

    public IActionResult SucessRegistration(string email) => View(email);

    public async Task<IActionResult> ResendEmailToConfirmAccount(string email)
    {
        var user = await _userManager.FindByEmailAsync(_userManager.NormalizeEmail(email));
        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        var confirmationLink = Url.Action(
            nameof(ConfirmEmail),
            "SignIn", 
            new { email = email, token = token },
            Request.Scheme
            );
        
        bool emailResponse = _emailService.SendConfirmationEmail(user.Email, user.UserName, confirmationLink);
        
        if (!emailResponse)
        {
            ViewBag.Erro = "Não foi possível reenviar o e-mail.";
        }

        return View(nameof(SucessRegistration), email);
    }

    public async Task<IActionResult> ConfirmEmail(string email, string token)
    {
        
        var errors = new List<string>();
        var user = await _userManager.FindByEmailAsync(_userManager.NormalizeEmail(email));
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
            var user = await _userManager.FindByNameAsync(usuario.Nome);
            if(user != null)
            {
                if (!await _userManager.IsEmailConfirmedAsync(user))
                {
                    ModelState.AddModelError(string.Empty, "E-mail não confirmado, primeiro confirme-o.");
                    return View(nameof(LogInView));
                }
            }
            
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
                return View(nameof(LogInView));
            }

            return RedirectToAction("MeusAstros", "Feed"); /*Redirecionar para o feed :)*/
        }
        catch
        {
            Console.WriteLine("Passou aqui 2");
            ModelState.AddModelError(string.Empty, "Algo deu errado! Verifique sua conexão de internet");
            return View(nameof(LogInView));
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
            var user = await _userManager.FindByEmailAsync(_userManager.NormalizeEmail(forgotPassword.Email));
            if(!await _userManager.IsEmailConfirmedAsync(user))
            {
                ModelState.AddModelError(string.Empty, "Confirme sua conta para ser possível redefinir sua senha.");
                return View(forgotPassword);
            }
            if(user != null)
            {   
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                var passwordResetLink = Url.Action(
                    "ResetPassword",
                    "SignIn",
                    new { email = forgotPassword.Email, token =  token }, 
                    Request.Scheme
                );

                
                bool emailResponse = _emailService.SendEmailPasswordReset(user.Email, user.UserName, passwordResetLink);

                if (!emailResponse)
                {
                    ModelState.AddModelError(string.Empty, "Não foi possível enviar o e-mail, verifique se ele está correto ou tente novamente mais tarde.");
                    return View(forgotPassword);
                }

                return View("ForgotPasswordConfirmation", forgotPassword);

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

    public IActionResult ForgotPasswordConfirmation(ForgotPassword forgotPassword) => View(forgotPassword);

    public async Task<IActionResult> ResendEmail(string email)
    {
        var user = await _userManager.FindByEmailAsync(_userManager.NormalizeEmail(email));

        ForgotPassword forgotPassword = new ForgotPassword();
        forgotPassword.Email = user.Email;

        if(user != null)
        {   
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var passwordResetLink = Url.Action(
                "ResetPassword",
                "SignIn",
                new { email = email, token =  token }, 
                Request.Scheme
            );

            bool emailResponse = _emailService.SendEmailPasswordReset(user.Email, user.UserName, passwordResetLink);

            if (!emailResponse)
            {
               ViewBag.Erro = "Não foi possível reenviar o e-mail.";
            }
        }
        return View("ForgotPasswordConfirmation", forgotPassword);
    }

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
            var user = await _userManager.FindByEmailAsync(_userManager.NormalizeEmail(resetPassword.Email));

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