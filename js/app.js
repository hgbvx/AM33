function showGetResult() {
	var result = null;
	var scriptUrl = "get_config.php";
	$.ajax({
		url: scriptUrl,
		type: 'get',
		dataType: 'json',
		async: false,
		success: function (data) {
			result = data;
		}
	});
	return result;
}

var json_result = showGetResult();


const sampleTimeSec = json_result.sampling;                  ///< sample time in sec
const sampleTimeMsec = 1000 * sampleTimeSec;  ///< sample time in msec
const maxSamplesNumber = json_result.samples;               ///< maximum number of samples

var xdata; ///< x-axis labels array: time stamps
var ydata; ///< y-axis data array: random value
var ydata1;
var ydata2;
var lastTimeStamp; ///< most recent time stamp 

var chartContext;  ///< chart context i.e. object that "owns" chart
var chart;         ///< Chart.js object
var chart1;         ///< Chart.js object
var chart2;         ///< Chart.js object


var timer; ///< request timer
var timer1;

const url = 'http://192.168.0.201/get_all_sensors.php'; ///< server app with JSON API
//const url = 'http://' + window.location.hostname + '/nocache/chartdata.json'

/**
* @brief Generate random value.
* @retval random number from range <-1, 1>
*/
function getRand() {
	const maxVal = 1.0;
	const minVal = -1.0;
	return (maxVal - minVal) * Math.random() + minVal;
}

/**
* @brief Add new value to next data point.
* @param y New y-axis value 
*/
function addData(y, y1, y2) {
	if (ydata.length > maxSamplesNumber) {
		removeOldData();
		lastTimeStamp += sampleTimeSec;
		xdata.push(lastTimeStamp.toFixed(4));
	}


	ydata.push(y);
	ydata1.push(y1);
	ydata2.push(y2);

	chart.update();
	chart1.update();
	chart2.update();
}

/**
* @brief Remove oldest data point.
*/
function removeOldData() {
	xdata.splice(0, 1);
	ydata.splice(0, 1);
	ydata1.splice(0, 1);
	ydata2.splice(0, 1);
}

/**
* @brief Start request timer
*/
function startTimer() {
	timer = setInterval(ajaxJSON, sampleTimeMsec);
}

function startTimer2() {
	timer1 = setInterval(getValuesList, 10000);
}

/**
* @brief Stop request timer
*/
function stopTimer() {
	clearInterval(timer);
}

/**
* @brief Send HTTP GET request to IoT server
*/
var result;
function ajaxJSON() {
	$.ajax(url, {
		type: 'GET', dataType: 'json',
		success: function (data) {
			result = data;
			addData(result[0].value, result[1].value, result[2].value);
		}

	});
}

function getValuesList() {
	$.ajax(url, {
		type: 'GET', dataType: 'json',
		success: function (data) {
			result = data;
			updateList(result[0].value, result[1].value, result[2].value);
		}

	});
}

/**
* @brief Chart initialization
*/
function chartInit() {
	// array with consecutive integers: <0, maxSamplesNumber-1>
	xdata = [...Array(maxSamplesNumber).keys()];
	// scaling all values ​​times the sample time 
	xdata.forEach(function (p, i) { this[i] = (this[i] * sampleTimeSec).toFixed(4); }, xdata);

	// last value of 'xdata'
	lastTimeStamp = +xdata[xdata.length - 1];

	// empty array
	ydata = [];


	// get chart context from 'canvas' element
	chartContext = $("#chart")[0].getContext('2d');

	chart = new Chart(chartContext, {
		// The type of chart: linear plot
		type: 'line',

		// Dataset: 'xdata' as labels, 'ydata' as dataset.data
		data: {
			labels: xdata,
			datasets: [{
				fill: false,
				label: 'Temperature',
				backgroundColor: 'rgb(255, 0, 0)',
				borderColor: 'rgb(255, 0, 0)',
				data: ydata,
				lineTension: 0
			}]
		},

		// Configuration options
		options: {
			responsive: true,
			maintainAspectRatio: false,
			animation: false,
			scales: {
				yAxes: [{
					scaleLabel: {
						display: true,
						labelString: 'Temperature[C]'
					}
				}],
				xAxes: [{
					scaleLabel: {
						display: true,
						labelString: 'Time [s]'
					}
				}]
			}
		}
	});

	ydata = chart.data.datasets[0].data;
	//ydata1 = chart.data.datasets[1].data;
	//ydata2 = chart.data.datasets[2].data;

	xdata = chart.data.labels;

}

