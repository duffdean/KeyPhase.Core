var app = app || {};
app.View = app.View || {};

(function () {
    'use strict';

    var page, viewModel, vmFunctions;

    function VM() {
        var vm = this;

        vm.userProjects = ko.observable([]);
        vm.projectOverview = ko.observable(null);
        vm.projectTasks = ko.observableArray([]);
        vm.currentProject = ko.observable(null);
        vm.currentTask = ko.observable(null);

        vm.closePopup = page.helpers.ClosePopup;
        vm.taskPopup = page.helpers.TaskPopup;
        vm.dragPhase = page.events.DragPhase;
        vm.login = page.helpers.loginRedirect;
        vm.dashSideSlide = page.helpers.dashSideSlide;
        vm.updateUI = page.helpers.updateUI;
        vm.loadProjectData = page.events.LoadProjectData;
        vm.postComment = page.helpers.PostComment;

        vm.createCustom = page.events.CreateCustomLayout;
        vm.createDefault = page.events.CreateDefaultLayout;

        vm.currentPage = ko.observable('');
        vm.KPSettings = ko.observableArray();
        vm.contentLoading = ko.observable(false);



        return vm;
    }

    vmFunctions = {
        computed: {
        },

        events: {
            GetProjects: function () {
                var requests = [];

                requests.push(page.getters.GetUserProjects());
                requests.push(page.getters.GetProjectsOverview());

                $.when.apply(undefined, requests).then(function () {
                    viewModel.contentLoading(false);
                });
            },
            GetProjectData: function () {
                var requests = [];

                requests.push(page.getters.GetProjectData());

                $.when.apply(undefined, requests).then(function () {
                    viewModel.contentLoading(false);
                });
            },
            GetTaskData: function () {

            },
            GetDashboardData: function () {

            },
            UpdateTaskPhase: function (phaseID, taskID) {
                app.Controllers.Tasks.UpdateTaskPhase(phaseID, taskID);
            }
        },
        //May not need any mappers now, as data is retruned as json by default. So can probably get away with ko.fromJS mapping.
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
            DragPhase: function (event) {
                event.dataTransfer.setData
                    ('target_id', ev.target.id);
            },
            LoadProjectData: function (proj) {
                var requests = [];

                $('.proj-overview').fadeOut();

                //show loader while getting data 
                requests.push(page.getters.GetProjectData(proj.ID));

                $.when.apply(undefined, requests).then(function () {
                    //remove loader
                    page.events.DragDrop();
                });
            },
            DragDrop: function () {
                $(".tasktest").draggable({ cursor: "crosshair", revert: "invalid" });

                $(".tester").droppable({
                    accept: ".tasktest",
                    drop: function (event, ui, a, b) {
                        //Figure out what is being dropped first then perform some actions
                        vmFunctions.events.UpdateTaskPhase(ko.dataFor(event.target).ID(), ko.dataFor(ui.draggable[0]).ID());
                        ko.dataFor(ui.draggable[0]).ID(); //ID of dropped task.
                        ko.dataFor(event.target).ID(); //ID of the location task was dropped.
                        console.log("drop");
                        var dropped = ui.draggable;
                        var droppedOn = $(this);
                        $(dropped).detach().css({ top: 0, left: 0 }).appendTo(droppedOn);


                    },
                    over: function (event, elem) {
                        $(this).addClass("over");
                        console.log("over");
                    }
                    ,
                    out: function (event, elem) {
                        $(this).removeClass("over");
                    }
                });
                $(".tester").sortable();
            },
            CreateDefaultLayout: function (Proj) {
                return app.Controllers.Projects.CreateDefaultLayout(Proj.Project.ID())
                    .done(function (obj) {
                        viewModel.currentProject(ko.mapping.fromJS(obj));
                    })
                    .always(function () {

                    });
            },
            CreateCustomLayout: function () {
                debugger;
            }
        },

        getters: {
            GetProjectsOverview: function () {
                return app.Controllers.Projects.GetProjectsOverview(1) //replace with userid
                    .done(function (obj) {
                        viewModel.projectOverview(ko.mapping.fromJS(obj));
                        //viewModel.currentProject(obj);
                    })
                    .always(function () {

                    });
            },
            GetProjectData: function (ProjID) {

                return app.Controllers.Projects.GetProjectDetailed(ProjID)
                    .done(function (obj) {
                        viewModel.currentProject(ko.mapping.fromJS(obj));
                        //viewModel.currentProject(obj);
                    })
                    .always(function () {

                    });
            },
            GetUserProjects: function () {

                return app.Controllers.Projects.GetUserProjects(1)
                    .done(function (obj) {
                        if (viewModel.userProjects().length) {
                            viewModel.userProjects([]);
                        }

                        for (var i = 0; i < obj.length; i++) {
                            viewModel.userProjects().push(ko.mapping.fromJS(obj[i]));
                        }
                        //viewModel.userProjects(ko.mapping.fromJS(obj));
                        //viewModel.userProjects(_.map(obj, vmFunctions.mappers.MapProject));
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
            GetTaskDetailed: function (taskID) {

                return app.Controllers.Tasks.GetTaskDetailed(taskID)
                    .done(function (obj) {
                        viewModel.currentTask(ko.mapping.fromJS(obj));
                        //viewModel.currentTask(_.map(obj, vmFunctions.mappers.MapTask));
                    })
                    .always(function () {

                    });
            },
        },

        helpers: {
            PostComment: function (item, event) {
                var comment = $('.popup-task-chat').val();

                if (comment.length) {
                    return app.Controllers.Tasks.AddTaskHistory(
                        1,
                        viewModel.currentTask().Task.ID(),
                        comment
                    )
                        .done(function (obj) {
                            viewModel.currentTask().TaskHistory.push(ko.mapping.fromJS(obj));
                            $('.popup-task-chat').val("");
                            $('.popup-task-history').animate({ scrollTop: $('.popup-task-history').prop("scrollHeight") }, 1000);
                        })
                        .always(function () {

                        });
                }
                

            },
            ClosePopup: function () {
                $('.bg-overlay').fadeOut();
                $('.popup-task').fadeOut();
                viewModel.currentTask(null);
            },
            KPSettings: function () {
                viewModel.KPSettings({
                    Pages: {
                        Dash: 'dashboard',
                        Projects: 'projects',
                        Tasks: 'workload',
                        Stream: 'stream',
                        Reports: 'reports'
                    }
                });

                viewModel.currentPage(viewModel.KPSettings().Pages.Dash);
            },
            updateUI: function () {
                var curTab;

                curTab = event.currentTarget.text.toLowerCase();

                if (curTab !== viewModel.currentPage()) {
                    viewModel.contentLoading(true);
                    viewModel.currentPage(curTab);

                    switch (curTab) {
                        case viewModel.KPSettings().Pages.Dash:
                            vmFunctions.events.GetDashboardData();
                            break;
                        case viewModel.KPSettings().Pages.Projects:
                            vmFunctions.events.GetProjects();
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
            TaskPopup: function () {
                //var page, taskPop;

                //viewModel.currentTask(this);
                page.getters.GetTaskDetailed(this.ID);
                $('.bg-overlay').fadeIn();
                $('.popup-task').fadeIn();
                //page = $('.kp');
                //taskPop = $('.proj-popup');
                //taskPop.fadeIn();
                //page.addClass('kp-main-blur');
                //page.addClass('kp-main-blurTrans');
                
            },

            fillHeight: function () {
                //$('.contentArea').height($(document).height() - 50);
                //$('.dash-sidebar').height($(document).height() - 50);
            },

            dashSideSlide: function () {
                var content, sideBar, spacer, dashContent;

                content = $('.dashSidebarContent');
                sideBar = $('.projSidebar');
                spacer = $('.ca-dash-spacer');
                dashContent = $('.ca-dash-content');

                if (content.is(':visible')) {
                    $('.projSidebar-chevron').removeClass('lnr-chevron-left').addClass('lnr-chevron-right');
                    spacer.removeClass('col-xs-2');
                    dashContent.removeClass('col-xs-10');
                    dashContent.addClass('col-xs-12');
                    content.hide();
                    sideBar.width(20);
                }
                else {
                    $('.projSidebar-chevron').removeClass('lnr-chevron-right').addClass('lnr-chevron-left');
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
            //requests.push(page.getters.GetUserProjects());
            //requests.push(gets.GetProjects());
            $.when.apply(undefined, requests).then(function () {
                page.helpers.removeLoader();
                app.Global.DragScrollListener();

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