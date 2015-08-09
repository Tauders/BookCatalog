using System.Collections.Generic;
using BooksCatalog.Model.Entities;

namespace BooksCatalog.Model.Interface
{
    public interface IRepository<T> 
        where T: BaseEntity
    {
        List<T> GetAll();
        T GetById(long id);
        void Add(T entity);
        void Update(T entity);
        void Delete(long id);

    }
}
