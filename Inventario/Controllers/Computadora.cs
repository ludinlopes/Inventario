using Inventario.ConexionDB.Consultas;
using Inventario.Implement;
using Inventario.Models;
using Microsoft.AspNetCore.Mvc;
using Mysqlx;
using System;

namespace Inventario.Controllers
{
    public class Computadora : Controller
    {
        
        
        public IActionResult Editar(string b)
        {

            ImComputadora emple = new ImComputadora();

            MComputadora a = emple.getComputadoraByNoInv(b);
            var c = new ImEmpleado();
            a.Empleados = c.getEmpleados();
            return View(a);
        }

        //public IActionResult Actualizar(MComputadora b)
        //{
            
        //    ImComputadora compu = new ImComputadora();

        //    string a = compu.setComputadora(b);
        //    MRespuestaDB resp = new MRespuestaDB();
        //    resp.respuesta = a;
        //    return View(resp);
        //}




        [HttpPost]
        public IActionResult Update([FromBody] MComputadora b)
        {

            ImComputadora compu = new ImComputadora();
            string a = compu.setComputadora(b);
            MRespuestaDB resp = new MRespuestaDB();
            resp.respuesta = a;
            return Ok(resp.respuesta);
        }


        public IActionResult Nuevo(MComputadora b)
        {

            var h = new ConsultasDB();
            b.No_Inventario = h.getNewNoInv("CPU", HttpContext.Session.GetString("Sucursal"));

            var c = new ImEmpleado();
            
            b.Empleados = c.getEmpleados();
            return View(b);
        }

        //[HttpPost]
        //public string Insert (MComputadora b)
        //{

        //    ImComputadora compu = new ImComputadora();
        //    MComputadora c = new MComputadora();
        //    c = b;
        //    c.RespuestaSql = compu.insert(b);
        //    MInvListado inv = new MInvListado();
            
        //    var g = c.RespuestaSql;
        //    //return RedirectToAction("MenuInv_", "Home",new {b = inv });
        //    return g;

        //}







        [HttpGet]
        public IActionResult GetNewItemView()
        {
            MComputadora b = new MComputadora();
            var h = new ConsultasDB();
            b.No_Inventario = h.getNewNoInv("CPU", HttpContext.Session.GetString("Sucursal"));

            var c = new ImEmpleado();
            ViewBag.accionCom = "Save()";
            b.Empleados = c.getEmpleados();

            return PartialView("_Nuevo", b);
        }



        [HttpGet]
        public IActionResult GetEditItemView(string noInventario)
        {

            ImComputadora Computadora = new ImComputadora();
            MComputadora b = Computadora.getComputadoraByNoInv(noInventario);
            var c = new ImEmpleado();
            b.Empleados = c.getEmpleados();
            ViewBag.accionCom = "Update()";
            return PartialView("_Nuevo", b);

        }




        [HttpPost]
        public IActionResult Insert([FromBody] MComputadora b)
        {

            ImComputadora compu = new ImComputadora();
            MComputadora c = new MComputadora();
            c = b;
            c.RespuestaSql = compu.insert(b);
            MInvListado inv = new MInvListado();

            var g = c.RespuestaSql;
            

            return Ok(g);
        }


        [HttpGet]
        public IActionResult GetNewNoInv()
        {
            var h = new ConsultasDB();
            var b = h.getNewNoInv("CPU", HttpContext.Session.GetString("Sucursal"));

            return Ok(b);
        }











        }
    }



