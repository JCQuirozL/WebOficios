var datatable;

$(document).ready(function () {
    loadDatatable();
    var id = document.getElementById("idTipo");
    if (id.value > 0) {
        $('#myModal').modal('show');
    }
});

function limpiar() {
    var idtipo = document.getElementById("idTipo");
    var idnombre = document.getElementById("idNombre");

    idtipo.value = 0;
    idnombre.value = "";
}

function loadDatatable() {
    datatable = $('#tblData').DataTable({

        "ajax": { "url": "/TipoOficios/Listado" },

        "columns": [
           
            { "data": "nombre", "width": "70%" },

            {
                "data": "idTipo",
                "render": function (data) {
                    return `
                      <div>
                        <a href="/TipoOficios/Create/${data}" class="btn btn-outline-success" style="cursor:pointer;">
                            Editar
                        </a>
                        <a onclick=Delete("/TipoOficios/Delete/${data}") class="btn btn-danger text-white" style="cursor:pointer;">
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
        title: "¿Está seguro de querer borrar éste registro?",
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