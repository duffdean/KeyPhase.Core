var app = app || {};
app.Controllers = app.Controllers || {};

(function () {
    'use strict';
    
    app.Controllers.Tasks = {
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