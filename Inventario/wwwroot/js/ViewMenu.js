

const assetListBody = document.getElementById('asset-card');

////////////////////////////////////////////////////////////


///////////////////////////////////////////////////////////

var tipoVista;

function loadView(viewName) {
    tipoVista = viewName;

    /*var idSuc = @sucursalID;*/
    const idSuc = SUCURSAL_ID_ACTUAL;


    /*mainTitle.textContent = `Vista de Inventario: ${viewName.charAt(0).toUpperCase() + viewName.slice(1)}`;*/
    actualizarInventario(idSuc, viewName);

    // Manejo de la clase activa en el menú
    document.querySelectorAll('.sidebar-menu .nav-link').forEach(link => {
        link.classList.remove('active');
    });
    event.target.classList.add('active');

}

// Asignar el evento click a los enlaces de la barra lateral
document.querySelectorAll('.sidebar-menu .nav-link').forEach(link => {
    link.addEventListener('click', function (event) {
        event.preventDefault();
        loadView(this.getAttribute('data-view'));
    });
});





async function actualizarInventario  (sucursalID, Tipo)  {


    // mainTitle.textContent = `Vista de Inventario: ${viewName.charAt(0).toUpperCase() + viewName.slice(1)}`;
    assetListBody.innerHTML = '<tr><td colspan="8">Cargando datos...</td></tr>';

    if (Tipo == "Empleado") {
        var url = `/Empleado/GetFilas?sucursal=${sucursalID}`;
    } else {
        var url = `/Home/GetFilasGeneral?sucursal=${sucursalID}&tipo=${Tipo}`;
    }


    assetListBody.innerHTML = await getFetch(url);

};













//////////////////////////Modal en Funcion (Bueno)/////////////////

const boton = document.getElementById('btnAccion');
const modal1 = document.getElementById('miModal');
const contenedor = document.getElementById('contenedorTexto');



boton.addEventListener('click', async function () {

    let HTMLContenModal = '';
    contenedor.innerHTML = await getFetch(`/${tipoVista}/GetNewItemView`);

    // Mostramos el modal
    modal1.classList.add('mostrar');
});

// Función auxiliar para cerrar
function cerrarModal() {
    modal1.classList.remove('mostrar');
    // Opcional: Limpiar el contenido al cerrar
    contenedor.innerHTML = "";
}


async function getFetch(url1) {
    try {
        const response = await fetch(url1);

        if (!response.ok) {
            // Lanza un error para ser manejado si es necesario
            throw new Error(`Error ${response.status} de servidor.`);
        }

        // 1. Obtiene el texto y lo devuelve.
        return await response.text();

    } catch (error) {
        console.error('Fallo en la carga:', error);
        // 2. O devuelve el mensaje de error en caso de fallo de red.
        return '<tr><td colspan="8" class="text-danger">Error de conexión.</td></tr>';
    }
}


async function prueba() {
    const form = document.getElementById('formulario-computadora');
    const resultado = 'prueba de funcion'
    if (!form) {
        console.error('No se encontró el formulario con el ID "formulario-computadora".');
        return;
    }

    const formData = new FormData(form);

    const dataToSend = {};
    for (let [key, value] of formData.entries()) {
        // Usamos la misma clave que el nombre del campo del formulario
        // para mapear a la propiedad del modelo C# (ej: Nombre, No_Inventario)
        dataToSend[key] = value;
    }




    try {
        const response = await fetch(`/Computadora/GetNewItemView2`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json' // ⚠️ IMPORTANTE: Añadir este encabezado
            },
            body: JSON.stringify(dataToSend)
        });

        if (!response.ok) {
            // Lanza un error para ser manejado en el 'catch'
            // Es buena práctica incluir el estado y, si es posible, el texto del error del servidor.
            const errorText = await response.text();
            throw new Error(`Error de servidor (${response.status}): ${errorText || 'Sin mensaje de error'}`);
        }

        // 1. Obtiene el texto y lo devuelve/usa.
        const resultText = await response.text(); // ⚠️ CORRECCIÓN: 'await' es necesario aquí.
        alert(resultText);
        if (resultText.includes('Guardado exitosamente')) {
            form.reset();
        }
        
        //return resultText; // Si el objetivo es devolver el resultado de la función.

    } catch (error) {
        console.error('Fallo en la carga:', error);

        // 2. Manejo de error: puedes devolver el mensaje de error *o* mostrar una alerta, pero no ambas cosas de esta forma.

        // Si quieres mostrar una alerta *además* de devolver un mensaje HTML de error:
        alert(`Fallo en la carga: ${error.message}`);

        // Devuelve un mensaje de error HTML:
        //return '<tr><td colspan="8" class="text-danger">Error de conexión/carga.</td></tr>';

        // ⚠️ NOTA: El 'alert' que tenías después del 'return' nunca se ejecutaría.
        // He movido el 'alert' antes del 'return' o lo he dejado como una decisión.
    }



}



///**
//* Envía los datos del formulario (JSON) a la acción API del Controller
//* y notifica al usuario con el mensaje retornado por la base de datos (RespuestaSql).
//*/
//async function enviarFormulario() {
//    // 1. Obtener la referencia al formulario por su ID
//    const form = document.getElementById('formulario-computadora');
//    if (!form) {
//        console.error('Error JS: No se encontró el formulario con el ID "formulario-computadora".');
//        return;
//    }

//    // 2. Crear objeto FormData y convertirlo a JSON
//    const formData = new FormData(form);
//    const dataToSend = {};
//    formData.forEach((value, key) => {
//        // Mapea las claves HTML (name) a las propiedades del modelo C#
//        dataToSend[key] = value;
//    });

//    // 3. Definir la URL API
//    // ¡IMPORTANTE! Debe coincidir con la ruta [Route("...")] del método C#
//    const urlAPI = '/api/Computadora/InsertarArticulo';

//    try {
//        const response = await fetch(urlAPI, {
//            method: 'POST',
//            //headers: {
//            //    // Indica al servidor que el cuerpo es JSON
//            //    'Content-Type': 'application/json'
//            //},
//            // Envía el objeto JS como una cadena JSON
//            body: JSON.stringify(dataToSend)
//        });

//        const result = await response.json();

//        // 4. Manejo de la Respuesta (Códigos 200-299 = Éxito; 400/500 = Error)
//        if (response.ok) {
//            // Éxito al guardar
//            const mensajeExito = result.RespuestaSql || "Datos guardados exitosamente. (Sin mensaje específico de la DB)";

//            // Muestra la notificación de éxito (puedes reemplazar 'alert' por un Toast)
//            alert(`✅ Éxito:\n${mensajeExito}`);

//            // Limpia el formulario
//            form.reset();
//        } else {
//            // Error en el servidor (400, 500, etc.)
//            const mensajeError = result.RespuestaSql || `Error ${response.status}: Ocurrió un problema en el servidor.`;
//            alert(`❌ Fallo al guardar:\n${mensajeError}`);
//        }

//    } catch (error) {
//        // Manejar errores de red (ej. servidor caído)
//        console.error('Error de conexión o fetch:', error);
//        alert(`🚨 Error de conexión. Verifique la URL de la API: ${error.message}`);
//    }
//}