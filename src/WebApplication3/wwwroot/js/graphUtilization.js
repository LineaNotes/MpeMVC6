/* graphUtilization.js */
	(function () {

		$.getJSON("/Gases/GetUtilization", function (data) {

			var chart = new Highcharts.Chart({
				chart: {
					plotBackgroundColor: null,
					plotBorderWidth: null,
					plotShadow: false,
					type: 'pie',
					renderTo: 'contain1'
				},
				title: {
					text: 'Izkoristek za izbrano obdobje'
				},
				tooltip: {
					pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
				},
				plotOptions: {
					pie: {
						allowPointSelect: true,
						cursor: 'pointer',
						dataLabels: {
							enabled: true,
							format: '<b>{point.name}</b>: {point.percentage:.1f} %',
							style: {
								color: (Highcharts.theme && Highcharts.theme.contrastTextColor) || 'black'
							}
						}
					}
				},

				credits: {
					enabled: false
				},
				series: [{
					name: 'Izkoristek',
					colorByPoint: true,
					data: data
				}]
			});

		});

	})();
