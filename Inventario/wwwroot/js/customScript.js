
        // Obtener el modal
    var modal = document.getElementById("myModal");

    // Obtener el botón que abre el modal
    var btn = document.getElementById("seleccionarEmpleadoBtn");

        // Obtener el elemento <span> que cierra el modal
        var span = document.getElementsByClassName("close")[0];

        // Cuando el usuario hace clic en el botón, abre el modal
        btn.onclick = function () {
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

            // Manejar la selección de empleado
            var selectBtns = document.getElementsByClassName("selectEmpleadoBtn");
            for (var i = 0; i < selectBtns.length; i++) {
                selectBtns[i].onclick = function () {
                    var empleadoNombre = this.getAttribute("data-nombre");
                    var empleadoCodigo = this.getAttribute("data-cod-emple");

                    document.getElementById("empleado").value = empleadoNombre;
                    document.getElementById("CodEmple").value = empleadoCodigo;

                    modal.style.display = "none";
                }
            }
        