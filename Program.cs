using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using RegistroDePrioridades.Data;
using RegistroDePrioridades.DAL;
using RegistroDePrioridades.BLL;

namespace RegistroDePrioridades
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            //leer la ConnectionString llamada "ConStr" que pusimos en appsettings.json
            var ConStr = builder.Configuration.GetConnectionString("ConStr");
            //Inyectar el contexto para que este disponible en los construtores
            //donde lo solicitemos
            builder.Services.AddDbContext<Contexto>(options => options.UseSqlite(ConStr));
            //Inyectar todas las bll para poder recibirlas en cualquier registro
            builder.Services.AddScoped<PrioridadesBLL>();

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddSingleton<WeatherForecastService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            app.Run();
        }
    }
}