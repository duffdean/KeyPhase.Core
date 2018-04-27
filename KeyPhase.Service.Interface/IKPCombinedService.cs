using KeyPhase.Models.DTO;
using KeyPhase.Models.DTO.Reports;
using KeyPhase.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace KeyPhase.Service.Interface
{
    public interface IKPCombinedService
    {
        ProjectDetailed SelectedProject(int ProjectID);
        ProjectOverview UserProjectsOverview(int UserID);
        TaskDetailed TaskDetailed(int TaskID);
        TaskHistory AddTaskHistory(int TaskID, int UserID, string Value);
        ProjectDetailed CreateDefaultLayout(int ProjectID);
        ProjectOverview AddProject(int UserID, string Name, DateTime EstStartDT, DateTime EstEndDT, int PhaseID, double? Budget);
        ProjectDetailed AddTask(int UserID, string Name, DateTime EstStartDT, DateTime EstEndDT, int PhaseID, int ProjectID, double? Budget);
        ProjectOverview CreateDefaultCoreLayout(int UserID);
        List<DashTaskPerProject> GetTasksPerProject(int UserID);
        ReportingData GetReportingData(int UserID);
        ChartData GetTaskReportingData(int[] Tasks, DateTime StartDate, DateTime EndDate, double MinCost, double MaxCost, bool Overdue, int DueIn, string ReportName, int UserID);
        ReportingData GetProjectReportingData (ReportingDataProject ProjData);
        List<ReportOverview> GetReportingDataOverview(int UserID);
        ChartData GetReportByID(int ReportID);
        void TaskPhaseHistory(int PrevPhase, int CurrPhase, int TaskID, int UserID);
        //IEnumerable<Task> GetAll();
        //IEnumerable<Task> GetAllForProject(int ProjectID);
    }
}