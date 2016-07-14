using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace CRIMAS.Repository.contracts
{
    interface IRepository<T> where T : class
    {
        void Add(T obj);
        IList<T> GetAll();
        T GetById(object id);
        T Find(Expression<Func<T, bool>> match);
        IList<T> FindAll(Expression<Func<T, bool>> match);
        T Update(T updated, int Key);
        bool RemoveById(object id);
        void Commit();
        int count();
    }
}
