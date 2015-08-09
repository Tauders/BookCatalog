namespace BooksCatalog.Model.Entities
{
    //TODO расширить количество полей для книг
    public class Book: BaseEntity
    {
        public string Title { get; set; }
        public string Annotation { get; set; }
        public long CatalogId { get; set; }
    }
}