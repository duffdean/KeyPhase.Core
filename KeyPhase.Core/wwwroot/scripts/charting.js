var app = app || {};
app.Charting = app.Charting || {};

(function () {
    'use strict';

    app.Charting.Map = (function () {
        var tst = [
            {
                "letter": "A",
                "frequency": 0.08167
            },
            {
                "letter": "B",
                "frequency": 0.01492
            },
            {
                "letter": "C",
                "frequency": 0.02780
            },
            {
                "letter": "D",
                "frequency": 0.04253
            },
            {
                "letter": "E",
                "frequency": 0.12702
            },
            {
                "letter": "F",
                "frequency": 0.02288
            },
            {
                "letter": "G",
                "frequency": 0.02022
            },
            {
                "letter": "H",
                "frequency": 0.06094
            },
            {
                "letter": "I",
                "frequency": 0.06973
            },
            {
                "letter": "J",
                "frequency": 0.00153
            },
            {
                "letter": "K",
                "frequency": 0.00747
            },
            {
                "letter": "L",
                "frequency": 0.04025
            },
            {
                "letter": "M",
                "frequency": 0.02517
            },
            {
                "letter": "N",
                "frequency": 0.06749
            },
            {
                "letter": "O",
                "frequency": 0.07507
            },
            {
                "letter": "P",
                "frequency": 0.01929
            },
            {
                "letter": "Q",
                "frequency": 0.00098
            },
            {
                "letter": "R",
                "frequency": 0.05987
            },
            {
                "letter": "S",
                "frequency": 0.06333
            },
            {
                "letter": "T",
                "frequency": 0.09056
            },
            {
                "letter": "U",
                "frequency": 0.02758
            },
            {
                "letter": "V",
                "frequency": 0.01037
            },
            {
                "letter": "W",
                "frequency": 0.02465
            },
            {
                "letter": "X",
                "frequency": 0.00150
            },
            {
                "letter": "Y",
                "frequency": 0.01971
            },
            {
                "letter": "Z",
                "frequency": 0.00074
            }
        ]


        var margin = { top: 40, right: 20, bottom: 30, left: 40 },
            width = $('#dash-bar').parent().width() - margin.left - margin.right,
            height = $('#dash-bar').parent().height() - margin.top - margin.bottom;

        var formatPercent = d3.format(".0%");

        var x = d3.scale.ordinal()
            .rangeRoundBands([0, width], .1);

        var y = d3.scale.linear()
            .range([height, 0]);

        var xAxis = d3.svg.axis()
            .scale(x)
            .orient("bottom");

        var yAxis = d3.svg.axis()
            .scale(y)
            .orient("left")
            .tickFormat(formatPercent);



        var svg = d3.select("#dash-bar").append("svg")
            .attr("width", width + margin.left + margin.right)
            .attr("height", height + margin.top + margin.bottom)
            .append("g")
            .attr("transform", "translate(" + margin.left + "," + margin.top + ")");


        var data = tst;

        //d3.tsv("data.tsv", type, function(error, data) {
        x.domain(data.map(function (d) { return d.letter; }));
        y.domain([0, d3.max(data, function (d) { return d.frequency; })]);

        svg.append("g")
            .attr("class", "x axis")
            .attr("transform", "translate(0," + height + ")")
            .call(xAxis);

        svg.append("g")
            .attr("class", "y axis")
            .call(yAxis)
            .append("text")
            .attr("transform", "rotate(-90)")
            .attr("y", 6)
            .attr("dy", ".71em")
            .style("text-anchor", "end")
            .text("Frequency");

        svg.selectAll(".bar")
            .data(data)
            .enter().append("rect")
            .attr("class", "bar")
            .attr("x", function (d) { return x(d.letter); })
            .attr("width", x.rangeBand())
            .attr("y", function (d) { return y(d.frequency); })
            .attr("height", function (d) { return height - y(d.frequency); })

        //});

        function type(d) {
            d.frequency = +d.frequency;
            return d;
        }


    });

    app.Charting.Flot = (function () {
        var data = [{ "ProjectName": "My first project", "TotalTasks": 7 }, { "ProjectName": "My second project", "TotalTasks": 0 }, { "ProjectName": "my third project", "TotalTasks": 0 }, { "ProjectName": "my foruth project", "TotalTasks": 0 }, { "ProjectName": "my fifth projk", "TotalTasks": 0 }, { "ProjectName": "my ss projksss", "TotalTasks": 0 }, { "ProjectName": "dfgdsgsdfgfsdg", "TotalTasks": 0 }, { "ProjectName": "ddfgdf", "TotalTasks": 0 }, { "ProjectName": "ghdhfhfh", "TotalTasks": 0 }, { "ProjectName": "sdfgsdgsdfg", "TotalTasks": 0 }, { "ProjectName": "dfgsdgdsgdsgsf", "TotalTasks": 0 }, { "ProjectName": "dhfhdfh5555", "TotalTasks": 0 }, { "ProjectName": "12313213212312", "TotalTasks": 0 }, { "ProjectName": "fghdfh4444444", "TotalTasks": 0 }, { "ProjectName": "1q1q2w23e4ree3w2", "TotalTasks": 0 }, { "ProjectName": "g444ff44ff44", "TotalTasks": 0 }, { "ProjectName": "zzzzzzzzzzzzzzzz", "TotalTasks": 0 }, { "ProjectName": "zffzzzz", "TotalTasks": 0 }, { "ProjectName": "my first task", "TotalTasks": 0 }, { "ProjectName": "test", "TotalTasks": 0 }, { "ProjectName": "test", "TotalTasks": 0 }, { "ProjectName": "rdfgdfg", "TotalTasks": 0 }];

        var bar_array = [];

        $.each(data, function (i, bar) { bar_array.push([bar.ProjectName, bar.TotalTasks]); });

        $.plot("#dash-bar", [bar_array], {
            series: {
                bars: {
                    show: true,
                    barWidth: 0.8,
                    align: "center",
                    fillColor: 'rgb(27, 185, 225)',
                    lineWidth: 0,
                }                
            },
            xaxis: {
                mode: "categories",
                tickLength: 0
            }
        });
    });

    app.Charting.Test = (function () {
        var mdata = [[0, 5], [1, 6], [2, 3], [4, 1]];
        var bar_array = [];

        $.each(mdata, function (i, bar) { bar_array.push({ data: [bar], bars: { show: true, fill: 0.9 } }); });

        $.plot($('#dash-bar'), bar_array);
    });

    app.Charting.Table = (function () { });

    app.Charting.TaskPerProject = (function (obj) {
        var chartData = [];

        $.each(obj, function (i, bar) { chartData.push([bar.ProjectName, bar.TotalTasks]); });

        $.plot("#dash-bar", [chartData], {
            series: {
                bars: {
                    show: true,
                    barWidth: 0.8,
                    align: "center",
                    fillColor: 'rgb(27, 185, 225)',
                    lineWidth: 0,
                }
            },
            xaxis: {
                mode: "categories",
                tickLength: 0
            }
        });
    });

    app.Charting.MostRecentTaskTable = (function (obj) {
        var table = $('.dash-mostRecent').find('tbody');

        _.each(obj, function (item) {
            table.append(
                '<tr>' +
                '<td>' + item.TaskName + '</td>' +
                '<td>' + item.ProjectName + '</td>' +
                '<td>' + item.Cost + '</td>' +
                '</tr>');
        });
    })
})();