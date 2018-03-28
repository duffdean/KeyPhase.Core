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


        public ProjectService(IRepository<Project> ProjRepository, IRepository<UserProject> userProjRepository,
            IRepository<Phase> PhaseRepository, IRepository<PhaseUser> PhaseUserRepository)
        {
            _projRepository = ProjRepository;
            _userProjRepository = userProjRepository;
            _phaseRepository = PhaseRepository;
            _phaseUserRepository = PhaseUserRepository;
        }

        public IEnumerable<Project> GetAll()
        {
            var a = _projRepository.GetAll();

            return a;
        }

        public IEnumerable<Project> GetAllForUser(int UserID)
        {
            var b = _userProjRepository.FindAll(c => c.UserID == UserID);
            //var a = _testRepository.FindAll(c => c.Name == "test project");
            IEnumerable<Project> a = _projRepository.GetAll().Where(p => b.Any(cb => cb.ProjectID == p.ID));

            return a;
        }
        
    }
}
