function ParametrosReadModulo() {
    var strNombreModulo = $("#htxtModulo").val();
    return {
        strNombreModulo: strNombreModulo
    };
}
function Index() {
    $.ajax({
        type: 'Get',
        dataType: 'json',
        cache: false,
        url: BASE_APP_URL + "Modulo/Leer_Modulo",
        data: ParametrosReadModulo(),
        beforeSend: addLoading("ContenidoWeb"),
        success: function (data) {
            clearLoading();
            var table = $('#tblModulos').DataTable();
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
    $('#tblModulos').DataTable({
        //<"col-sm-3"l> -- Numero de registros por mostrar
        "dom": GfooterDT,
        "lengthMenu": [[5, 25, 50, -1], [5, 25, 50, "All"]],
        bFilter: false,
        "bSort": false,
        "pageLength": CANTIDAD_REGISTROS_DATATABLE,
        responsive: true,
        "columns": [{
            "title": "ID", 'data': 'MODULO_ID'
        }, {
            "title": "Nombre del Módulo", 'data': 'MODULO_NOMBRE'
        }, {
            "title": "Estado", 'data': 'ESTADO_DESCRIPCION'
        }, {
            "title": "Sigla", 'data': 'SIGLA'
        }, {
            "title": "Acción", render: function () { return GcolDTVer }
        }
        ],
        tableTools: {
            "sRowSelect": "single"
        }
    });
    Index();
    /* AGREGAR BOTONERA AL FOOTER */
    $('#BotoneraModulos').appendTo('#Botonera');
    /* SELECCION REGISTRO */
    $("#tblModulos tbody").on('click', 'tr', function (event) {
        event.preventDefault();
        $("#tblModulos tbody tr").removeClass('row_selected');
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
    $('#tblModulos tbody').on("dblclick", 'tr', function (event) {
        event.preventDefault();
        var $this = $(this);
        var row = $this.closest("tr");
        var intIdCodigoModulo = row.find('td:first').text();
        var url = BASE_APP_URL + 'Modulo/Editar?$intIdCodigoModulo=' + EncriptarParametro(intIdCodigoModulo);
        window.location.href = url;
    });

    $('#tblModulos tbody').on("click", 'img.btnEditar', function (event) {
        event.preventDefault();
        var $this = $(this);
        var row = $this.closest("tr");
        var intIdCodigoModulo = row.find('td:first').text();
        var url = BASE_APP_URL + 'Modulo/Editar?$intIdCodigoModulo=' + EncriptarParametro(intIdCodigoModulo);
        window.location.href = url;
    });

    /*BOTÓN VER*/
    $("#tblModulos tbody").on('click', 'img.btnVer', function (event) {
        event.preventDefault();
        var $this = $(this);
        var row = $this.closest("tr");
        var intIdCodigoModulo = row.find('td:first').text();
        var url = BASE_APP_URL + 'Modulo/Ver?$intIdCodigoModulo=' + EncriptarParametro(intIdCodigoModulo);
        window.location.href = url;
    });
    /*BOTÓN CONSULTAR*/
    $("#btnConsultar").click(function (event) {
        event.preventDefault();
        Index();
    });
});