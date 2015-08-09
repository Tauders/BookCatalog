using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using BooksCatalog.Model.Entities;
using BooksCatalog.Model.Interface;

namespace BooksCatalog.Model.Implementation
{
    public class FileRepository<T> : IRepository<T> where T : BaseEntity
    {
        public List<T> GetAll()
        {
            XmlSerializer formatter = new XmlSerializer(typeof (List<T>));
            List<T> entities;
            using (FileStream fs = new FileStream(Path.Combine("Data", typeof (T).Name + ".xml"), FileMode.OpenOrCreate)
                )
            {
                entities = (List<T>) formatter.Deserialize(fs);
            }
            return entities;
        }

        public T GetById(long id)
        {
            throw new NotImplementedException();
        }

        public void Add(T entity)
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(long id)
        {
            throw new NotImplementedException();
        }
    }
}