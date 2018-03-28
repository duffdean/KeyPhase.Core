var app = app || {};
app.Global = app.Global || {};

(function () {
    'use strict';

    app.Global.BaseAddress = "http://localhost:61238/api/";
    app.Global.AuthAddress = "http://18.221.239.64/KeyPhase.Users/api/"; //"http://localhost:62734/api/";
    app.Global.Controllers = {
        Compound: "Compound/",
        Project: "Projects/",
        Task: "Tasks/",
        Auth: "Auth/"
    };
    app.Global.DragScrollListener = (function () {
        $(document).on('DOMNodeInserted', function (e) {
            if ($(e.target).hasClass('proj-overview')) {
                dragscroll.reset();
            }
        });
    });
    app.Global.Popup = function (a,b) {
        debugger
    };
})();