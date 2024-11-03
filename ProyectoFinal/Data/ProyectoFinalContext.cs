using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProyectoFinal.Models;

    public class ProyectoFinalContext : DbContext
    {
        public ProyectoFinalContext (DbContextOptions<ProyectoFinalContext> options)
            : base(options)
        {
        }

        public DbSet<ProyectoFinal.Models.Producto> Producto { get; set; } = default!;

        public DbSet<Cliente> Cliente { get; set; } = default!;

        public DbSet<ProyectoFinal.Models.Categoria> Categoria { get; set; } = default!;

        public DbSet<Administrador> Administrador { get; set; }




}
