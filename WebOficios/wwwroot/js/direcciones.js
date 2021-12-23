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
            /*{ "data": "idDireccion", "width": "20%" },*/
            { "data": "nombre", "width": "60%" },
            
            {
                "data": "idDireccion",
                "render": function (data) {
                    return `
                      <div>
                        <a href="/Direcciones/Create/${data}" class="btn btn-outline-success" style="cursor:pointer;">
                            Editar
                        </a>
                        <a onclick=Delete("/Direcciones/Delete/${data}") class="btn btn-danger text-white" style="cursor:pointer;">
                            Borrar
                        </a>
                      </div>
                    `;
                }, "width": "15%"
                
            }
        ]
    });
}

function Delete(url) {
    swal({
        title: "¿Está seguro de querer borrar ésta Dirección?",
        text: "Éste registro NO se podrá recuperar",
        icon: "warning",
        buttons: true
    }).then((borrar) => {
        if (borrar) {
            $.ajax({
                type: "DELETE",
                url: url,
                success: function (data) {
                    if (data.success) {
                        alert(data.message);
                        datatable.ajax.reload();
                    }
                    else {
                        alert(data.message);
                    }
                }
            });
        }
    });
}