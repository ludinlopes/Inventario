document.addEventListener('DOMContentLoaded', function () {
    var menuLink = document.querySelector('.menu-link');
    var submenu = document.querySelector('.submenu');

    menuLink.addEventListener('click', function () {
        submenu.style.display = (submenu.style.display === 'block') ? 'none' : 'block';
    });

    // Opcional: cerrar el menú si se hace clic fuera de él
    document.addEventListener('click', function (event) {
        if (!menuLink.contains(event.target) && !submenu.contains(event.target)) {
            submenu.style.display = 'none';
        }
    });
});




