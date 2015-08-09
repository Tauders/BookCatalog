using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BooksCatalog.Model.Entities;

namespace BooksCatalog.Model.Implementation
{
    //public class CatalogsRepository:CacheRepository<Catalog>
    //{
    //    public CatalogsRepository(List<Catalog> listOfEntities)
    //        : base(listOfEntities)
    //    {
    //        List<Catalog> hirecacicalCatalogs = new List<Catalog>();

    //        foreach (var catalog in Entities)
    //        {
    //            var entities = Entities.Where(x => x.ParentId == catalog.Id);
    //            catalog.Children.AddRange(Entities.Where(x=>x.ParentId == catalog.Id));
    //        }
    //    }
    //}
}
