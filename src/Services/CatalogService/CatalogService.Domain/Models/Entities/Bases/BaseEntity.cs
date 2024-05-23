namespace CatalogService.Domain.Models.Entities.Bases
{
    public abstract class BaseEntity<T>
    {
        public T Id { get; set; }
    }
}
