using KeyPhase.Models.Models;
using KeyPhase.Repository.Interface;
using KeyPhase.Service.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace KeyPhase.Service
{
    public class ProjectService : IProjectService
    {
        private readonly IRepository<Project> _testRepository;

        public ProjectService(IRepository<Project> testRepository)
        {
            _testRepository = testRepository;
        }

        public IEnumerable<Project> GetAll()
        {
            var a = _testRepository.GetAll();

            return a;
        }
    }
}
