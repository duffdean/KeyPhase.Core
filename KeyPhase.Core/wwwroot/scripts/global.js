var app = app || {};
app.Global = app.Global || {};

(function () {
    'use strict';

    app.Global.BaseAddress = "http://localhost:61238/api/";
    app.Global.Controllers = {
        Compound: "compound/",
        Project: "projects/",
        Task: "tasks/"
    };
    app.Global.DragScrollListener = (function () {
        $(document).on('DOMNodeInserted', function (e) {
            if ($(e.target).hasClass('proj-overview')) {
                dragscroll.reset();
            }
        });
    });
})();