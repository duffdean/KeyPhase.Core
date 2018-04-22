var app = app || {};
app.View = app.View || {};

(function () {
    'use strict';

    var page, viewModel, vmFunctions;

    function VM() {
        var vm = this;

        vm.login = page.helpers.loginRedirect;

        return vm;
    }

    vmFunctions = {
        computed: {
        },

        events: {
        },

        mappers: {
        },

        subscriptions: {
        }
    };

    page = {
        viewModel: null,

        events: {
        },

        getters: {
        },

        helpers: {
            loginRedirect: function () {
                window.location.href = "http://keyphase.net/login";
            }
        },

        init: function () {
            ko.applyBindings(viewModel);
        }
    };

    viewModel = new VM();
    page.viewModel = viewModel;

    app.View.Home = page;

    $(document).ready(page.init);
})();