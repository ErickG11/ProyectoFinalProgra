using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Models;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace ProyectoFinal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ProyectoFinalContext _context;

        public HomeController(ILogger<HomeController> logger, ProyectoFinalContext context)
        {
            _logger = logger;
            _context = context;
        }

        // Acción para mostrar los productos en la vista de inicio
        public async Task<IActionResult> Index()
        {
            var productos = await _context.Producto.Include(p => p.categoria).ToListAsync();
            return View(productos);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult IniciarSesion()
        {
            return View();
        }

        public async Task<IActionResult> CerrarSesion()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("IniciarSesion", "inicio");
        }
    }
}
