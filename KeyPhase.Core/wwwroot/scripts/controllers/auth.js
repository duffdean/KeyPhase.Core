var app = app || {};
app.Controllers = app.Controllers || {};

(function () {
    'use strict';

    app.Controllers.Auth = {
        Login: function (email, pass) {
            return $.ajax({
                url: app.Global.AuthAddress + app.Global.Controllers.Auth + "Login",
                type: "GET",
                dataType: "json",
                data: {
                    Email: email,
                    Pass: pass
                }
            })
                .done(function (obj) {
                })
                .fail(function (obj) {
                });
        }
    };

})();