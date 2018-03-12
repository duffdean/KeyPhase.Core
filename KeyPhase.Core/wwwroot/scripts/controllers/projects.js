﻿var app = app || {};
app.Controllers = app.Controllers || {};

(function () {
    'use strict';

    app.Controllers.Projects = {
        CreateDefaultLayout: function (id) {
            return $.ajax({
                url: app.Global.BaseAddress + app.Global.Controllers.Compound + "CreateDefaultLayout",
                type: "GET",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: {
                    ProjectID: id,
                }
            })
                .done(function (obj) {
                })
                .fail(function (obj) {
                });
        },
        GetProjectsOverview: function (id) {
            return $.ajax({
                url: app.Global.BaseAddress + app.Global.Controllers.Compound + "GetProjectsOverview",
                type: "GET",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: {
                    UserID: id,
                }
            })
                .done(function (obj) {
                })
                .fail(function (obj) {
                });
        },
        GetProjectDetailed: function (id) {
            return $.ajax({
                url: app.Global.BaseAddress + app.Global.Controllers.Compound + "GetProjectData",
                type: "GET",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: {
                    ProjectID: id,
                }
            })
                .done(function (obj) {
                })
                .fail(function (obj) {
                });
        },

        GetUserProjects: function (id) {
            return $.ajax({
                url: app.Global.BaseAddress + "projects/",
                type: "GET",
                data: {
                    UserID: id,
                }
            })
                .done(function (obj) {
            })
                .fail(function (obj) {
                });
        },

        Get: function (options, id) {
            return $.ajax({
                url: app.Global.BaseAddress + "projects",
                type: "GET",
                data: {
                    UserID: id,
                },
                success: function (response) {
                },
                error: function (xhr) {
                }
            });
        }
    };

})();