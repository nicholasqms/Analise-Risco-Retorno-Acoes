function drawLineChart(symbol) {
    var DataPoints = [];
    var labels = [], data = [];
    var start = new Date();
    start.setMonth(8);
    var end = new Date();
    var jsonData = $.ajax({
        url: '/CompanyList/IndexChart/',
        data: { 'symbol': symbol, "start": start.toISOString(), "end": end.toISOString() },
        dataType: 'json',
        success: function (response) {
            for (i in response) {
                DataPoints[i] = (response[i]);
            }


            // Split timestamp and data into separate arrays
            for (i in DataPoints) {
                labels[i] = DataPoints[i].x;
                data[i] = DataPoints[i].y;
            }


            //alert(JSON.stringify(DataPoints));
            //alert(JSON.stringify(labels));
            //alert(JSON.stringify(data));
            myLineChart.update();
            $("#chart-container"+symbol).fadeIn(2000);
        },
    });
    // Create the chart.js data structure using 'labels' and 'data'
    var config = {
        type: 'line',
        data: {
            labels: labels,
            datasets: [{
                label: symbol,
                //fillColor: "rgba(0,0,0,0.1)",
                backgroundColor: 'rgb(54, 162, 235)',
                bordercolor: 'rgb(54, 162, 235)',
                data: data,
                fill: false,
            }
            ],
        },
        options: {
            elements: {
                line: {
                    tension: 0,
                }
            },
            responsive: true,
            maintainAspectRatio: false
        }
    }


    // Get the context of the canvas element we want to select
    var ctx = document.getElementById("myLineChart"+symbol).getContext("2d");

    // Instantiate a new chart
    var myLineChart = new Chart(ctx, config);
}

function drawLineChartPeriod(symbol,start) {
    var DataPoints = [];
    var labels = [], data = [];
    var end = new Date();

    var jsonData = $.ajax({
        url: '/CompanyList/IndexChart/',
        data: { 'symbol': symbol, "start": start, "end": end.toISOString() },
        dataType: 'json',
        success: function (response) {
            for (i in response) {
                DataPoints[i] = (response[i]);
            }


            // Split timestamp and data into separate arrays
            for (i in DataPoints) {
                labels[i] = DataPoints[i].x;
                data[i] = DataPoints[i].y;
            }


            //alert(JSON.stringify(DataPoints));
            //alert(JSON.stringify(labels));
            //alert(JSON.stringify(data));
            myLineChart.update();
            $("#chart-container" + symbol).fadeIn(2000);
        },
    });
    // Create the chart.js data structure using 'labels' and 'data'
    var config = {
        type: 'line',
        data: {
            labels: labels,
            datasets: [{
                label: symbol,
                //fillColor: "rgba(0,0,0,0.1)",
                backgroundColor: 'rgb(54, 162, 235)',
                bordercolor: 'rgb(54, 162, 235)',
                data: data,
                fill: false,
            }
            ],
        },
        options: {
            elements: {
                line: {
                    tension: 0,
                }
            },
            responsive: true,
            maintainAspectRatio: false
        }
    }


    // Get the context of the canvas element we want to select
    var ctx = document.getElementById("myLineChart" + symbol).getContext("2d");

    // Instantiate a new chart
    var myLineChart = new Chart(ctx, config);
}