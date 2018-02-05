using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace KeyPhase.Repository.Interface
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        ICollection<T> FindAll(Expression<Func<T, bool>> match);
    }
}
