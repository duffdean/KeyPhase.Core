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
        private readonly IRepository<Project> _testRepository;
        private readonly IRepository<UserProject> _userProjRepository;

        public ProjectService(IRepository<Project> testRepository, IRepository<UserProject> userProjRepository)
        {
            _testRepository = testRepository;
            _userProjRepository = userProjRepository;
        }

        public IEnumerable<Project> GetAll()
        {
            var a = _testRepository.GetAll();

            return a;
        }

        public IEnumerable<Project> FindAll()
        {
            var b = _userProjRepository.FindAll(c => c.UserID == 1);
            //var a = _testRepository.FindAll(c => c.Name == "test project");
            IEnumerable<Project> a = _testRepository.GetAll().Where(p => b.Any(cb => cb.ProjectID == p.ID));
           
            return a;
        }
        
    }
}
