var app = app || {};
app.Controllers = app.Controllers || {};

(function () {
    'use strict';

    app.Controllers.Tasks = {
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