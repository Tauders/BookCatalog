using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using BooksCatalog.Model.Entities;
using BooksCatalog.Model.Interface;

namespace BooksCatalog.Model.Implementation
{
    public class CacheRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly List<T> Entities;

        public CacheRepository(List<T> listOfEntities)
        {
            Entities = new List<T>(listOfEntities);
        }

        public List<T> GetAll()
        {
            return Entities;
        }

        public T GetById(long id)
        {
            return Entities.Find(x => x.Id == id);
        }

        public void Add(T entity)
        {
            Entities.Add(entity);
        }

        public void Update(T entity)
        {
            var index = Entities.FindIndex(x => x.Id == entity.Id);
            if (index == -1) { throw new Exception("Нет сущности для обновления");}
            Entities[index] = entity;
        }

        public void Delete(long id)
        {
            Entities.RemoveAll(x => x.Id == id);
        }

        public List<T> Where(Expression<Func<T, bool>> condition)
        {
            return Entities.AsQueryable().Where(condition).ToList();
        }

        public virtual List<T> Search(string str)
        {
            //TODO Сделать общий поиск
            throw new NotImplementedException();
        }
    }
}