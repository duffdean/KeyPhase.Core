var app = app || {};
app.Controllers = app.Controllers || {};

(function () {
    'use strict';

    app.Controllers.Projects = {
        GetUserProjects: function (id) {
            return $.ajax({
                url: app.Global.BaseAddress + "projects/",
                type: "GET",
                data: {
                    UserID: id,
                },
                success: function (response) {
                },
                error: function (xhr) {
                }
            });
        },

        Get: function (options, id) {
            return $.ajax({
                url: app.Global.BaseAddress + "projects/",
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