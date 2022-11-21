using System.Text;
using Astromedia.DTO;
using Astromedia.Models;
using Astromedia.Services;
using Astromedia.Validations;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;

namespace Astromedia.Controllers;

[Authorize]
public class ConfigurationUserController : Controller
{
    private readonly UserManager<Usuario> _userManager;
    private readonly AstroContext _astroContext;
    private readonly UsuarioService _usuarioService;
    private readonly SignInManager<Usuario> _signInManager;
    private readonly EmailService _emailService;

    public ConfigurationUserController(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager, AstroContext context, UsuarioService usuarioService, EmailService emailService)
    {
        _userManager = userManager;
        _astroContext = context;
        _usuarioService = usuarioService;
        _signInManager = signInManager;
        _emailService = emailService;
    }

    public async Task<IActionResult> UpdateProfile()
    {
        var usuario = await _userManager.GetUserAsync(User);
        var usuarioDTO = new UsuarioDTO
        {
            Nome = usuario.UserName,
            DataNascimento = usuario.DataNascimento,
            Email = usuario.Email,
            Senha = usuario.PasswordHash,
            Bio = usuario.Bio
        };

        return View(usuarioDTO);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateProfile(UsuarioDTO usuarioDTO)
    {
        var usuario = await _userManager.GetUserAsync(User);
        usuarioDTO.Atualizar = true;
        var validator = new UsuarioValidator();
        var validationResult = await validator.ValidateAsync(usuarioDTO, options => options.IncludeRuleSets("ValidacaoUpdateProfile"));

        if (validationResult.IsValid)
        {
            usuario.UserName = usuarioDTO.Nome;
            usuario.DataNascimento = usuarioDTO.DataNascimento.ToUniversalTime();
            usuario.Bio = usuarioDTO.Bio;
            if (usuarioDTO.FotoPerfil is not null)
            {
                var respostaImgur = await new ImgurService().UploadImagem(usuarioDTO.FotoPerfil);
                usuario.FotoPerfil = respostaImgur.Data.data.link;
            }

            if (usuarioDTO.FotoBackground is not null)
            {
                var respostaImgur = await new ImgurService().UploadImagem(usuarioDTO.FotoBackground);
                usuario.FotoBackground = respostaImgur.Data.data.link;
            }

            try
            {
                var resultado = await _userManager.UpdateAsync(usuario);
                foreach (var error in resultado.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                    Console.WriteLine(error.Description);
                }

                return RedirectToAction("PerfilUsuario", "Feed", new { id = usuario.Id });
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Houve um erro de conexão! Aguarde e tente novamente mais tarde.");

            }
        }

        foreach (var error in validationResult.Errors)
            ModelState.AddModelError(string.Empty, error.ErrorMessage);

        return View();
    }


    public IActionResult UpdatePassword()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> UpdatePassword(UsuarioDTO usuarioDTO)
    {
        var usuario = await _userManager.GetUserAsync(User);
        usuarioDTO.Atualizar = true;
        var validator = new UsuarioValidator();
        var validationResult = await validator.ValidateAsync(usuarioDTO, options => options.IncludeRuleSets("ValidacaoSenha"));

        if (validationResult.IsValid)
        {
            try
            {
                var resultado = await _userManager.ChangePasswordAsync(usuario, usuarioDTO.SenhaAtual, usuarioDTO.Senha);
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

        return View();
    }

    public IActionResult DeleteAccount() => View();

    [HttpPost]
    public async Task<IActionResult> DeleteAccount(UsuarioDTO usuarioDTO)
    {

        var usuario = await _userManager.GetUserAsync(User);
        var usuarioAtual = await _usuarioService.GetById(usuario.Id);

        if (await _userManager.CheckPasswordAsync(usuarioAtual, usuarioDTO.SenhaAtual))
        {
            if (usuarioAtual.Astros?.Count > 0)
            {
                usuarioAtual.Astros.ForEach(a => a.Usuarios.Remove(usuarioAtual));
                _astroContext.Astros.UpdateRange(usuarioAtual.Astros);
            }

            if (usuarioAtual.Postagens?.Count > 0)
            {
                _astroContext.Postagens.RemoveRange(_astroContext.Postagens.Where(a => a.Usuario.Id == usuarioAtual.Id));
            }
            await _signInManager.SignOutAsync();
            await _userManager.DeleteAsync(usuarioAtual);
            return Redirect("/Home/Index");
        }
        ModelState.AddModelError(string.Empty, "Senha incorreta");
        return View();
    }

    public async Task<IActionResult> UpdateEmail()
    {
        var usuario = await _userManager.GetUserAsync(User);
        var usuarioDTO = new UsuarioDTO
        {
            Nome = usuario.UserName,
            DataNascimento = usuario.DataNascimento,
            Email = usuario.Email,
            Senha = usuario.PasswordHash,
            Bio = usuario.Bio
        };

        return View(usuarioDTO);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateEmail(UsuarioDTO usuarioDTO)
    {
        var usuario = await _userManager.GetUserAsync(User);
        var usuarioAtual = await _usuarioService.GetById(usuario.Id);
        var validator = new UsuarioValidator();
        var validationResult = await validator.ValidateAsync(usuarioDTO, options => options.IncludeRuleSets("ValidacaoEmail"));
        
        if (validationResult.IsValid)
        {
            var existingUser = await _userManager.FindByEmailAsync(_userManager.NormalizeEmail(usuarioDTO.NovoEmail));
            if(existingUser != null)
            {
                ModelState.AddModelError(string.Empty, "O e-mail fornecido já foi cadastrado.");
                return View(usuarioDTO);
            }
            if (await _userManager.CheckPasswordAsync(usuarioAtual, usuarioDTO.SenhaAtual))
            {
                var token = await _userManager.GenerateChangeEmailTokenAsync(usuarioAtual, usuarioDTO.NovoEmail);
                var decodedTokenString = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));
                var link = Url.Action(
                        nameof(ConfirmEmail),
                        "ConfigurationUser", 
                        new { email = usuarioDTO.Email, newemail = usuarioDTO.NovoEmail,  token = token },
                        Request.Scheme
                        );
                var emailResponse = _emailService.SendEmailToUpdate(usuarioDTO.NovoEmail, usuario.UserName, link);

                if(!emailResponse)
                {
                    ModelState.AddModelError(string.Empty, "Não foi possível enviar o e-mail, verifique se ele está correto ou tente novamente mais tarde.");
                    return View(usuarioDTO);
                }
                ViewBag.Sucesso = "Sucesso";
                return View(usuarioDTO);
            }
            
            ModelState.AddModelError(string.Empty, "Senha incorreta");
            return View(usuarioDTO);
        }

        foreach (var error in validationResult.Errors)
            ModelState.AddModelError(string.Empty, error.ErrorMessage);

        return View();
    }

    public async Task<IActionResult> ConfirmEmail(string email, string newemail, string token)
    {
        var errors = new List<string>();
        var user = await _userManager.FindByEmailAsync(_userManager.NormalizeEmail(email));
        if (user == null || token == null)
        {
            errors.Add("Token de confirmação de e-mail inválido.");
            return View(errors);
        }

        var result = await _userManager.ChangeEmailAsync(user, newemail, token);

        if(result.Succeeded)
            return View();

        foreach (var error in result.Errors)
            errors.Add(error.Description);

        return View(errors);
    }

    public async Task<IActionResult> ResendEmail(string email)
    {
        var usuario = await _userManager.GetUserAsync(User);
        var usuarioAtual = await _usuarioService.GetById(usuario.Id);

        var token = await _userManager.GenerateChangeEmailTokenAsync(usuarioAtual, email);
        var decodedTokenString = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));
        var link = Url.Action(
                nameof(ConfirmEmail),
                "ConfigurationUser", 
                new { email = usuario.Email, newemail = email,  token = token },
                Request.Scheme
                );
        var emailResponse = _emailService.SendEmailToUpdate(email, usuario.UserName, link);
        var usuarioDTO = new UsuarioDTO();
        usuarioDTO.NovoEmail = email;
        ViewBag.Sucesso = "Sucesso";
        if(!emailResponse)
        {
            ViewBag.Error = "Não foi possível enviar o e-mail, verifique se ele está correto ou tente novamente mais tarde.";
            return View("UpdateEmail", usuarioDTO);
        }
        
        return View("UpdateEmail", usuarioDTO);
    }

}