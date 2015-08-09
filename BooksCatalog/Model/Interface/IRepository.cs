using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using BooksCatalog.Model.Entities;

namespace BooksCatalog.Model.Interface
{
    public interface IRepository<T>
        where T : BaseEntity
    {
        List<T> GetAll();
        T GetById(long id);
        void Add(T entity);
        void Update(T entity);
        void Delete(long id);
        List<T> Where(Expression<Func<T, bool>> condition);
        List<T> Search(string str);
    }
}