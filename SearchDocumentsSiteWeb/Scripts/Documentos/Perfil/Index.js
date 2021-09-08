function ParametrosReadPerfil() {
    var strNombrePerfil = $("#htxtPerfil").val();
    return {
        strNombrePerfil: strNombrePerfil
    };
}

function Index() {
    $.ajax({
        type: 'Get',
        dataType: 'json',
        cache: false,
        url: BASE_APP_URL + "Perfil/Leer_Perfil",
        data: ParametrosReadPerfil(),
        beforeSend: addLoading("ContenidoWeb"),
        success: function (data) {
            clearLoading();
            var table = $('#tblPerfiles').DataTable();
            table.clear().draw();
            table.rows.add(data).draw();
        }
    });
}
$(document).ready(function () {
    $("#ul_1").css("display", "block");
    $('.form-control').keypress(function (e) {
        if (e.which == 13) {
            $("#btnConsultar").click();
            return false;
        }
    });
    // ROLES
    $('#tblPerfiles').DataTable({
        //<"col-sm-3"l> -- Numero de registros por mostrar
        "dom": GfooterDT,
        "lengthMenu": [[5, 25, 50, -1], [5, 25, 50, "All"]],
        bFilter: false,
        "bSort": false,
        "pageLength": CANTIDAD_REGISTROS_DATATABLE,
        responsive: true,
        "columns": [{
            "title": "ID", 'data': 'PERFIL_ID'
        }, {
            "title": "Nombre de Perfil", 'data': 'PERFIL_NOMBRE'
        }, {
            "title": "Descripción", 'data': 'DESCRIPCION'
        },{
            "title": "Acción", render: function () { return GcolDTVer }
        }
        ],
        tableTools: {
            "sRowSelect": "single"
        }
    });
    Index();
    /* AGREGAR BOTONERA AL FOOTER */
    $('#BotoneraPerfil').appendTo('#Botonera');
    /* SELECCION REGISTRO */
    $("#tblPerfiles tbody").on('click', 'tr', function (event) {
        event.preventDefault();
        $("#tblPerfiles tbody tr").removeClass('row_selected');
        $(this).addClass('row_selected');
    });

    /*BOTONES*/
    /*BOTÓN CREAR*/
    $("#btnCrear").click(function (event) {
        event.preventDefault();
        var url = 'Crear'
        window.location.href = url;
    });
    /*BOTÓN EDITAR*/
    $('#tblPerfiles tbody').on("dblclick", 'tr', function (event) {
        event.preventDefault();
        var $this = $(this);
        var row = $this.closest("tr");
        var intIdCodigoPerfil = row.find('td:first').text();
        var url = BASE_APP_URL + 'Perfil/Editar?$intIdCodigoPerfil=' + EncriptarParametro(intIdCodigoPerfil);
        window.location.href = url;
    });
    $('#tblPerfiles tbody').on("click", 'img.btnEditar', function (event) {
        event.preventDefault();
        var $this = $(this);
        var row = $this.closest("tr");
        var intIdCodigoPerfil = row.find('td:first').text();
        var url = BASE_APP_URL + 'Perfil/Editar?$intIdCodigoPerfil=' + EncriptarParametro(intIdCodigoPerfil);
        window.location.href = url;
    });
    /*BOTÓN VER*/
    $("#tblPerfiles tbody").on('click', 'img.btnVer', function (event) {
        event.preventDefault();
        var $this = $(this);
        var row = $this.closest("tr");
        var intIdCodigoPerfil = row.find('td:first').text();
        var url = BASE_APP_URL + 'Perfil/Ver?$intIdCodigoPerfil=' + EncriptarParametro(intIdCodigoPerfil);
        window.location.href = url;
    });
    /*BOTÓN CONSULTAR*/
    $("#btnConsultar").click(function (event) {
        event.preventDefault();
        Index();
    });
});