var GcolDTFile2 = '<center><img class="btnPdf" title="Ver" src="' + "../Content/images/pdf.png" + '"/><img class="btnEditar" title="Editar" src="' + "../Content/images/edit.png" + '"/><img class="btnEditar" title="Editar" src="' + "../Content/images/delete.png" + '"/></center>';//ADD MGAMBINI


function ParametrosReadParametros() {
    var strIdCliente = $("#hddlCliente").val();
    var strIdSede = $("#hddlSedes").val();
    var strIdArea = $("#hddlAreas").val();
    var strIdTipoDocumental = $("#hddlTipoDocumental").val();
    var strApellidos = $("#htxtApellidos").val();
    var strNombres = $("#htxtNombres").val();
    var strDescripcion = $("#htxtDescripcion").val();
    var strFechaDesde = $("#fechaDesde").val();
    var strFechaHasta = $("#fechaHasta").val();
    return {
        strIdCliente: strIdCliente,
        strIdSede: strIdSede,
        strIdArea: strIdArea,
        strIdTipoDocumental: strIdTipoDocumental,
        strApellidos: strApellidos,
        strNombres: strNombres,
        strDescripcion: strDescripcion,
        strFechaDesde: strFechaDesde,
        strFechaHasta: strFechaHasta
    };
}

function Index() {
    //alert('index');
    $.ajax({
        type: 'Get',
        dataType: 'json',
        cache: false,
        url: BASE_APP_URL + "ConsultaDocumentoFile/Leer_DetalleArchivo",
        data: ParametrosReadParametros(),
        beforeSend: addLoading("ContenidoWeb"),
        success: function (data) {
            clearLoading();
            var table = $('#tblResultados').DataTable();
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

    $('#tblResultados').DataTable({
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
        responsive: true,
        "columns": [
        {
                "title": "Acción", render: function () { return GcolDTFile2 }
            ////"title": "ID", 'data': 'IN_CODIGO_FILE'
        },
        {
            "title": "ID", 'data': 'IN_CODIGO_FILE'
        }, {
            "title": "Apellidos", 'data': 'VC_DESCRIP_2'
        }, {
            "title": "Nombres", 'data': 'VC_DESCRIP_3'
        }, {
            "title": "Tomo", 'data': 'VC_DESCRIP_4'
        }, {
            "title": "Año", 'data': 'VC_DESCRIP_5'
        }, {
            "title": "Mes", 'data': 'VC_DESCRIP_6'
        }, {
            "title": "Caja", 'data': 'VC_DESCRIP_7'
        }, {
            "title": "Doc.", 'data': 'VC_DESCRIP_8'
        }, {
            "title": "Fecha", 'data': 'DT_FECHA_INI'
        }, {
            "title": "Cantidad Pag.", 'data': 'IN_CANT_PAG'
        }
        ],
        tableTools: {
            "sRowSelect": "single"
        },

        "fnRowCallback": function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
            if (aData[10] == "0") {
                $('td', nRow).css('background-color', 'Red');
            }
        }
    });

    /* AGREGAR BOTONERA AL FOOTER */
    $('#BotoneraModulos').appendTo('#Botonera');

    $('#tblResultados tbody').on("click", 'img.btnPdf', function (event) {
        event.preventDefault();
        var $this = $(this);
        var row = $this.closest("tr");
        var intCodigoFile = row.find('td:eq(1)').text();

        AbrirPopUpPDF(intCodigoFile);
    });

    /*BOTONES*/
    /*BOTÓN CREAR*/
    $("#btnCrear").click(function (event) {
        event.preventDefault();
        var url = 'Crear'
        window.location.href = url;
    });

    /*BOTÓN CONSULTAR*/
    $("#btnConsultar").click(function (event) {
        //event.preventDefault();
        Index();
    });


    $('#hddlSedes').change(function () {
        var selectedValue = $(this).val();
        //alert(selectedValue);
        if (selectedValue != null && selectedValue != '0') {
            var intCodigoCliente = $("#hddlCliente").val();;
            //alert(intCodigoCliente);
            //var parametro = { "intCodigoFile": intCodigoFile };
            var data = { "intCodigoCliente": intCodigoCliente };
            $.ajax({
                type: 'Get',
                dataType: 'json',
                cache: false,
                url: BASE_APP_URL + "ConsultaDocumentoFile/GetAreas",
                data: data,
                beforeSend: addLoading("ContenidoWeb"),
                success: function (data) {
                    clearLoading();
                    $("#hddlAreas").empty();

                    //var optionhtml1 = '<option value="' +
                    //    0 + '">' + "--Select State--" + '</option>';
                    //$("#hddlAreas").append(optionhtml1);

                    $.each(data, function (i) {

                        var optionhtml = '<option value="' +
                            data[i].Value + '">' + data[i].Text + '</option>';
                        $("#hddlAreas").append(optionhtml);
                    });

                }
            });





        }

    });

    
});


function f_open_popup_pdf(intCodigoFile) {

    var parametro = { "intCodigoFile": intCodigoFile };
    var hdnFlagVal = $("#hidViewPdf").val();
    if (hdnFlagVal == '1') {
        alert('1');
    }
    else {
        alert('1');
    }

}


function AbrirPopUpPDF(intCodigoFile) {
    var parametro = { "intCodigoFile": intCodigoFile };

    $.ajax({
        cache: false,
        type: "POST",
        url: BASE_APP_URL + "ConsultaDocumentoFile/MostrarPDF",
        data: parametro,
        dataType: "json",
        //beforeSend: addLoading("ContenidoWeb"),
        success: function (result) {
            //clearLoading();
            if (result.strRespuesta != '') {
                //var url = 'Index'
                //window.location.href = url;
                var strRuta = result.strRespuesta;
                var strTocId = "123";
                var myWidth = 900;
                var myHeight = 700;
                var top = (screen.height - myHeight) / 4;
                var left = (screen.width - myWidth) / 2;
                //window.open(strRuta, strTocId, 'resizable=yes, center=yes');
                window.open(strRuta, strTocId, 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=no, copyhistory=no, width=' + myWidth + ', height=' + myHeight + ', top=' + top + ', left=' + left);
            } else {
                bootbox.alert("Ocurrió un error en el registro.", null);
            }
        },
        error: function () {
            bootbox.alert("Ocurrió un error en el registro.", null);
        }
    });
}
function GenerarSolicitud(intIdCajaDetalle) {

    bootbox.confirm({
        message: "¿Está seguro que desea generar una solicitud?",
        buttons: {
            confirm: {
                label: 'Si',
                className: 'btn-success'
            },
            cancel: {
                label: 'No',
                className: 'btn-danger'
            }
        },
        callback: function (result) {
            if (result == true) {
                //Llamar al registro
                var dataOutput = {
                    intIdCajaDetalle: intIdCajaDetalle
                };
                var url = BASE_APP_URL + 'ConsultaDocumento/Crear?$intIdCajaDetalle=' + EncriptarParametro(intIdCajaDetalle);
                window.location.href = url;
            }
        }
    });
}
