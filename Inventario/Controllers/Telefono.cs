using Inventario.ConexionDB.Consultas;
using Inventario.Implement;
using Inventario.Models;
using Microsoft.AspNetCore.Mvc;

namespace Inventario.Controllers
{
    public class Telefono : Controller
    {
        public IActionResult Editar(string b)
        {
            
            ImTelefono ups = new ImTelefono();

            MTelefono a = ups.getTelefonoByCodEmple(b);
            var c = new ImEmpleado();
            a.Empleados = c.getEmpleados();
            return View(a);
        }


        //public IActionResult Actualizar(MTelefono b)
        //{

        //    ImTelefono compu = new ImTelefono();

        //    string a = compu.setTelefono(b);
        //    MRespuestaDB resp = new MRespuestaDB();
        //    resp.respuesta = a;
        //    return View(resp);
        //}

        public IActionResult Nuevo(MTelefono b)
        {
            var h = new ConsultasDB();
            b.No_Inventario = h.getNewNoInv("TEL", HttpContext.Session.GetString("Sucursal"));
            var c = new ImEmpleado();
            b.Empleados = c.getEmpleados();
            return View(b);
        }

        [HttpPost]
        public IActionResult Update([FromBody] MTelefono b)
        {

            ImTelefono compu = new ImTelefono();
            string a = compu.setTelefono(b);
            MRespuestaDB resp = new MRespuestaDB();
            resp.respuesta = a;
            return Ok(resp.respuesta);

        }

        


        //public IActionResult Insert(MTelefono b)
        //{

        //    ImTelefono compu = new ImTelefono();
        //    MTelefono c = new MTelefono();
        //    c = b;
        //    c.RespuestaSql = compu.insertTelefono(b);

        //    return RedirectToAction("Nuevo", "Telefono", c);

        //}



        [HttpPost]
        public IActionResult Insert([FromBody] MTelefono b)
        {

            ImTelefono Telefono = new ImTelefono();
            MTelefono c = new MTelefono();
            c = b;
            c.RespuestaSql = Telefono.insertTelefono(b);
            MInvListado inv = new MInvListado();

            var g = c.RespuestaSql;


            return Ok(g);
        }



        [HttpGet]
        public IActionResult GetNewItemView()
        {


            MTelefono b = new MTelefono();
            var h = new ConsultasDB();
            b.No_Inventario = h.getNewNoInv("TEL", HttpContext.Session.GetString("Sucursal"));
            var c = new ImEmpleado();
            b.Empleados = c.getEmpleados();
            ViewBag.accionTel = "Save()";
            return PartialView("_Nuevo", b);
        }



        [HttpGet]
        public IActionResult GetEditItemView(string noInventario)
        {

            ImTelefono telefono = new ImTelefono();
            MTelefono b = telefono.getTelefonoByCodEmple(noInventario);
            var c = new ImEmpleado();
            b.Empleados = c.getEmpleados();
            ViewBag.accionTel = "Update()";
            return PartialView("_Nuevo", b);

        }



        [HttpGet]
        public IActionResult GetNewNoInv()
        {
            var h = new ConsultasDB();
            var b = h.getNewNoInv("TEL", HttpContext.Session.GetString("Sucursal"));

            return Ok(b);
        }
    }
}
