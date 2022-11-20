using Astromedia.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Astromedia.Controllers;

[AllowAnonymous]
public class HomeController : Controller
{
    private readonly SignInManager<Usuario> _signInManager;

    public HomeController(SignInManager<Usuario> signInManager)
    {
        _signInManager = signInManager;
    }

    public IActionResult Index()
    {
        if (_signInManager.IsSignedIn(User)) return RedirectToAction("MeusAstros", "Feed", new { id = 0 });

        return View();
    }
}
