


//RecaudacionSemanalTipoCliente
	function TotalEntradasSalidas(fecha)
    {
	
	
				var urlserver = "http://" + $("#servername").val() + "/wsJP/ServiceReportesJockey.asmx?op=SKI_TotalEntradasSalidas";
		        var soapMessage =
				'<soap12:Envelope xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:soap12="http://www.w3.org/2003/05/soap-envelope"> \
				<soap12:Body> \
				 <SKI_TotalEntradasSalidas xmlns="http://WebServicesReportesJockey.com/reportesWS/"> \
				<fecha>' + fecha + '</fecha> \
				 </SKI_TotalEntradasSalidas> \
				</soap12:Body> \
				</soap12:Envelope>';

					$.ajax({
					url: urlserver,
					type: "POST",
					dataType: "xml",
					data: soapMessage,
					complete: resultadoTotalEntradasSalidas,
					contentType: "application/soap+xml; charset=utf-8",
					error: errores
					});

	}
	
	function resultadoTotalEntradasSalidas(xmlHttpRequest, status) {
	
	
		$("#spanEntradas").empty();
		$("#spanSalidas").empty();
		var Entradas = $("#spanEntradas"); 
		var Salidas = $("#spanSalidas"); 
		var veces = 0;
		
		$(xmlHttpRequest.responseXML).find('E_Ski').each(function()
		 {
		   var entradasalida = $(this).find('EntradaSalida').text();
		   if(veces == 0 )
				Entradas.append(entradasalida);
		   if(veces == 1)
				Salidas.append(entradasalida);
				
				veces++;
		 });
		  	
	}
	
	
	function errores(msg) {
			alert(msg.d);
		}