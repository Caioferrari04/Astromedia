using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[AllowAnonymous]
public class HomeController : Controller {

    public IActionResult Index() => View();

    public IActionResult PginaNov() => View();
}
