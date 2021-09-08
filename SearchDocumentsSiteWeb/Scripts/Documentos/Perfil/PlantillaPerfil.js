
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
        url: BASE_APP_URL + "Perfil/Leer_Plantilla",
        data: ParametrosReadModulo(intTipoConsulta),
        beforeSend: addLoading("ContenidoWeb"),
        success: function (data) {
            clearLoading();
            if (intTipoConsulta == 1) {
                var table = $('#tblPlantillas').DataTable();
                table.clear().draw();
                table.rows.add(data).draw();
            } else {
                var table = $('#tblPlantillasActuales').DataTable();
                table.clear().draw();
                table.rows.add(data).draw();
            }
        }
    });
}


$(document).ready(function () {
    $("#ul_2").css("display", "block");
    var tblPlantillas = $("#tblPlantillas");
    tblPlantillas.dataTable({
        "dom": GfooterDTOpcion,
        bFilter: false,
        "ordering": false,
        "bSort": false,
        "pageLength": CANTIDAD_REGISTROS_DATATABLE,
        responsive: true,
        bAutoWidth: false,
        "columns": [{
            "title": "Código Interno", 'data': 'PLANTILLA_ID'//, 'className': 'hide_column_dataTable'
        }, {
            "title": "", render: function () { return GcolDTArrowRight }
        }, {
            "title": "Plantilla", 'data': 'PLANTILLA_NOMBRE'
        }
        ]
    });
    var tblPlantillasActuales = $("#tblPlantillasActuales");
    tblPlantillasActuales.dataTable({
        "dom": GfooterDTOpcion,
        bFilter: false,
        "ordering": false,
        "bSort": false,
        "pageLength": CANTIDAD_REGISTROS_DATATABLE,
        responsive: true,
        bAutoWidth: false,
        "columns": [{
            "title": "Código Interno", 'data': 'PLANTILLA_ID'//, 'className': 'hide_column_dataTable'
        }, {
            "title": "Plantilla", 'data': 'PLANTILLA_NOMBRE'
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
    var tblPlantillasActualesDT = $('#tblPlantillasActuales').DataTable();
    var tblPlantillasDT = $('#tblPlantillas').DataTable();
    $("#tblPlantillasActuales tbody").on('click', 'img.btnConfigurar', function () {

        tr = $(this).closest('tr');
        row = tblPlantillasActualesDT.row(tr);
        rowData = [];
        tr.find('td').each(function (i, td) {
            rowData.push($(td).html());
        });
        row.remove().draw();

        tblPlantillasDT.row.add({
            "PLANTILLA_ID": rowData[0],
            "PLANTILLA_NOMBRE": rowData[1]
        }).draw();
    });
    $("#tblPlantillas tbody").on('click', 'img.btnConfigurar', function () {
        tr = $(this).closest('tr');
        row = tblPlantillasDT.row(tr);
        rowData = [];
        tr.find('td').each(function (i, td) {
            rowData.push($(td).html());
        });
        row.remove().draw();
        tblPlantillasActualesDT.row.add({
            "PLANTILLA_ID": rowData[0],
            "PLANTILLA_NOMBRE": rowData[2]
        }).draw();
    });


    /*BOTONES*/
    $("#hbtnCancelar").click(function (event) {
        event.preventDefault();
        var url = 'IndexPlantillaPerfil'
        window.location.href = url;
    });

    $("#hbtnGuardar").click(function (event) {
        event.preventDefault();
        var intPerfilID = $("#hhdnIdPerfil").val();
        var rows = $("#tblPlantillasActuales").dataTable().fnGetNodes();
        var myObject = {};
        myObject.PLANTILLAS = [];
        for (var i = 0; i < rows.length; i++) {
            myObject.PLANTILLAS.push({});
            myObject.PLANTILLAS[i]['PERFIL_ID'] = intPerfilID;
            myObject.PLANTILLAS[i]['PLANTILLA_ID'] = $(rows[i]).find("td:eq(0)").html();
            myObject.PLANTILLAS[i]['PLANTILLA_NOMBRE'] = $(rows[i]).find("td:eq(2)").html();
        }
        if (rows.length == 0) {
            myObject.PLANTILLAS.push({});
            myObject.PLANTILLAS[i]['PERFIL_ID'] = intPerfilID;
            myObject.PLANTILLAS[i]['PLANTILLA_ID'] = 0;
            myObject.PLANTILLAS[i]['PLANTILLA_NOMBRE'] = '';
        }
        $.ajax({
            cache: false,
            url: BASE_APP_URL + "Perfil/GuardarPerfilPlantilla",
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
                    var url = 'IndexPlantillaPerfil'
                    window.location.href = url;
                } else {
                    bootbox.alert("Ocurrió un error al registrar.", null);
                }
            }
        });
    });
    /*BOTÓN VER*/
    $("#tblPlantillas tbody").on('click', 'img.btnVer', function (event) {
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




