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

    app.Charting.ActiveVsComplete = (function (obj) {
        var data = [{ "Series": "Complete", "Total": 2 }, { "Series": "Active", "Total": 5 }];
        var chartData = [];
        var colour;
        //var data = [
        //	{ label: "Series1",  data: 10},
        //	{ label: "Series2",  data: 30}
        //];


        $.each(data, function (i, bar) {
            if (bar.Series.toLowerCase() == "complete") {
                colour = "#2e86de";
            }
            else {
                colour = "#ee5253";
            }
            chartData.push({ label: bar.Series, data: bar.Total, color: colour   })
        });

        function labelFormatter(label, series) {
            return "<div style='font-size:8pt; text-align:center; padding:2px; color:white;'>" + label + "<br/>" + Math.round(series.percent) + "%</div>";
        }

        $.plot('#dash-avsc', chartData, {
            series: {
                pie: {
                    show: true,
                    radius: 1,
                    label: {
                        show: true,
                        radius: 2 / 3,
                        threshold: 0.1,
                        formatter: labelFormatter,
                    }
                }
            },
            legend: {
                show: false
            }
        });
    });

    app.Charting.OverdueTasks = (function (obj) {
        //var chartData = [];

        //$.each(obj, function (i, bar) { chartData.push([bar.DaysOverdue, bar.TaskName]); });


        var rawData = [];
        var ticks = [];
        var max = 0;

        for (var i = 0; i < obj.length; i++) {
            if (max < obj[i].DaysOverdue) {
                max = obj[i].DaysOverdue + 5;
            }
            rawData.push([obj[i].DaysOverdue, i]);
            ticks.push([i, obj[i].TaskName]);
        }
        
        var dataSet = [{ label: "Days Overdue", data: rawData, color: "#1bb9e1" }];

        

        var options = {
            series: {
                bars: {
                    show: true
                }
            },
            bars: {
                align: "center",
                barWidth: 0.8,
                horizontal: true,
                fillColor: 'rgb(27, 185, 225)',
                lineWidth: 0
            },
            xaxis: {
                axisLabel: "Days Overdue",
                //axisLabelUseCanvas: true,
                //axisLabelFontSizePixels: 12,
                //axisLabelFontFamily: 'Verdana, Arial',
                //axisLabelPadding: 10,
                max: max
                //tickColor: "#5E5E5E",
                //color: "black"
            },
            yaxis: {
                axisLabel: "Task Name",
                //axisLabelUseCanvas: true,
                //axisLabelFontSizePixels: 12,
                //axisLabelFontFamily: 'Verdana, Arial',
                //axisLabelPadding: 3,
                //tickColor: "#5E5E5E",
                ticks: ticks//,
                //color: "black"
            },
            legend: {
                noColumns: 0,
                //labelBoxBorderColor: "#858585",
                position: "ne"
            },
            grid: {
               // hoverable: true,
                //borderWidth: 2,
                //backgroundColor: { colors: ["#171717", "#4F4F4F"] }
            }
        };






















        $.plot($("#dash-bar-overdue"), dataSet, options);




        //$.plot("#dash-bar-overdue", [chartData], {
        //    series: {
        //        bars: {
        //            align: "center",
        //            barWidth: 0.5,
        //            horizontal: true,
        //            fillColor: { colors: [{ opacity: 0.5 }, { opacity: 1 }] },
        //            lineWidth: 1
        //        }
        //    },
        //    xaxis: {
        //        mode: "categories",
        //        tickLength: 0
        //    }
        //});
    });

    app.Charting.MostRecentTaskTable = (function (obj) {
        var table = $('.dash-mostRecent').find('tbody');
        var cost;
        _.each(obj, function (item) {
            if (item.Cost >= 0) {
                cost = '£' + item.Cost;
            }
            else {
                cost = '£0.00'
            }

            table.append(
                '<tr>' +
                '<td>' + item.TaskName + '</td>' +
                '<td>' + item.ProjectName + '</td>' +
                '<td>' + cost + '</td>' +
                '</tr>');
        });
    });

    app.Charting.CurrentProjectGantt = (function (projData) {
        var ganttData = [], currValues;

        _.each(projData.PhaseTasks(), function (item) {
            currValues = [];

            _.each(item.Tasks(), function (task) {
                currValues.push({
                    from: task.EstStartDate(),
                    to: task.EstEndDate(),
                    label: task.Name(),
                    dataObj: item
                })
            });

            ganttData.push({
                name: item.Name(),
                desc: '',
                values: currValues
            })
        });

        $(".proj-gantt").gantt({
            source: ganttData,
            scale: "weeks",
            minScale: "hours",
            navigate: "scroll",
            onItemClick: function (data) {
                app.View.Home.viewModel.taskPopup(data.Tasks()[0].ID());
            },
        });
    });

    app.Charting.CurrentProjectTable = (function (projData) {
        var currPhase, html = $('.projectTable');


        _.each(projData.PhaseTasks(), function (item, index) {
            
            html.append(
                '<div class="col-xs-11" style="margin-left: 5px; font-size: 25px;">' + item.Name() + '</div>' + 
                '<div class="col-xs-11" style="background: #323641;     margin: 10px 10px 25px 20px;">' + 
                '<table class="table tbl' + index + '" style="font-size: 14px; font-weight: 100;">' +
                '<thead>' + 
                '<tr>' + 
                '<th scope="col">Task Name</th>' + 
                '<th scope="col">Cost</th>' + 
                '<th scope="col">Start Date</th>' + 
                '<th scope="col">End Date</th>' + 
                '<th scope="col">Created On</th>' + 
                '</tr>' + 
                '</thead >' + 
                '<tbody style=""></tbody>' + 
                '</table >' + 
                '' + 
                '' + 
                '' + 
                '' + 
                '' + 
                ''
               );
        });

        _.each(projData.PhaseTasks(), function (item, index) {
            currPhase = $('.' + 'tbl' + index + '').find('tbody');

            _.each(item.Tasks(), function (item, index) {

                currPhase.append(
                    '<tr>' +
                    '<td>' + item.Name() + '</td>' +
                    '<td>£ ' + item.Cost() + '</td>' +
                    '<td>' + moment(item.ActStartDate()).format('DD-MMM-YYYY') + '</td>' +
                    '<td>' + moment(item.ActEndDate()).format('DD-MMM-YYYY') + '</td>' +
                    '<td>' + moment(item.CreatedOn()).format('DD-MMM-YYYY') + '</td>' +
                    '</tr>');

            });

        });
    });

    app.Charting.ReportingPageCurrent = (function (reportData) {
        //var chartData = [];

        //$.each(obj, function (i, bar) { chartData.push([bar.DaysOverdue, bar.TaskName]); });


        var rawData = [];
        var ticks = [];
        var max = 0;

        _.each(reportData.SeriesData, function (item, index) {
            if (max < item.XSeries) {
                max = item.XSeries + 5;
            }
            rawData.push([item.XSeries, index]);
            ticks.push([index, item.YSeries]);
        });

        var dataSet = [{ label: reportData.XLabel, data: rawData, color: "#1bb9e1" }];



        var options = {
            series: {
                bars: {
                    show: true
                }
            },
            bars: {
                align: "center",
                barWidth: 0.8,
                horizontal: true,
                fillColor: 'rgb(27, 185, 225)',
                lineWidth: 0
            },
            xaxis: {
                axisLabel: reportData.XLabel,
                //axisLabelUseCanvas: true,
                //axisLabelFontSizePixels: 12,
                //axisLabelFontFamily: 'Verdana, Arial',
                //axisLabelPadding: 10,
                max: max
                //tickColor: "#5E5E5E",
                //color: "black"
            },
            yaxis: {
                axisLabel: reportData.YLabel,
                //axisLabelUseCanvas: true,
                //axisLabelFontSizePixels: 12,
                //axisLabelFontFamily: 'Verdana, Arial',
                //axisLabelPadding: 3,
                //tickColor: "#5E5E5E",
                ticks: ticks//,
                //color: "black"
            },
            legend: {
                noColumns: 0,
                //labelBoxBorderColor: "#858585",
                position: "ne"
            },
            grid: {
                // hoverable: true,
                //borderWidth: 2,
                //backgroundColor: { colors: ["#171717", "#4F4F4F"] }
            }
        };

        $.plot($("#currentReport"), dataSet, options);
    });

})();