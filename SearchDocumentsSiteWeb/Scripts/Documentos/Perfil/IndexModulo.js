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
            var table = $('#tblPerfilesModulo').DataTable();
            table.clear().draw();
            table.rows.add(data).draw();
        }
    });
}
$(document).ready(function () {
    $("#ul_2").css("display", "block");
    /************************/
    // PERFILES
    $('#tblPerfilesModulo').DataTable({
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
        }, {
            "title": "Configurar", render: function () { return GcolDTConfig }
        }
        ],
        tableTools: {
            "sRowSelect": "single"
        }

    });

    Index();

    /* SELECCION REGISTRO */
    $("#tblPerfilesModulo tbody").on('click', 'tr', function (event) {
        event.preventDefault();
        $("#tblRolesOpcion tbody tr").removeClass('row_selected');
        $(this).addClass('row_selected');
    });
    /*BOTONES*/
    $("#btnConsultar").click(function (event) {
        event.preventDefault();
        Index();
    });
    /*BOTÓN CONFIGURAR*/
    $('#tblPerfilesModulo tbody').on("dblclick", 'tr', function (event) {
        event.preventDefault();
        var $this = $(this);
        var row = $this.closest("tr");
        var intIdCodigoPerfil = row.find('td:first').text();
        var strNombrePerfil = row.find('td:nth-child(2)').text();
        var url = BASE_APP_URL + 'Perfil/PerfilModulo?$intIdCodigoPerfil=' + EncriptarParametro(intIdCodigoPerfil) + '&$strNombrePerfil=' + EncriptarParametro(strNombrePerfil);
        window.location.href = url;
    });
    /*BOTÓN CONFIGURAR*/
    $("#tblPerfilesModulo tbody").on('click', 'img.btnConfigurar', function (event) {
        event.preventDefault();
        var $this = $(this);
        var row = $this.closest("tr");
        var intIdCodigoPerfil = row.find('td:first').text();
        var strNombrePerfil = row.find('td:nth-child(2)').text();
        var url = BASE_APP_URL + 'Perfil/PerfilModulo?$intIdCodigoPerfil=' + EncriptarParametro(intIdCodigoPerfil) + '&$strNombrePerfil=' + EncriptarParametro(strNombrePerfil);
        window.location.href = url;
    });

});