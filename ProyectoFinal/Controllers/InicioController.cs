using Microsoft.AspNetCore.Mvc;

using ProyectoFinal.Models;
using ProyectoFinal.Recursos;
using ProyectoFinal.Servicios.Contrato;

using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace ProyectoFinal.Controllers
{
    public class InicioController : Controller
    {
        private readonly IClienteService _clienteServicio;
        private readonly IAdministradorService _administradorServicio;

        // Constructor que inyecta ambos servicios
        public InicioController(IClienteService clienteServicio, IAdministradorService administradorServicio)
        {
            _clienteServicio = clienteServicio;
            _administradorServicio = administradorServicio;
        }

        public IActionResult Registrarse()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registrarse(Cliente modelo)
        {
            modelo.Contraseña = Utilidades.EncriptarContraseña(modelo.Contraseña);

            Cliente cliente_creado = await _clienteServicio.SaveCliente(modelo);

            if (cliente_creado.IdCliente > 0)
                return RedirectToAction("IniciarSesion", "Inicio");

            ViewData["Mensaje"] = "No se pudo crear el usuario";
            return View();
        }

   
        
        public IActionResult IniciarSesion()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> IniciarSesion(string correo, string contraseña)
        {
            // Encriptar la contraseña
            string EncriptarContraseña = Utilidades.EncriptarContraseña(contraseña);

            // Intentar obtener un cliente con el correo y la contraseña encriptada
            Cliente cliente_encontrado = await _clienteServicio.GetCliente(correo, EncriptarContraseña);
            if (cliente_encontrado != null)
            {
                // Crear los claims para el cliente
                await CrearClaims(cliente_encontrado.Nombre, "Cliente");
               return RedirectToAction("Index", "Home");
            }

            // Si no se encuentra el cliente, intentar obtener un administrador
            Administrador administrador_encontrado = await _administradorServicio.GetAdministrador(correo, EncriptarContraseña);
            if (administrador_encontrado != null)
            {
                // Crear los claims para el administrador
                await CrearClaims(administrador_encontrado.Nombre, "Administrador");
                return RedirectToAction("Index", "Home");
            }

            // Si no se encuentra ni cliente ni administrador, mostrar mensaje de error
            ViewData["Mensaje"] = "No se encontraron coincidencias";
            return View();
        }

        private async Task CrearClaims(string nombre, string rol)
        {
            List<Claim> claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, nombre),
        new Claim(ClaimTypes.Role, rol) // Añadir el rol del usuario
    };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            AuthenticationProperties properties = new AuthenticationProperties
            {
                AllowRefresh = true
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                properties
            );
        }


    }
}


