using Astromedia.Models;
using Astromedia.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Astromedia.Services;

public class UsuarioService
{
    private readonly UserManager<Usuario> _userManager;
    private readonly SignInManager<Usuario> _signInManager;
    private readonly AstroContext _astroContext;

    public UsuarioService(UserManager<Usuario> userManager, AstroContext astroContext, SignInManager<Usuario> signInManager)  
    {
        _userManager = userManager;
        _astroContext = astroContext;
        _signInManager = signInManager;
    }
    public async Task<Usuario> GetById(string id) 
        => await _astroContext.Users
            .Include(el => el.Postagens)
            .Include(el => el.Astros)
            .Include(el => el.Likes)
            .FirstOrDefaultAsync(usuario => usuario.Id == id);
}
