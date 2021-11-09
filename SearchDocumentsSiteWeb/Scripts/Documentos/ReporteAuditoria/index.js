$(function () {
    reporteAuditoriajs.inicializarEvento();
    reporteAuditoriajs.inicializarObjetos();
});

let reporteAuditoriajs = {

    inicializarEvento: function () {
        reporteAuditoriajs.inicializarControles();

        $("#chkFechas").click(function () {
            if ($(this).is(':checked')) {
                $("#fechaInicioAudit").prop("disabled", false);
                $("#fechaFinAudit").prop("disabled", false);
            } else {
                $("#fechaInicioAudit").prop("disabled", true);
                $("#fechaFinAudit").prop("disabled", true);
            }
        });

        $("#btnConsultar").click(function (event) {
            event.preventDefault();
            reporteAuditoriajs.consulta();
        });
    },

    inicializarObjetos: function () {
        reporteAuditoriajs.inicializarDatatable();
    },

    inicializarControles: function () {
        $('#fechaInicioAudit').datetimepicker({
            format: 'DD/MM/YYYY',
            defaultDate: new Date(),
        });

        $('#fechaFinAudit').datetimepicker({
            format: 'DD/MM/YYYY',
            defaultDate: new Date(),
        });

        $("#fechaInicioAudit").prop("disabled", true);
        $("#fechaFinAudit").prop("disabled", true);
    },

    inicializarDatatable: function () {
        $('#tblEventos').dataTable({
            //<"col-sm-3"l> -- Numero de registros por mostrar
            "deferRender": true,
            "deferLoading": 1000,
            //"serverSide": true,
            "paging": true,
            "dom": GfooterDT,
            "lengthMenu": [[5, 25, 50, -1], [5, 25, 50, "All"]],
            bFilter: false,
            "bSort": false,
            "pageLength": CANTIDAD_REGISTROS_DATATABLE,
            responsive: false,


            "columns": [
                {
                    //Column(0)
                    "title": "Id", 'data': 'IdAudit'
                },
                {
                    //Column(1)
                    "title": "Fecha de creación", 'data': 'CreateDateString'
                },
                {
                    //Column(2)
                    "title": "Usuario", 'data': 'FullName'
                },
                {
                    //Column(3)
                    "title": "HostName", 'data': 'HostName'
                },
                {
                    //Column(4)
                    "title": "Ip_Address", 'data': 'Ip_Address'
                },
                {
                    //Column(5)
                    "title": "Evento", 'data': 'Event'
                },
                {
                    //Column(6)
                    "title": "TocId", 'data': 'TocId'
                },
                {
                    //Column(7)
                    "title": "Name_Toc", 'data': 'Name_Toc'
                },
                {
                    //Column(8)
                    "title": "Navegador", 'data': 'UserName_Domain'
                }
            ]
        });
    },

    consulta: function () {

        let IdUsuario = $('#hddlUsuarioAudit').val();
        let FechaInicio = $('#fechaInicioAudit').val();
        let FechaFin = $('#fechaFinAudit').val();
        let Event = $('#hddlEventoAudit').val() == "0" ? "" : $("#hddlEventoAudit option:selected").html();
        let EsFechaHabilitada = $('#chkFechas').prop('checked') ? "1" : "0";

        let request = {
            IdUsuario: IdUsuario,
            FechaInicio: FechaInicio,
            FechaFin: FechaFin,
            Event: Event,
            EsFechaHabilitada: EsFechaHabilitada
        };

        $.ajax({
            type: 'Post',
            dataType: 'json',
            cache: false,
            url: BASE_APP_URL + "ReporteAuditoria/GetDatosAuditoria",
            data: { request: request },
            beforeSend: addLoading("ContenidoWeb"),
            success: function (data) {
                clearLoading();

                document.getElementById("cantidadRegistros").innerHTML = "Cantidad de Registros: " + data.cantidadRegistros;
                document.getElementById("cantidadRegistros").innerHTML = "Cantidad de Registros: " + data.cantidadRegistros;

                var table = $('#tblEventos').DataTable();
                table.clear().draw();
                table.rows.add(data.list).draw();
            },
            error: function (data) {
                clearLoading();
                bootbox.alert("Error al hacer consulta: " + data, null);
            }
        });
    }
}