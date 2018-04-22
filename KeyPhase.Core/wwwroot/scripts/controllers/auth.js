var app = app || {};
app.Controllers = app.Controllers || {};

(function () {
    'use strict';

    app.Controllers.Auth = {
        Register: function (email, pass, firstName, lastName) {
            return $.ajax({
                url: app.Global.AuthAddress + app.Global.Controllers.Auth + "Register",
                type: "POST",
                dataType: 'json',
                data: {
                    Email: email,
                    Pass: pass,
                    FirstName: firstName,
                    LastName: lastName
                }
            }).done(function (obj) {

            }).fail(function (obj) {

            });
        },
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