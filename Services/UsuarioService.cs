using Astromedia.Models;
using Microsoft.AspNetCore.Identity;

namespace Astromedia.Services;

public class UsuarioService
{
    private readonly UserManager<Usuario> _userManager;
    private readonly SignInManager<Usuario> _signInManager;
    private readonly AstroContext _context;

    public UsuarioService(UserManager<Usuario> userManager, AstroContext context, SignInManager<Usuario> signInManager)  
    {
        _userManager = userManager;
        _context = context;
        _signInManager = signInManager;
    }
}
