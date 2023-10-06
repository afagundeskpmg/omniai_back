using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Web.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(IConfiguration configuracao) : base(configuracao) { }
        public IActionResult Index()
        {
            return RedirectToAction("Listar", "Projeto");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(string? errorMessage)
        {
            ViewBag.Error = errorMessage;
            return View();
        }      

    }
}
