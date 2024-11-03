using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoFinal.Models;
using ProyectoFinal.Recursos;

namespace ProyectoFinal.Controllers
{
    public class AdministradorsController : Controller
    {
        private readonly ProyectoFinalContext _context;

        public AdministradorsController(ProyectoFinalContext context)
        {
            _context = context;
        }

        // GET: Administradors
        public async Task<IActionResult> Index()
        {
            return View(await _context.Administrador.ToListAsync());
        }

        // GET: Administradors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var administrador = await _context.Administrador
                .FirstOrDefaultAsync(m => m.IdAdministrador == id);
            if (administrador == null)
            {
                return NotFound();
            }

            return View(administrador);
        }

        // GET: Administradors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Administradors/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdAdministrador,Nombre,Apellido,Correo,Contraseña")] Administrador administrador)
        {
            if (ModelState.IsValid)
            {
                // Encriptar la contraseña antes de guardar
                administrador.Contraseña = Utilidades.EncriptarContraseña(administrador.Contraseña);

                _context.Add(administrador);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(administrador);
        }

        // GET: Administradors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var administrador = await _context.Administrador.FindAsync(id);
            if (administrador == null)
            {
                return NotFound();
            }
            return View(administrador);
        }

        // POST: Administradors/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdAdministrador,Nombre,Apellido,Correo,Contraseña")] Administrador administrador)
        {
            if (id != administrador.IdAdministrador)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Encriptar la contraseña si se está editando
                    administrador.Contraseña = Utilidades.EncriptarContraseña(administrador.Contraseña);

                    _context.Update(administrador);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdministradorExists(administrador.IdAdministrador))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(administrador);
        }

        // GET: Administradors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var administrador = await _context.Administrador
                .FirstOrDefaultAsync(m => m.IdAdministrador == id);
            if (administrador == null)
            {
                return NotFound();
            }

            return View(administrador);
        }

        // POST: Administradors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var administrador = await _context.Administrador.FindAsync(id);
            if (administrador != null)
            {
                _context.Administrador.Remove(administrador);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdministradorExists(int id)
        {
            return _context.Administrador.Any(e => e.IdAdministrador == id);
        }
    }
}
