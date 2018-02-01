using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KeyPhase.Repository.Interface
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll();
    }
}
