using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ProyectoFinal.Models;
using ProyectoFinal.Servicios.Contrato;

namespace ProyectoFinal.Servicios.Implementacion
{
    public class ClienteService : IClienteService
    {
        private readonly ProyectoFinalContext _dbContext;
        public ClienteService(ProyectoFinalContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Cliente> GetCliente(string correo, string contraseña)
        {
            Cliente cliente_encontrado = await _dbContext.Cliente.Where(u => u.Correo == correo && u.Contraseña == contraseña)
                 .FirstOrDefaultAsync();

            return cliente_encontrado;
        }

        public async Task<Cliente> SaveCliente(Cliente modelo)
        {
            _dbContext.Cliente.Add(modelo);
            await _dbContext.SaveChangesAsync();
            return modelo;
        }
    }
}
