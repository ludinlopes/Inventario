

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