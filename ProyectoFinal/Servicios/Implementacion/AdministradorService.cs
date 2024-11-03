using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ProyectoFinal.Models;
using ProyectoFinal.Servicios.Contrato;

namespace ProyectoFinal.Servicios.Implementacion
{
    public class AdministradorService : IAdministradorService
    {
        private readonly ProyectoFinalContext _dbContext;
        public AdministradorService(ProyectoFinalContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Administrador> GetAdministrador(string correo, string contraseña)
        {
            Administrador Administrador_encontrado = await _dbContext.Administrador.Where(u => u.Correo == correo && u.Contraseña == contraseña)
                 .FirstOrDefaultAsync();

            return Administrador_encontrado;
        }

        public async Task<Administrador> SaveAdministrador(Administrador modelo)
        {
            _dbContext.Administrador.Add(modelo);
            await _dbContext.SaveChangesAsync();
            return modelo;
        }
    }
}
