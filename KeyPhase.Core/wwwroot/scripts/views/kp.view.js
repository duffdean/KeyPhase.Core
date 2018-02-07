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
            fillHeight: function () {
                //$('.contentArea').height($(document).height() - 50);
                //$('.dash-sidebar').height($(document).height() - 50);
            },

            removeLoader: function () {
                setTimeout(function () {
                    $('#kploader').fadeOut("slow", function () {
                        $(this).remove();
                    }); }, 5000);
            }
        },

        init: function () {
            var requests = [];

            ko.applyBindings(viewModel);
            page.helpers.fillHeight();

            //requests.push(gets.GetProjects());

            $.when.apply($, requests).done(function () {
                //page.getters.GetTasks();
            });

            //This is here just for testing, it should fire inside ajax done on getters.
            page.helpers.removeLoader();
            


        }
    };

    viewModel = new VM();
    page.viewModel = viewModel;

    app.View.Home = page;

    $(document).ready(page.init);
})();