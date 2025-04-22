using Inventario.ConexionDB.ConexionSSH;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllersWithViews();
        services.AddHttpContextAccessor(); // Registrar IHttpContextAccessor
        services.AddSession(); // Agregar soporte para sesiones
        

    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        // Agregar middleware de sesión
        app.UseSession();

        // Inicializar la variable de sesión "Sucursal" si no está establecida
        app.Use(async (context, next) =>
        {
            if (string.IsNullOrEmpty(context.Session.GetString("Sucursal")))
            {
                context.Session.SetString("Sucursal", "ValorPredeterminado");
            }
            await next();
        });

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Login}/{action=VLogin}");
                //pattern: "{controller=InvGeneral}/{action=Vista}");
            //pattern: "{controller=Empleado}/{action=Nuevo}/{id?}");
        });
    }
}
