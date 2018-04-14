using KeyPhase.Models.Models;
using KeyPhase.Repository.Interface;
using KeyPhase.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace KeyPhase.Service
{
    public class ProjectService : IProjectService
    {
        private readonly IRepository<Project> _projRepository;
        private readonly IRepository<UserProject> _userProjRepository;
        private readonly IRepository<PhaseUser> _phaseUserRepository;
        private readonly IRepository<Phase> _phaseRepository;
        private readonly IRepository<Team> _teamRepository;
        private readonly IRepository<TeamProject> _teamProjectRepository;
        private readonly IRepository<TeamUser> _teamUserRepository;


        public ProjectService(IRepository<Project> ProjRepository, IRepository<UserProject> userProjRepository,
            IRepository<Phase> PhaseRepository, IRepository<PhaseUser> PhaseUserRepository, IRepository<Team> TeamRepository,
            IRepository<TeamProject> TeamProjectRepository, IRepository<TeamUser> TeamUserRepository)
        {
            _projRepository = ProjRepository;
            _userProjRepository = userProjRepository;
            _phaseRepository = PhaseRepository;
            _phaseUserRepository = PhaseUserRepository;
            _teamRepository = TeamRepository;
            _teamProjectRepository = TeamProjectRepository;
            _teamUserRepository = TeamUserRepository;
        }

        public IEnumerable<Project> GetAll()
        {
            var a = _projRepository.GetAll();

            return a;
        }

        public List<Project> GetAllForUser(int UserID)
        {
            List<Project> uProjects = new List<Project>();
            IEnumerable<UserProject> userProjects = _userProjRepository.FindAll(c => c.UserID == UserID);
            List<TeamUser> usersTeams = _teamUserRepository.FindAll(ut => ut.UserID == UserID).ToList();
            IEnumerable<TeamProject> teamProjects = _teamProjectRepository.GetAll().Where(p => usersTeams.Any(cb => cb.TeamID == p.TeamID));

            List<Project> projects = _projRepository.GetAll().ToList();

            uProjects.AddRange(projects.Where(p => userProjects.Any(cb => cb.ProjectID == p.ID)));
            uProjects.AddRange(projects.Where(p => teamProjects.Any(cb => cb.ProjectID == p.ID)));

            return uProjects;
        }

        public List<Project> GetAllForTeam(int UserID)
        {
            return null;
        }

    }
}
