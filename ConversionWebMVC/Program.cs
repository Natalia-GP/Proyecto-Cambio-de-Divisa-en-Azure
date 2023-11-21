using ConversionWebMVC.Models;
using ConversionWebMVC.Servicios;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Llamada a Interfaz para alternar entre servicio de acceso normal o el de pruebas (hola@hola.com, hola)
//1 - Servicio normal
builder.Services.AddScoped<IServicioAcceso, ServicioAcceso>();
//2 - Servicio pruebas
//builder.Services.AddScoped<IServicioAcceso, ServicioAccesoPruebas>();

//Servicio de BBDD
builder.Services.AddDbContext<Contexto>(configuracion => configuracion.UseSqlServer(
    builder.Configuration["ConnectionStrings:conversorCadena"]));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Acceso}/{action=Acceso}/{id?}");

app.Run();
