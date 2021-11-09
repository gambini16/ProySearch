$(function () {
    reporteInfoImagenesjs.inicializarEvento();
    reporteInfoImagenesjs.inicializarObjetos();
});

let reporteInfoImagenesjs = {

    inicializarEvento: function () {

        $("#btnConsultar").click(function (event) {
            event.preventDefault();
            reporteInfoImagenesjs.consulta();
        });

    },

    inicializarObjetos: function () {
        reporteInfoImagenesjs.inicializarDatatable();
    },

    inicializarDatatable: function () {
        $('#tblInfoImagenes').dataTable({
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
                    "title": "Cliente", 'data': "TemplateName"
                },
                {
                    //Column(0)
                    "title": "Tamaño", 'data': 'Img_size'
                },
                {
                    //Column(1)
                    "title": "Cant. Imagenes", 'data': 'PageCount'
                },
                {
                    //Column(2)
                    "title": "Cant. Registros", 'data': 'CantidadRegistros'
                }
            ]
        });
    },

    consulta: function () {

        let templateId = $('#hddlTemplateId').val();

        $.ajax({
            type: 'Post',
            dataType: 'json',
            cache: false,
            url: BASE_APP_URL + "ReporteInfoImagenes/GetDatosInfoImagenes",
            data: { templateId: templateId },
            beforeSend: addLoading("ContenidoWeb"),
            success: function (data) {
                clearLoading();
                let cantidadRegistros = data.length;
                document.getElementById("cantidadRegistros").innerHTML = "Cantidad de Registros: " + cantidadRegistros;

                var table = $('#tblInfoImagenes').DataTable();
                table.clear().draw();
                table.rows.add(data).draw();
            },
            error: function (data) {
                clearLoading();
                bootbox.alert("Error al hacer consulta: " + data, null);
            }
        });
    }
}