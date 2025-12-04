// Lógica de JavaScript para el Dashboard

/**
 * Simula la carga de una vista diferente (al hacer clic en el sidebar).
 * param {string} viewName - El nombre de la vista (ej: 'assignment', 'computers').
 */
/*const mainTitle = document.querySelector('#main-content h2');*/
/*const assetCountBadge = document.getElementById('asset-count');*/
/* const assetListBody = document.getElementById('asset-list');*/
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





const actualizarInventario = (sucursalID, Tipo) => {


    // mainTitle.textContent = `Vista de Inventario: ${viewName.charAt(0).toUpperCase() + viewName.slice(1)}`;
    assetListBody.innerHTML = '<tr><td colspan="8">Cargando datos...</td></tr>';

    // var sucursalID = '1';
    // var Tipo = "Monitor";

    // ✅ URL CORRECTA: Usa solo el nombre de la clase 'Home'

    if (Tipo == "Empleado") {
        var url = `/Empleado/GetFilas?sucursal=${sucursalID}`;
    } else {
        var url = `/Home/GetFilasGeneral?sucursal=${sucursalID}&tipo=${Tipo}`;
    }
    // var url = `/Home/GetFilasGeneral?sucursal=${sucursalID}&tipo=${Tipo}`;

    fetch(url)
        .then(response => {
            if (!response.ok) {
                // Manejo de error de servidor
                throw new Error('Error de servidor al cargar las filas.');
            }
            return response.text();
        })
        .then(htmlContent => {
            assetListBody.innerHTML = htmlContent;

            // ... (Lógica para actualizar el contador) ...
        })
        .catch(error => {
            console.error('Fallo en la carga:', error);
            assetListBody.innerHTML = '<tr><td colspan="8" class="text-danger">Error de conexión.</td></tr>';
        });

};
var HTMLContenModal = '';












//////////////////////////Modal en Funcion (Bueno)/////////////////

const boton = document.getElementById('btnAccion');
const modal1 = document.getElementById('miModal');
const contenedor = document.getElementById('contenedorTexto');

// EVENTO: Al hacer clic en el botón
boton.addEventListener('click', async function () {
    //                                 ^^^^^ Aquí agregas 'async'

    // Declaración de variables (asumiendo que 'contenedor' y 'modal1' ya existen)
    let HTMLContenModal = ''; // Inicializamos para manejar el scope

    switch (tipoVista) {
        case "Computadora":

            var direccionURL = `/Computadoras/GetNewItemView`;

            // 🛑 ¡CAMBIO CLAVE! Usamos 'await' para pausar la ejecución
            // hasta que la función getFetch devuelva el resultado.
            HTMLContenModal = await getFetch(direccionURL);

            // Esta línea ahora se ejecuta SÓLO después de que la respuesta de red haya llegado.
            contenedor.innerHTML = HTMLContenModal;

            break;

        case "Monitor":
            contenedor.innerHTML += `<p>Has seleccionado la vista de <b>Monitores</b>.</p>`;
            break;
        case "Impresora":
            contenedor.innerHTML += `<p>Has seleccionado la vista de <b>Impresoras</b>.</p>`;
            break;
        case "Empleado":
            contenedor.innerHTML += `<p>Has seleccionado la vista de <b>Empleados</b>.</p>`;
            break;
        default:
            contenedor.innerHTML += `<p>Vista desconocida.</p>`;
    }

    // Mostramos el modal
    modal1.classList.add('mostrar');
});

// Función auxiliar para cerrar
function cerrarModal() {
    modal1.classList.remove('mostrar');
    // Opcional: Limpiar el contenido al cerrar
    contenedor.innerHTML = "";
}




//async function getFetch(url1) {

//    try {
//        const response = await fetch(url1);
//        if (!response.ok) {
//            // Manejo de error de servidor
//            throw new Error('Error de servidor al cargar las filas.');
//        }
//        const htmlContent = undefined;
//        HTMLContenModal = htmlContent;
//        console.log("Prueba desde fetcjh" + htmlContent);
//    } catch (error) {
//        console.error('Fallo en la carga:', error);
//        HTMLContenModal = '<tr><td colspan="8" class="text-danger">Error de conexión.</td></tr>';
//    }
//};

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