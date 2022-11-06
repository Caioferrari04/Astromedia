using Astromedia.DTO;
using Astromedia.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Astromedia.Services;
using Microsoft.AspNetCore.Authentication;

namespace Astromedia.Controllers;

[Authorize]
public class ConfigurationUserController : Controller
{
    private readonly UserManager<Usuario> _userManager;
    private readonly AstroContext _astroContext;
    private readonly UsuarioService _usuarioService;
    private readonly SignInManager<Usuario> _signInManager;

    public ConfigurationUserController(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager, AstroContext context, UsuarioService usuarioService)
    {
        _userManager = userManager;
        _astroContext = context;
        _usuarioService = usuarioService;
        _signInManager = signInManager;
    }

    public async Task<IActionResult> UpdateProfile()
    {
        var usuario = await _userManager.GetUserAsync(User);
        var usuarioDTO = new UsuarioDTO();
        usuarioDTO.Nome = usuario.UserName;
        usuarioDTO.DataNascimento = usuario.DataNascimento;
        usuarioDTO.Email = usuario.Email;
        usuarioDTO.Senha = usuario.PasswordHash;
        usuarioDTO.FotoPerfil = usuario.FotoPerfil;
        return View(usuarioDTO);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateProfile(UsuarioDTO usuarioDTO)
    {
        var usuario = await _userManager.GetUserAsync(User);
        usuarioDTO.Atualizar = true;
        var validator = new UsuarioValidator();
        var validationResult = await validator.ValidateAsync(usuarioDTO);

        if (validationResult.IsValid)
        {
            usuario.UserName = usuarioDTO.Nome;
            usuario.DataNascimento = usuarioDTO.DataNascimento.ToUniversalTime();
            usuario.Email = usuarioDTO.Email;
            usuario.FotoPerfil = usuarioDTO.FotoPerfil;                                                              
            try 
            {
                var resultado = await _userManager.UpdateAsync(usuario);
                    foreach (var error in resultado.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                        Console.WriteLine(error.Description);
                    }
                
            } 
            catch(Exception error)
            {
                Console.WriteLine(error.Message);
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
        var validationResult = await validator.ValidateAsync(usuarioDTO);

        if (validationResult.IsValid)
        {                                                        
            try 
            {
                var resultado = await _userManager.ChangePasswordAsync(usuario, usuarioDTO.SenhaAtual, usuarioDTO.Senha);
                foreach (var error in resultado.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);
            } 
            catch(Exception error)
            {
                Console.WriteLine(error.Message);
            } 
        }

        foreach (var error in validationResult.Errors)
            ModelState.AddModelError(string.Empty, error.ErrorMessage);

        return View();
    }

    public IActionResult DeleteAccount() => View();

    // [HttpPost]
    // public async Task<IActionResult> DeleteAccount(UsuarioDTO usuarioDTO)
    // {
    //     var usuario = await _userManager.GetUserAsync(User);
    //     var checkPassword =  await _userManager.CheckPasswordAsync(usuario, usuarioDTO.SenhaAtual);

    //     if (checkPassword)
    //     {                                                        
    //         try 
    //         {
    //             var resultado = await _userManager.DeleteAsync(usuario);
    //             foreach (var error in resultado.Errors)
    //                 ModelState.AddModelError(string.Empty, error.Description);
    //         } 
    //         catch(Exception error)
    //         {
    //             Console.WriteLine(error.Message);
    //         } 
    //     }

    //     ModelState.AddModelError(string.Empty, "Senha incorreta");

    //     return View();
    // }

    [HttpPost]
    public async Task<IActionResult> DeleteAccount(UsuarioDTO usuarioDTO)
    {
        
        var usuario = await _userManager.GetUserAsync(User);
        var usuarioAtual = await _usuarioService.GetById(usuario.Id);

        if (await _userManager.CheckPasswordAsync(usuarioAtual, usuarioDTO.SenhaAtual))
        {
            if(usuarioAtual.Astros?.Count() > 0)
            {
                usuarioAtual.Astros.ForEach(a => a.Usuarios.Remove(usuarioAtual));
                _astroContext.Astros.UpdateRange(usuarioAtual.Astros);
            }

            if(usuarioAtual.Postagens?.Count() > 0)
            {
                _astroContext.Postagens.RemoveRange( _astroContext.Postagens.Where(a => a.Usuario.Id == usuarioAtual.Id));
            }
            await _signInManager.SignOutAsync();
            await _userManager.DeleteAsync(usuarioAtual);
            return Redirect("/Home/Index");
        }
        ModelState.AddModelError(string.Empty, "Senha incorreta");
        return View();
    }
}