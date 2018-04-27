var app = app || {};
app.Controllers = app.Controllers || {};
// CORS https://stackoverflow.com/questions/47523265/jquery-ajax-no-access-control-allow-origin-header-is-present-on-the-requested
(function () {
    'use strict';

    app.Controllers.Tasks = {
        TaskPhaseHistory: function (prevPhase, currPhase, taskID, userID) {
            return $.ajax({
                url: app.Global.BaseAddress + app.Global.Controllers.Compound + "TaskPhaseHistory",
                type: "GET",
                data: {
                    PrevPhase: prevPhase,
                    CurrPhase: currPhase,
                    TaskID: taskID,
                    UserID: userID
                }
            }).done(function (obj) {

            }).fail(function (obj) {

            });
        },
        GetReportByID: function (reportID) {
            return $.ajax({
                url: app.Global.BaseAddress + app.Global.Controllers.Compound + "GetReportByID",
                type: "GET",
                data: {
                    ReportID: reportID
                }
            }).done(function (obj) {

            }).fail(function (obj) {

            });
        },
        GetReportingDataOverview: function (userID) {
            return $.ajax({
                url: app.Global.BaseAddress + app.Global.Controllers.Compound + "GetReportingDataOverview",
                type: "GET",
                data: {
                    UserID: userID
                }
            }).done(function (obj) {

            }).fail(function (obj) {

            });
        },//string Tasks, DateTime StartDate, DateTime EndDate, double MinCost, double MaxCost, bool Overdue, int DueIn
        GetTaskReportingData: function (tasks, startDT, endDT, minCost, maxCost, overdue, dueIn, reportName, userID) {
            return $.ajax({
                url: app.Global.BaseAddress + app.Global.Controllers.Compound + "GetTaskReportingData",
                type: "POST",
                dataType: 'json',
                data: {
                    "Tasks": tasks,
                    "StartDate": startDT,
                    "EndDate": endDT,
                    "MinCost": minCost,
                    "MaxCost": maxCost,
                    "Overdue": overdue,
                    "DueIn": dueIn,
                    "ReportName": reportName,
                    "UserID": userID
                }
            }).done(function (obj) {

            }).fail(function (obj) {

            });
        },
        GetProjectReportingData: function (projData) {
            return $.ajax({
                url: app.Global.BaseAddress + app.Global.Controllers.Compound + "GetProjectReportingData",
                type: "POST",
                dataType: 'json',
                data: {
                    ProjData: projData
                }
            }).done(function (obj) {

            }).fail(function (obj) {

            });
        },
        GetReportingData: function (userID) {
            return $.ajax({
                url: app.Global.BaseAddress + app.Global.Controllers.Compound + "GetReportingData",
                type: "GET",
                data: {
                    UserID: userID
                }
            }).done(function (obj) {

            }).fail(function (obj) {

            });
        }, 
        GetOverdueTasks: function (userID) {
            return $.ajax({
                url: app.Global.BaseAddress + app.Global.Controllers.Task + "GetOverdueTasks",
                type: "GET",
                data: {
                    UserID: userID
                }
            }).done(function (obj) {

            }).fail(function (obj) {

            });
        },
        GetActiveVsComplete: function (projID, userID) {
            return $.ajax({
                url: app.Global.BaseAddress + app.Global.Controllers.Task + "GetActiveVsComplete",
                type: "GET",
                data: {
                    ProjectID: projID,
                    UserID: userID
                }
            }).done(function (obj) {

            }).fail(function (obj) {

            });
        },
        GetTaskPerProject: function (userID) {
            return $.ajax({
                url: app.Global.BaseAddress + app.Global.Controllers.Compound + "GetTaskPerProject",
                type: "GET",
                data: {
                    UserID: userID
                }
            }).done(function (obj) {

            }).fail(function (obj) {

            });
        },
        AddTask: function (userID, name, estStartDT, estEndDT, phaseID, projectID, cost) {
            return $.ajax({
                url: app.Global.BaseAddress + app.Global.Controllers.Compound + "AddTask",
                type: "POST",
                dataType: 'json',
                data: {
                    UserID: userID,
                    Name: name,
                    EstStartDT: estStartDT,
                    EstEndDT: estEndDT,
                    PhaseID: phaseID,
                    ProjectID: projectID,
                    Cost: cost
                }
            }).done(function (obj) {

            }).fail(function (obj) {

            });
        },
        AddTaskHistory: function (userID, taskID, value) {
            return $.ajax({
                url: app.Global.BaseAddress + app.Global.Controllers.Compound + "AddTaskHistory",
                type: "POST",
                dataType: 'json',
                data: {
                    UserID: userID,
                    TaskID: taskID,
                    Value: value
                }
            }).done(function (obj) {

            }).fail(function (obj) {

            });
        },
        GetMostRecent: function (userID) {
            return $.ajax({
                url: app.Global.BaseAddress + app.Global.Controllers.Task + "GetMostRecent",
                type: "GET",
                data: {
                    UserID: userID
                }
            }).done(function (obj) {

            }).fail(function (obj) {

            });
        },
        GetTaskDetailed: function (taskID) {
            return $.ajax({
                url: app.Global.BaseAddress + app.Global.Controllers.Compound + "GetTaskDetailed",
                type: "GET",
                data: {
                    TaskID: taskID
                }
            }).done(function (obj) {

            }).fail(function (obj) {

            });
        },
        UpdateTaskPhase: function (phaseID, taskID) {
            return $.ajax({
                url: app.Global.BaseAddress + app.Global.Controllers.Task + "UpdateTaskPhase",
                type: "POST",
                dataType: 'json',
                data: {
                    PhaseID: phaseID,
                    TaskID: taskID
                }
            }).done(function (obj) {

            }).fail(function (obj) {

            });
        },
        GetProjectTasks: function (id) {
            return $.ajax({
                url: app.Global.BaseAddress + "tasks",
                type: "GET",
                data: {
                    ProjectID: id,
                }
            }).done(function (obj) {

            }).fail(function (obj) {

            });
        }
    };

})();