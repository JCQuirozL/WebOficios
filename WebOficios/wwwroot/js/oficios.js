﻿var datatable;

$(document).ready(function () {
    loadDatatable();
    var id = document.getElementById("idOficio");
    if (id.value > 0) {
        $('#myModal').modal('show');
    }
});

function limpiar() {
    var idoficio = document.getElementById("idOficio");
    var idnumero = document.getElementById("idNumero");
    var idfecha = document.getElementById("idFecha");
    var idtipo = document.getElementById("idTipo");
    var idireccion = document.getElementById("idDireccion");
    var idasunto = document.getElementById("idAsunto");
    var idfolio = document.getElementById("idFolio");
    var idarchivo = document.getElementById("idArchivo");

    idoficio.value = 0;
    idnumero.value = "";
    idfecha.value = null;
    idtipo.value = 0;
    idireccion.value = 0;
    idasunto.value = "";
    idfolio.value = "";


}

function loadDatatable() {
    datatable = $('#tblData').DataTable({

        "language": {
            "url": "https://cdn.datatables.net/plug-ins/1.10.19/i18n/Spanish.json"
        },

        "ajax": { "url": "/Oficios/Listado" },

        "columns": [
           
            
            { "data": "numOficio", "width": "10%" },
            { "data": "fecha", "width": "1%"  },
            { "data": "tipoOficio", "width": "20%" },
            { "data": "asunto", "width": "10%" },
            { "data": "nombreDireccion", "width": "20%"  },
            { "data": "folioSolicitud", "width": "10%"   },
            { "data": "usuario", "width": "10%" },
            


            {
                "data": "idOficio",
                "render": function (data) {
                    return `
                      <div>
                        <a href="/Oficios/Create/${data}" class="btn btn-outline-success" style="cursor:pointer;">
                            Editar
                        </a>
                        <a onclick=Delete("/Oficios/Delete/${data}") class="btn btn-danger text-white" style="cursor:pointer;">
                            Borrar
                        </a>
                      </div>
                    `;
                }, "width": "30%"

            }
        ]
    });
}

function Delete(url) {
    swal({
        title: "¿Está seguro de querer borrar éste Oficio?",
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