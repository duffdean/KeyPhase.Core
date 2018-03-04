using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KeyPhase.Repository.Interface
{
    public interface IRepository<T> where T : class
    {
        T Add(T t);
        T Get(int ID);
        IQueryable<T> GetAll();
        Task<ICollection<T>> GetAllAsync();
        ICollection<T> FindAll(Expression<Func<T, bool>> Match);
        T Update(T t, object key);
    }
}
