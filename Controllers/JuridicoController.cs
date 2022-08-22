using Microsoft.AspNetCore.Mvc;
using Astromedia.Services;
using Astromedia.Models;

namespace Astromedia.Controllers;

public class JuridicoController : Controller {
    public IActionResult TermosUso() => View();
}
