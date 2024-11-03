using Microsoft.EntityFrameworkCore;
using ProyectoFinal.Models;
using ProyectoFinal.Servicios.Contrato;
using ProyectoFinal.Servicios.Implementacion;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IAdministradorService, AdministradorService>(); // Registro de IAdministradorService
builder.Services.AddDbContext<ProyectoFinalContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("ProyectoFinalContext"));
});

builder.Services.AddScoped<IClienteService, ClienteService>();

// Configurar autenticación con cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Inicio/IniciarSesion"; // Ruta de inicio de sesión
        options.AccessDeniedPath = "/Home/AccessDenied"; // Ruta para acceso denegado si la necesitas
        options.ExpireTimeSpan = TimeSpan.FromMinutes(20); // Tiempo de expiración de la sesión
    });

// Configuración para evitar caché en las vistas
builder.Services.AddControllersWithViews(options => {
    options.Filters.Add(new ResponseCacheAttribute
    {
        NoStore = true,
        Location = ResponseCacheLocation.None,
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // Habilitar autenticación
app.UseAuthorization();

// Configuración del enrutamiento de controladores
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"); // Iniciar en Home/Index

app.Run();
