﻿using Inventario.Implement;
using Inventario.Models;
using Microsoft.AspNetCore.Mvc;

namespace Inventario.Controllers
{
    public class Scanner : Controller
    {
        public IActionResult Editar(string b)
        {
            ImScanner mon = new ImScanner();

            MScanner a = mon.getEmple(b);
            return View(a);
        }


        public IActionResult Actualizar(MScanner b)
        {

            ImScanner compu = new ImScanner();

            string a = compu.setScanner(b);
            MRespuestaDB resp = new MRespuestaDB();
            resp.respuesta = a;
            return View(resp);
        }
    }
}