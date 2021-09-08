
function ParametrosReadModulo(intTipoConsulta) {
    var intPerfilId = $("#hhdnIdPerfil").val();
    var intTipoConsulta = intTipoConsulta
    return {
        intPerfilId: intPerfilId,
        intTipoConsulta: intTipoConsulta
    };
}
function Listar(intTipoConsulta) {
    $.ajax({
        type: 'Get',
        dataType: 'json',
        cache: false,
        url: BASE_APP_URL + "Perfil/Leer_Modulo",
        data: ParametrosReadModulo(intTipoConsulta),
        beforeSend: addLoading("ContenidoWeb"),
        success: function (data) {
            clearLoading();
            if (intTipoConsulta == 1) {
                var table = $('#tblModulos').DataTable();
                table.clear().draw();
                table.rows.add(data).draw();
            } else {
                var table = $('#tblModulosActuales').DataTable();
                table.clear().draw();
                table.rows.add(data).draw();
            }
        }
    });
}


$(document).ready(function () {
    $("#ul_2").css("display", "block");
    var tblModulos = $("#tblModulos");
    tblModulos.dataTable({
        "dom": GfooterDTOpcion,
        bFilter: false,
        "ordering": false,
        "bSort": false,
        "pageLength": CANTIDAD_REGISTROS_DATATABLE,
        responsive: true,
        bAutoWidth: false,
        "columns": [{
            "title": "Código Interno", 'data': 'MODULO_ID'//, 'className': 'hide_column_dataTable'
        }, {
            "title": "", render: function () { return GcolDTArrowRight }
        }, {
            "title": "Módulo", 'data': 'MODULO_NOMBRE'
        }
        ]
    });
    var tblModulosActuales = $("#tblModulosActuales");
    tblModulosActuales.dataTable({
        "dom": GfooterDTOpcion,
        bFilter: false,
        "ordering": false,
        "bSort": false,
        "pageLength": CANTIDAD_REGISTROS_DATATABLE,
        responsive: true,
        bAutoWidth: false,
        "columns": [{
            "title": "Código Interno", 'data': 'MODULO_ID'//, 'className': 'hide_column_dataTable'
        }, {
            "title": "Módulo", 'data': 'MODULO_NOMBRE'
        }, {
            "title": "", render: function () { return GcolDTArrowLeft }
        }
        ]
    });

    Listar(1);
    Listar(2);

    var tr,
        row,
        rowData;
    var tblModulosActualesDT = $('#tblModulosActuales').DataTable();
    var tblModulosDT = $('#tblModulos').DataTable();
    $("#tblModulosActuales tbody").on('click', 'img.btnConfigurar', function () {

        tr = $(this).closest('tr');
        row = tblModulosActualesDT.row(tr);
        rowData = [];
        tr.find('td').each(function (i, td) {
            rowData.push($(td).html());
        });
        row.remove().draw();

        tblModulosDT.row.add({
            "MODULO_ID": rowData[0],
            "MODULO_NOMBRE": rowData[1]
        }).draw();
    });
    $("#tblModulos tbody").on('click', 'img.btnConfigurar', function () {
        tr = $(this).closest('tr');
        row = tblModulosDT.row(tr);
        rowData = [];
        tr.find('td').each(function (i, td) {
            rowData.push($(td).html());
        });
        row.remove().draw();
        tblModulosActualesDT.row.add({
            "MODULO_ID": rowData[0],
            "MODULO_NOMBRE": rowData[2]
        }).draw();
    });


    /*BOTONES*/
    $("#hbtnCancelar").click(function (event) {
        event.preventDefault();
        var url = 'IndexModulo'
        window.location.href = url;
    });

    $("#hbtnGuardar").click(function (event) {
        event.preventDefault();
        var intPerfilID = $("#hhdnIdPerfil").val();
        var rows = $("#tblModulosActuales").dataTable().fnGetNodes();
        var myObject = {};
        myObject.MODULOS = [];
        for (var i = 0; i < rows.length; i++) {
            myObject.MODULOS.push({});
            myObject.MODULOS[i]['PERFIL_ID'] = intPerfilID;
            myObject.MODULOS[i]['MODULO_ID'] = $(rows[i]).find("td:eq(0)").html();
            myObject.MODULOS[i]['MODULO_NOMBRE'] = $(rows[i]).find("td:eq(2)").html();
        }
        if (rows.length == 0) {
            myObject.MODULOS.push({});
            myObject.MODULOS[i]['PERFIL_ID'] = intPerfilID;
            myObject.MODULOS[i]['MODULO_ID'] = 0;
            myObject.MODULOS[i]['MODULO_NOMBRE'] = '';
        }
        $.ajax({
            cache: false,
            url: BASE_APP_URL + "Perfil/GuardarPerfilModulo",
            type: "POST",
            data: JSON.stringify(myObject),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            beforeSend: addLoading("ContenidoWeb"),
            error: function (response) {
                bootbox.alert("Error", null);
            },
            success: function (result) {
                clearLoading();
                if (result.strRespuesta == 1) {
                    var url = 'IndexModulo'
                    window.location.href = url;
                } else {
                    bootbox.alert("Ocurrió un error al registrar.", null);
                }
            }
        });
    });
    /*BOTÓN VER*/
    $("#tblModulos tbody").on('click', 'img.btnVer', function (event) {
        event.preventDefault();
        var $this = $(this);
        var row = $this.closest("tr");
        var intIdPerfil = $("#hhdnIdPerfil").val();
        var intIdOpcion = row.find('td:nth-child(1)').text();
    });
    $("#tblActuales tbody").on('click', 'img.btnVer', function (event) {
        event.preventDefault();
        var $this = $(this);
        var row = $this.closest("tr");
        var intIdPerfil = $("#hhdnIdPerfil").val();
        var intIdOpcion = row.find('td:nth-child(1)').text();
    });
});




