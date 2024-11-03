using Microsoft.EntityFrameworkCore;
using ProyectoFinal.Models;

namespace ProyectoFinal.Servicios.Contrato
{
    public interface IClienteService
    {
        Task<Cliente> GetCliente(string correo, string contraseña);
        Task<Cliente> SaveCliente(Cliente modelo);

    }
}
