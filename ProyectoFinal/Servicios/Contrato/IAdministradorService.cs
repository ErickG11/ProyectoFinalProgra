using Microsoft.EntityFrameworkCore;
using ProyectoFinal.Models;

namespace ProyectoFinal.Servicios.Contrato
{
    public interface IAdministradorService
    {
        Task<Administrador> GetAdministrador(string correo, string contraseña);
        Task<Administrador> SaveAdministrador(Administrador modelo);

    }
}