function chartInit1() {
	// array with consecutive integers: <0, maxSamplesNumber-1>
	xdata = [...Array(maxSamplesNumber).keys()];
	// scaling all values ​​times the sample time 
	xdata.forEach(function (p, i) { this[i] = (this[i] * sampleTimeSec).toFixed(4); }, xdata);

	// last value of 'xdata'
	lastTimeStamp = +xdata[xdata.length - 1];

	// empty array
	ydata1 = [];

	// get chart context from 'canvas' element
	chartContext = $("#chart1")[0].getContext('2d');

	chart1 = new Chart(chartContext, {
		// The type of chart: linear plot
		type: 'line',

		// Dataset: 'xdata' as labels, 'ydata' as dataset.data
		data: {
			labels: xdata,
			datasets: [{
				fill: false,
				label: 'Pressure',
				backgroundColor: 'rgb(0, 0, 255)',
				borderColor: 'rgb(0, 0, 255)',
				data: ydata1,
				lineTension: 0
			}]
		},

		// Configuration options
		options: {
			responsive: true,
			maintainAspectRatio: false,
			animation: false,
			scales: {
				yAxes: [{
					scaleLabel: {
						display: true,
						labelString: 'Pressure [hPa]'
					}
				}],
				xAxes: [{
					scaleLabel: {
						display: true,
						labelString: 'Time [s]'
					}
				}]
			}
		}
	});

	//ydata = chart.data.datasets[0].data;
	ydata1 = chart1.data.datasets[0].data;
	//ydata2 = chart.data.datasets[2].data;

	xdata = chart1.data.labels;

}

function chartInit2() {
	// array with consecutive integers: <0, maxSamplesNumber-1>
	xdata = [...Array(maxSamplesNumber).keys()];
	// scaling all values ​​times the sample time 
	xdata.forEach(function (p, i) { this[i] = (this[i] * sampleTimeSec).toFixed(4); }, xdata);

	// last value of 'xdata'
	lastTimeStamp = +xdata[xdata.length - 1];

	// empty array
	ydata2 = [];

	// get chart context from 'canvas' element
	chartContext = $("#chart2")[0].getContext('2d');

	chart2 = new Chart(chartContext, {
		// The type of chart: linear plot
		type: 'line',

		// Dataset: 'xdata' as labels, 'ydata' as dataset.data
		data: {
			labels: xdata,
			datasets: [{
				fill: false,
				label: 'Intensity',
				backgroundColor: 'rgb(0, 255, 0)',
				borderColor: 'rgb(0, 255, 0)',
				data: ydata2,
				lineTension: 0
			}]
		},

		// Configuration options
		options: {
			responsive: true,
			maintainAspectRatio: false,
			animation: false,
			scales: {
				yAxes: [{
					scaleLabel: {
						display: true,
						labelString: 'Intensity [Lux]'
					}
				}],
				xAxes: [{
					scaleLabel: {
						display: true,
						labelString: 'Time [s]'
					}
				}]
			}
		}
	});


	ydata2 = chart2.data.datasets[0].data;

	xdata = chart2.data.labels;

}

function updateList(y, y1, y2) {
	$("#tmp_text").text("Temperatura: " + y);
	$("#prs_text").text("Cisnienie: " + y1);
	$("#lux_text").text("Naswietlenie: " + y2);
	startTimer2();

}
function postDisplay() {
	var t = $("#tempC").is(":checked") ? "t" : null;
	var p = $("#pressC").is(":checked") ? "p" : null;
	var l = $("#luxC").is(":checked") ? "l" : null;
	var i = $("#ipC").is(":checked") ? "i" : null;

	

	$.get("http://192.168.0.201/post_display.php", { arg1: t, arg2: p,arg3:l,arg4:i })


}

$(document).ready(() => {
	chartInit();
	chartInit1();
	chartInit2();
	$("#start").click(startTimer);
	$("#stop").click(stopTimer);
	$("#send").click(postDisplay);
	$("#sampletime").text(sampleTimeMsec.toString());
	$("#samplenumber").text(maxSamplesNumber.toString());
	getValuesList();
	updateList();
});