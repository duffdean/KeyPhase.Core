var app = app || {};
app.View = app.View || {};

(function () {
    'use strict';

    var page, viewModel, vmFunctions;

    function VM() {
        var vm = this;
        vm.register = ko.observable(false);
        vm.registerUser = vmFunctions.events.register;
        vm.login = vmFunctions.events.login;
        //vm.processStarting = ko.observable(false);
        //vm.loadingProcesses = ko.observable(false);
        vm.registerToggle = vmFunctions.events.registerToggle;
        vm.email = ko.observable("");
        vm.password = ko.observable("");
        vm.repeatPassword = ko.observable("");
        vm.errorMessage = ko.observable("");
        vm.clearError = vmFunctions.events.clearError;

        return vm;
    }

    vmFunctions = {
        computed: {
        },

        events: {
            register: function () {
                if (page.helpers.validEmail(viewModel.email())) {
                    if (viewModel.password().length <= 0) {
                        viewModel.errorMessage("Please enter a password")
                    }
                    if (viewModel.password() != viewModel.repeatPassword()) {
                        viewModel.errorMessage("Passwords do not match");
                    }
                }
                else {
                    viewModel.errorMessage("Invalid email entered")
                }
            },
            login: function () {
                if (page.helpers.validEmail(viewModel.email())) {
                    if (viewModel.password().length <= 0) {
                        viewModel.errorMessage("Please enter a password")
                    }
                }
                else {
                    viewModel.errorMessage("Invalid email entered")
                }
            },
            registerToggle: function () {
                viewModel.register(!viewModel.register());
            },
            clearError: function () {
                viewModel.errorMessage("");
            }
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
            validEmail: function (email) {
                var re;

                re = /^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$/i;

                return re.test(email);
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