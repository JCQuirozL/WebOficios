var datatable;

$(document).ready(function () {
    loadDatatable();
    var id = document.getElementById("idDireccion");
    if (id.value > 0) {
        $('#myModal').modal('show');
    }
});

function loadDatatable() {
    datatable = $('#tblData').DataTable({
        "ajax": {
            "url":"/Direcciones/Listado"
        },

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