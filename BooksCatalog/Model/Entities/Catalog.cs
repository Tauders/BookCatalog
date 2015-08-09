namespace BooksCatalog.Model.Entities
{
    public class Catalog : BaseEntity
    {
        public string Title { get; set; }
        public long? ParentId { get; set; }
    }
}