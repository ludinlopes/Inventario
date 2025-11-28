// Lógica de JavaScript para el Dashboard

/**
 * Simula la carga de una vista diferente (al hacer clic en el sidebar).
 * param {string} viewName - El nombre de la vista (ej: 'assignment', 'computers').
 */
const mainTitle = document.querySelector('#main-content h2');
const assetCountBadge = document.getElementById('asset-count');
// const assetListBody = document.getElementById('asset-list');
const assetListBody = document.getElementById('main-content');




function loadView(viewName) {

    /*var idSuc = @sucursalID;*/
    const idSuc = SUCURSAL_ID_ACTUAL;


    mainTitle.textContent = `Vista de Inventario: ${viewName.charAt(0).toUpperCase() + viewName.slice(1)}`;
    actualizarInventario(idSuc, viewName);

    // Manejo de la clase activa en el menú
    document.querySelectorAll('.sidebar-menu .nav-link').forEach(link => {
        link.classList.remove('active');
    });
    event.target.classList.add('active');

}



/**
 * Placeholder para mostrar filtros avanzados.
 */
function showFilters() {
    // Se mostraría un modal o un panel de filtros complejos.
    console.log("Acción: Mostrar/Ocultar Filtros Avanzados");
}


// Asignar el evento click a los enlaces de la barra lateral
document.querySelectorAll('.sidebar-menu .nav-link').forEach(link => {
    link.addEventListener('click', function (event) {
        event.preventDefault();
        loadView(this.getAttribute('data-view'));
    });
});

// Inicializar con la vista general
window.onload = function () {
    // Simular carga de datos iniciales si no están en la tabla.
    console.log("Dashboard cargado.");
};




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





/****************************Acciones para el Modal Empleado******************************************* */
/**
 * Placeholder para la función de agregar nuevo activo.
 */
var modal = document.getElementById("myModal");
function addNewAsset() {
    // Se abriría un modal o se navegaría a una página de creación.
    console.log("Acción: Abrir formulario para Nuevo Activo");
    modal.style.display = "block";
}
// Obtener el botón que abre el modal
/*var btn = document.getElementById("seleccionarEmpleadoBtn");*/

// Obtener el elemento <span> que cierra el modal
var span = document.getElementsByClassName("close")[0];

// Cuando el usuario hace clic en el botón, abre el modal
//btn.onclick = function () {
//    modal.style.display = "block";
//}

// Cuando el usuario hace clic en <span> (x), cierra el modal
span.onclick = function () {
    modal.style.display = "none";
}

// Cuando el usuario hace clic en cualquier parte fuera del modal, cierra el modal
window.onclick = function (event) {
    if (event.target == modal) {
        modal.style.display = "none";
    }
}

