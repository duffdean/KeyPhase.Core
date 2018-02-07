var app = app || {};
app.View = app.View || {};

(function () {
    'use strict';

    var page, viewModel, vmFunctions;

    function VM() {
        var vm = this;

        vm.userProjects = ko.observableArray([]);

        vm.login = page.helpers.loginRedirect;
        vm.dashSideSlide = page.helpers.dashSideSlide;
        vm.updateUI = page.helpers.updateUI;

        return vm;
    }

    vmFunctions = {
        computed: {
        },

        events: {
            GetDashboardData: function () {
                page.getters.GetUserProjects();
            }            
        },

        mappers: {
            MapProject: function (project) {
                project = $.extend({
                    ID: null,
                    Name: '',
                    PhaseID: null,
                    EstStartDate: '',
                    EstEndDate: '',
                    ActStartDate: '',
                    ActEndDate: '',
                    Active: null
                }, project);

                project.ID = project.ID;
                project.Name = project.Name;
                project.PhaseID = project.PhaseID;
                project.EstStartDate = project.EstStartDate;
                project.EstEndDate = project.EstEndDate;
                project.ActStartDate = project.ActStartDate;
                project.ActEndDate = project.ActEndDate;
                project.Active = project.Active;

                return project;
                
            },
        },

        subscriptions: {
        }
    };

    page = {
        viewModel: null,

        events: {
        },

        getters: {
            GetUserProjects: function () {
                
                return app.Controllers.Projects.GetUserProjects(1)
                    .done(function (obj) {
                        viewModel.userProjects(_.map(obj, vmFunctions.mappers.MapProject));
                    })
                    .always(function () {

                    });
            }
        },

        helpers: {
            updateUI: function () {
                var curTab;

                curTab = event.currentTarget.text.toLowerCase();

                vmFunctions.events.GetDashboardData();
            },

            fillHeight: function () {
                //$('.contentArea').height($(document).height() - 50);
                //$('.dash-sidebar').height($(document).height() - 50);
            },

            dashSideSlide: function () {
                var content, sideBar;

                content = $('.dashSidebarContent');
                sideBar = $('.dash-sidebar');

                if (content.is(':visible')) {
                    $('.sideChev').removeClass('lnr-chevron-left').addClass('lnr-chevron-right');
                    content.hide();
                    sideBar.width(20);
                }
                else {
                    $('.sideChev').removeClass('lnr-chevron-right').addClass('lnr-chevron-left');
                    sideBar.css("width", "");
                    content.fadeIn();
                }
            },

            removeLoader: function () {
                //setTimeout(function () {
                //    $('#kploader').fadeOut("slow", function () {
                //        $(this).remove();
                //    });
                //}, 5000);
                $('#kploader').fadeOut("slow", function () {
                    $(this).remove();
                });
            }
        },

        init: function () {
            var requests = [];

            ko.applyBindings(viewModel);
            page.helpers.fillHeight();

            requests.push(page.getters.GetUserProjects());
            //requests.push(gets.GetProjects());
            $.when.apply(undefined, requests).then(function () {
                page.helpers.removeLoader();
            });
        }
    };

    viewModel = new VM();
    page.viewModel = viewModel;

    app.View.Home = page;

    $(document).ready(page.init);
})();