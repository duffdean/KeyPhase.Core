﻿using KeyPhase.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using KeyPhase.Models;
using System.Linq.Expressions;
using System.Threading.Tasks;

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

        public virtual T Add(T t)
        {

            _context.Set<T>().Add(t);
            _context.SaveChanges();
            return t;
        }

        public T Get(int ID)
        {
            return _context.Set<T>().Find(ID);
        }

        public IQueryable<T> GetAll()
        {
            var a = _context.Set<T>();
            return a;   
        }

        public async Task<ICollection<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public ICollection<T> FindAll(Expression<Func<T, bool>> match)
        {
            return _context.Set<T>().Where(match).ToList();
        }

        public virtual T Update(T t, object key)
        {
            if (t == null)
                return null;
            T exist = _context.Set<T>().Find(key);
            if (exist != null)
            {
                _context.Entry(exist).CurrentValues.SetValues(t);
                _context.SaveChanges();
            }
            return exist;
        }
    }
}
