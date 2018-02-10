var app = app || {};
app.View = app.View || {};

(function () {
    'use strict';

    var page, viewModel, vmFunctions;

    function VM() {
        var vm = this;

        vm.userProjects = ko.observableArray([]);
        vm.projectTasks = ko.observableArray([]);

        vm.login = page.helpers.loginRedirect;
        vm.dashSideSlide = page.helpers.dashSideSlide;
        vm.updateUI = page.helpers.updateUI;
        vm.loadProjectData = page.events.LoadProjectData;

        vm.currentPage = ko.observable('');
        vm.KPSettings = ko.observableArray();
        vm.contentLoading = ko.observable(false);

        return vm;
    }

    vmFunctions = {
        computed: {
        },

        events: {
            GetProjectData: function () {
                var requests = [];
                
                requests.push(page.getters.GetUserProjects());

                $.when.apply(undefined, requests).then(function () {
                    viewModel.contentLoading(false);
                });                
            },
            GetTaskData: function () {

            },
            GetDashboardData: function () {
                
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
            MapTask: function (task) {
                task = $.extend({
                    ID: null,
                    Name: '',
                    PhaseID: null,
                    EstStartDate: '',
                    EstEndDate: '',
                    ActStartDate: '',
                    ActEndDate: '',
                    Cost: null,
                    Complete: null,
                    Active: null
                }, task);

                task.ID = task.ID;
                task.Name = task.Name;
                task.PhaseID = task.PhaseID;
                task.EstStartDate = task.EstStartDate;
                task.EstEndDate = task.EstEndDate;
                task.ActStartDate = task.ActStartDate;
                task.ActEndDate = task.ActEndDate;
                task.Cost = task.ActEndDate;
                task.Complete = task.ActEndDate;
                task.Active = task.Active;

                return task;
            },
        },

        subscriptions: {
        }
    };

    page = {
        viewModel: null,

        events: {
            LoadProjectData: function (proj) {
                var requests = [];
                //show loader while getting data 
                requests.push(page.getters.GetProjectTasks(proj.ID));

                $.when.apply(undefined, requests).then(function () {
                    //remove loader
                });
            }
        },

        getters: {
            GetUserProjects: function () {

                return app.Controllers.Projects.GetUserProjects(1)
                    .done(function (obj) {
                        viewModel.userProjects(_.map(obj, vmFunctions.mappers.MapProject));
                    })
                    .always(function () {

                    });
            },
            GetProjectTasks: function (projID) {

                return app.Controllers.Tasks.GetProjectTasks(projID)
                    .done(function (obj) {
                        viewModel.projectTasks(_.map(obj, vmFunctions.mappers.MapTask));
                    })
                    .always(function () {

                    });
            },
        },

        helpers: {     
            KPSettings: function () {
                viewModel.KPSettings({
                    Pages: {
                        Dash: 'dashboard',
                        Projects: 'my projects',
                        Tasks: 'my tasks',
                        Stream: 'stream',
                        Reports: 'reports'
                    }
                });

                viewModel.currentPage(viewModel.KPSettings().Pages.Dash);
            },
            updateUI: function () {
                var curTab;                
                
                curTab = event.currentTarget.text.toLowerCase();

                if (curTab != viewModel.currentPage()) {
                    viewModel.contentLoading(true);
                    viewModel.currentPage(curTab);

                    switch (curTab) {
                        case viewModel.KPSettings().Pages.Dash:
                            vmFunctions.events.GetDashboardData();
                            break;
                        case viewModel.KPSettings().Pages.Projects:
                            vmFunctions.events.GetProjectData();
                            break;
                        case viewModel.KPSettings().Pages.Tasks:
                            break;
                        case viewModel.KPSettings().Pages.Stream:
                            break;
                        case viewModel.KPSettings().Pages.Reports:
                            break;
                    }
                }
            },

            fillHeight: function () {
                //$('.contentArea').height($(document).height() - 50);
                //$('.dash-sidebar').height($(document).height() - 50);
            },

            dashSideSlide: function () {
                var content, sideBar, spacer, dashContent;

                content = $('.dashSidebarContent');
                sideBar = $('.dash-sidebar');
                spacer = $('.ca-dash-spacer');
                dashContent = $('.ca-dash-content');

                if (content.is(':visible')) {
                    $('.sideChev').removeClass('lnr-chevron-left').addClass('lnr-chevron-right');
                    spacer.removeClass('col-xs-2');
                    dashContent.removeClass('col-xs-10');
                    dashContent.addClass('col-xs-12');
                    content.hide();
                    sideBar.width(20);
                }
                else {
                    $('.sideChev').removeClass('lnr-chevron-right').addClass('lnr-chevron-left');
                    spacer.addClass('col-xs-2');
                    dashContent.removeClass('col-xs-12');
                    dashContent.addClass('col-xs-10');
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
            //page.helpers.fillHeight();
            page.helpers.KPSettings()
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

ko.bindingHandlers.fadeVisible = {
    init: function (element, valueAccessor) {
        // Initially set the element to be instantly visible/hidden depending on the value
        var value = valueAccessor();
        $(element).toggle(ko.unwrap(value)); // Use "unwrapObservable" so we can handle values that may or may not be observable
    },
    update: function (element, valueAccessor) {
        // Whenever the value subsequently changes, slowly fade the element in or out
        var value = valueAccessor();
        ko.unwrap(value) ? $(element).fadeIn() : $(element).fadeOut();
    }
};