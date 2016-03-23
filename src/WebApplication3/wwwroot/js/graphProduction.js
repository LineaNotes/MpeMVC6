/* graphProduction.js */
(function () {

	$.getJSON("/Gases/GetAllGasesDateOutput", function (data) {
		// Create the chart
		//alert(String(data));

		var chart = new Highcharts.StockChart({
			rangeSelector: {
				selected: 1
			},

			chart: {
				type: 'column',
				renderTo: 'contain'
			},
			title: {
				text: 'Proizvodnja'
			},

			credits: {
				enabled: false
			},
			series: [
				{
					name: 'AAPL',
					data: data,
					tooltip: {
						valueDecimals: 2
					}
				}
			]
		});
	});


})();
