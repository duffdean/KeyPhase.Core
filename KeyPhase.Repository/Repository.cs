using KeyPhase.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using KeyPhase.Models;

namespace KeyPhase.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected KeyPhaseDbContext _context;
        protected DbSet<T> DbSet;

        public Repository(KeyPhaseDbContext context)
        {
            _context = context;
        }

        public IQueryable<T> GetAll()
        {
            var a = _context.Set<T>();
            return a;   
        }
    }
}
