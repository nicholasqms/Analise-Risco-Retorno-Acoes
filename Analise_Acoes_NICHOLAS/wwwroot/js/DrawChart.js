function drawLineChartPeriod(symbol,start) {
    var DataPoints = [];
    var labels = [], data = [];
    var end = new Date();

    var jsonData = $.ajax({
        url: '/StockAnalyser/ClosingChart/',
        data: { 'symbol': symbol, "start": start, "end": end.toISOString() },
        dataType: 'json',
        success: function (response) {
            for (i in response) {
                DataPoints[i] = (response[i]);
            }

            // Split labels and data into separate arrays
            for (i in DataPoints) {
                labels[i] = DataPoints[i].x;
                data[i] = DataPoints[i].y;
            }

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
    //start.setMonth(8);
    // Get the context of the canvas element we want to select
    var ctx = document.getElementById("myLineChart" + symbol).getContext("2d");

    // Instantiate a new chart
    var myLineChart = new Chart(ctx, config);
}


function AddDataSetType(symbol, start, chartType) {
    var url = '/StockAnalyser/' + chartType +'Chart';
    chartColors = [
        "rgb(255, 99, 132)",
        "rgb(255, 159, 64)",
        "rgb(255, 205, 86)",
        "rgb(75, 192, 192)",
        "rgb(54, 162, 235)",
        "rgb(153, 102, 255)",
        "rgb(201, 203, 207)",
        "rgb(197,57,57)",
        "rgb(57,197,183)",
        "rgb(146,91,30)",
    ];
    if (chartType === 'Volume' || chartType === 'Histogram') {
        configType = 'bar';
    }
    else {
        configType = 'line';
    }

    var newColor = chartColors[Math.floor(Math.random() * chartColors.length)];
    var newDataset2 = {
        label: symbol,
        borderColor: newColor,
        backgroundColor: newColor,
        fill: false,
        data: [],
    };
    var dataPoints2 = [];
    var labels2 = [], data2 = [];


    var jsonData2 = $.ajax({
        url: url,
        data: { 'symbol': symbol, "start": start, "end": end.toISOString() },
        dataType: 'json',
        success: function (response) {
            for (i in response) {
                dataPoints2[i] = (response[i]);
            }

            // Split labels and data into separate arrays
            for (i in dataPoints2) {
                labels2.push(dataPoints2[i].x);
                data2.push(dataPoints2[i].y);  
            }
            myLineChart.update();

            for (var index = 0; index < data2.length; ++index) {
                newDataset2.data.push(data2[index]);
            }
            myLineChart.update();
        },
    });
    config.data.datasets.push(newDataset2);
    myLineChart.update();

};
