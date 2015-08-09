using System;
using System.Collections.Generic;
using System.Linq;
using BooksCatalog.Model.Entities;
using BooksCatalog.Model.Interface;

namespace BooksCatalog.Model.Implementation
{
    public class CacheRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly List<T> Entities;

        public CacheRepository(List<T> listOfEntities)
        {
            Entities = listOfEntities;
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
            Entities[Entities.FindIndex(x => x.Id == entity.Id)] = entity;
        }

        public void Delete(long id)
        {
            Entities.RemoveAll(x => x.Id == id);
        }
    }
}