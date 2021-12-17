var datatable;

$(document).ready(function () {
    loadDatatable();
    var id = document.getElementById("idDireccion");
    if (id.value > 0) {
        $('#myModal').modal('show');
    }
});

function limpiar() {
    var iddireccion = document.getElementById("idDireccion");
    var idnombre = document.getElementById("idNombre");

    iddireccion.value = 0;
    idnombre.value = "";
}

function loadDatatable() {
    datatable = $('#tblData').DataTable({

        "ajax": { "url": "/Direcciones/Listado" },

        "columns": [
            { "data": "idDireccion", "width": "20%" },
            { "data": "nombre", "width": "80%" },
            
            {
                "data": "idDireccion",
                "render": function (data) {
                    return `
                      <div>
                        <a href="/Direcciones/Create/${data}" class="btn btn-success text-white" style="cursor:pointer;">
                            Editar
                        </a>
                      </div>
                    `;
                }, "width": "10%"
                
            }
        ]
    });
}