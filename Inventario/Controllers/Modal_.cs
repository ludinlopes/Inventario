using Microsoft.AspNetCore.Mvc;

public class Modal_ : Controller
{
    public IActionResult Modal()
    {
        var empleados = new List<Empleado>
        {
            new Empleado { ID = 1, Nombre = "Juan Pérez" },
            new Empleado { ID = 2, Nombre = "María García" },
            new Empleado { ID = 3, Nombre = "Carlos López" }
        };

        var model = new EmpleadosViewModel { Empleados = empleados };
        return View(model);
    }
}

public class Empleado
{
    public int ID { get; set; }
    public string Nombre { get; set; }
}

public class EmpleadosViewModel
{
    public List<Empleado> Empleados { get; set; }
}
