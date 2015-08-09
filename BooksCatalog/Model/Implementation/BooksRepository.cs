using System.Collections.Generic;
using System.Linq;
using BooksCatalog.Model.Entities;

namespace BooksCatalog.Model.Implementation
{
    public class BooksRepository : CacheRepository<Book>
    {
        public BooksRepository(List<Book> listOfEntities)
            : base(listOfEntities)
        {
        }

        public override List<Book> Search(string str)
        {
            return
                Entities.Where(
                    x => x.Annotation.ToLower().Contains(str.ToLower()) || x.Title.ToLower().Contains(str.ToLower()))
                    .ToList();
        }
    }
}