﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProyectoFinal.Models;
using ProyectoFinal.Recursos;

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

    public void InicializarDatos()
    {
        // Verifica si ya hay administradores en la base de datos
        if (!Administrador.Any())
        {
            
            var administrador = new Administrador
            {
                Nombre = "Admin", 
                Apellido = "Admin",
                Correo = "admin@admin.com",
                Contraseña = Utilidades.EncriptarContraseña("123") 
            };

            Administrador.Add(administrador);
            SaveChanges(); 


        }
    }

public DbSet<ProyectoFinal.Models.Carrito> Carrito { get; set; } = default!;

public DbSet<ProyectoFinal.Models.Pedido> Pedido { get; set; } = default!;

public DbSet<ProyectoFinal.Models.Pago> Pago { get; set; } = default!;
}