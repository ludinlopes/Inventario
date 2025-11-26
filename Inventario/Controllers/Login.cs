using Inventario.Implement;
using Inventario.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Inventario.Controllers
{
    public class Login : Controller
    {
        // Este es el método VLogin modificado
        public IActionResult VLogin()
        {
            var imSucursales = new ImSucursales();
            List<MSucursales> sucursales = new List<MSucursales>();
            string errorMessage = null;

            try
            {
                // Intenta obtener las sucursales. Si hay un error de conexión, se irá al 'catch'.
                sucursales = imSucursales.getSucursalesActivas();
            }
            catch (Exception ex)
            {
                // Captura el error de la base de datos y guarda el mensaje.
                errorMessage = "Error al conectar con la base de datos: " + ex.Message;
                // En un entorno de producción, también podrías usar un logger:
                // logger.LogError(ex, "Error al obtener sucursales activas.");
            }

            // Pasamos la lista de sucursales a la vista, incluso si está vacía.
            // También pasamos el mensaje de error.
            ViewData["Sucursales"] = sucursales;
            ViewData["ErrorMessage"] = errorMessage;

            return View();
        }

        // Este es el método Acceso modificado para guardar ID y Abrev
        public IActionResult Acceso(string Usuario, string Contraseña, string Sucursal)
        {
            HttpContext.Session.SetString("Sucursal", Sucursal);
            string sucursal = HttpContext.Session.GetString("Sucursal");
            // Primero, autentica al usuario (la lógica original)
            var a = new ImLogin();
            var b = a.getUsuario(Usuario, Contraseña);

            if (b) // Si el usuario es válido
            {
                // Instancia la clase para buscar la sucursal por su abreviatura
                var imSucursales = new ImSucursales();
                var sucursalInfo = imSucursales.getSucursalesActivas().FirstOrDefault(s => s.Abrev == Sucursal);

                if (sucursalInfo != null)
                {
                    // GUARDA EL ID , la Abrev EN LA SESIÓN y EL NOMBRE
                    // 1. El ID de la sucursal
                    HttpContext.Session.SetInt32("SucursalID", sucursalInfo.ID);

                    // 2. La abreviatura de la sucursal
                    HttpContext.Session.SetString("SucursalAbrev", sucursalInfo.Abrev);

                    // 3. ¡EL NOMBRE COMPLETO DE LA SUCURSAL!
                    HttpContext.Session.SetString("SucursalNombre", sucursalInfo.Nombre);
                }

                return RedirectToAction("Menu", "Home");
            }
            else
            {
                Console.WriteLine("Hola");
                return RedirectToAction("VLogin", "Login");
            }
        }
    }
}


using Inventario.Implement;
using Microsoft.AspNetCore.Mvc;

namespace Inventario.Controllers
{
    public class Login : Controller
    {
        public IActionResult VLogin()
        {

            return View();
        }

        public IActionResult Acceso(string Usuario, string Contraseña, string Sucursal)
        {
            HttpContext.Session.SetString("Sucursal", Sucursal);
            var a = new ImLogin();
            var b = a.getUsuario(Usuario, Contraseña);
            if (b == true)
            {
                return RedirectToAction("Home_", "Home");
            }
            else
            {
                return RedirectToAction("VLogin", "Login");
            }

        }
    }
}