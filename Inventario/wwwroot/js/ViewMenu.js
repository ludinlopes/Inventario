

const assetListBody = document.getElementById('asset-card');

////////////////////////////////////////////////////////////

// Obtener el modal
var modal = document.getElementById("myModal");

// Obtener el botón que abre el modal
/*var btn = document.getElementById("btnSeleccionarEmpleado");*/

// Obtener el elemento <span> que cierra el modal
var span = document.getElementsByClassName("btn-close")[0];

// Cuando el usuario hace clic en el botón, abre el modal
function abrirModal() {
    modal.style.display = "block";
}

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

var selectBtns = document.getElementsByClassName("selectEmpleadoBtn");
for (var i = 0; i < selectBtns.length; i++) {
    selectBtns[i].onclick = function () {
        var empleadoNombre = this.getAttribute("data-nombre");
        var empleadoCodigo = this.getAttribute("data-cod-emple");

        document.getElementById("nombreEmpleado").value = empleadoNombre;
        document.getElementById("codigoEmpleado").value = empleadoCodigo;

        modal.style.display = "none";
    }
}
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
    contenedor.innerHTML = "";
}


async function getFetch(url1) {
    try {
        const response = await fetch(url1);

        if (!response.ok) {
            throw new Error(`Error ${response.status} de servidor.`);
        }

        return await response.text();

    } catch (error) {
        console.error('Fallo en la carga:', error);
        return '<tr><td colspan="8" class="text-danger">Error de conexión.</td></tr>';
    }
}


async function Save() {
    const form = document.getElementById('formulario');
    const resultado = 'prueba de funcion'
    if (!form) {
        console.error('No se encontró el formulario con el ID "formulario-computadora".');
        return;
    }

    const formData = new FormData(form);

    const dataToSend = {};
    for (let [key, value] of formData.entries()) {
        dataToSend[key] = value;
    }




    try {
        const response = await fetch(`/${tipoVista}/Insert`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(dataToSend)
        });

        if (!response.ok) {
            const errorText = await response.text();
            throw new Error(`Error de servidor (${response.status}): ${errorText || 'Sin mensaje de error'}`);
        }

        const resultText = await response.text(); 
        alert(resultText);
        if (resultText.includes('Guardado exitosamente')) {
            form.reset();
            
        }
        if (tipoVista !== "Empleado") {
            document.getElementById("noInventario").value = await getFetch(`/${tipoVista}/GetNewNoInv`);
        }
        
        


    } catch (error) {
        console.error('Fallo en la carga:', error);

        alert(`Fallo en la carga: ${error.message}`);
        

    }



}




//////////////////////////////Editar Item///////////////////////////////////
async function EditarItem(NoInventario) {
    contenedor.innerHTML = await getFetch(`/${tipoVista}/GetEditItemView?noInventario=${NoInventario}`);
    if (tipoVista == "Tablet" || tipoVista == "Celular") {
        const inputImei = document.getElementById('Imei');
        inputImei.readOnly = true;
    } else if (tipoVista == "Empleado") {
        const inputCodigoEmple = document.getElementById('CodEmpleado');
        inputCodigoEmple.readOnly = true;
    }

    // Mostramos el modal
    modal1.classList.add('mostrar');
}




async function Update() {
    const form = document.getElementById('formulario');
    const resultado = 'prueba de funcion'
    if (!form) {
        console.error('No se encontró el formulario con el ID "formulario-computadora".');
        return;
    }

    const formData = new FormData(form);

    const dataToSend = {};
    for (let [key, value] of formData.entries()) {
        dataToSend[key] = value;
    }




    try {
        const response = await fetch(`/${tipoVista}/Update`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(dataToSend)
        });

        if (!response.ok) {
            const errorText = await response.text();
            throw new Error(`Error de servidor (${response.status}): ${errorText || 'Sin mensaje de error'}`);
        }

        const resultText = await response.text();
        alert(resultText);
  


    } catch (error) {
        console.error('Fallo en la carga:', error);

        alert(`Fallo en la carga: ${error.message}`);

    }



}





function buscarItem() {
    var terminoBusqueda = document.getElementById('txtBuscar').value.toLowerCase();
    var filas = document.querySelectorAll('#asset-list tr');
    filas.forEach(fila => {
        const contenidoFila = fila.textContent.toLowerCase();
        if (contenidoFila.includes(terminoBusqueda)) {
            fila.style.display = '';
        } else {
            fila.style.display = 'none';
        }
    });
}






