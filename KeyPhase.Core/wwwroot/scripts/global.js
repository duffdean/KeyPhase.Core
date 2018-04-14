var app = app || {};
app.Global = app.Global || {};

(function () {
    'use strict';

    app.Global.BaseAddress = "http://localhost:61238/api/";
    app.Global.AuthAddress = "http://18.221.239.64/KeyPhase.Users/api/"; //"http://localhost:62734/api/";
    app.Global.ChatAddress = "http://18.221.239.64/KeyPhase.Chat/"; // Staging = http://18.221.239.64/KeyPhase.Chat/ || Dev = http://localhost:1240/
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
    app.Global.InitChatService = function () {
        $.ajax({
            url: app.Global.ChatAddress + "api/service",
            type: "GET"
        }).done(function (obj) {
            var html = '<iframe src="' + app.Global.ChatAddress + '" frameborder="0" class="chatContainer-frame"></iframe>';

            $('.chatContainer').append(html);
        }).fail(function (obj) {
            var html = '<div class="chatContainer-unavailable"><i class="material-icons chatContainer-unavailableIcon">cloud_off</i><div class="chatContainer-unavailableTxt">Messaging Service Unavailable</div></div>';

            $('.chatContainer').append(html);
        });
        //
    }
})();