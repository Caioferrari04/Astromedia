using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Astromedia.Controllers;

[AllowAnonymous]
public class JuridicoController : Controller 
{
    public IActionResult TermosUso() => View();
}
