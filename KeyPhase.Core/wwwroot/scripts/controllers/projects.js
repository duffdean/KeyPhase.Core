var app = app || {};
app.Controllers = app.Controllers || {};

(function () {
    'use strict';

    app.Controllers.Projects = {
        GetProjectDetailed: function (id) {
            return $.ajax({
                url: app.Global.BaseAddress + app.Global.Controllers.Compound,
                type: "GET",
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